using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
  private ShapeGenerator shapeGenerator;
  readonly private Mesh mesh;
  readonly private int resolution;
  readonly private Vector3 normal;
  readonly private Vector3 tangent;
  readonly private Vector3 biTangent;

  public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 normal)
  {
    this.shapeGenerator = shapeGenerator;
    this.mesh = mesh;
    this.resolution = resolution;

    normal.Normalize();
    this.normal = normal;
    this.tangent = new Vector3(normal.y, normal.z, normal.x);
    this.biTangent = Vector3.Cross(this.normal, this.tangent);
  }

  public void ConstructMesh()
  {
    Vector3[] vertices = new Vector3[resolution * resolution];
    int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
    int triIndex = 0;

    for (int y = 0; y < resolution; ++y)
    {
      for (int x = 0; x < resolution; ++x)
      {
        int index = x + y * resolution;
        Vector2 percent = new Vector2(x, y) / (resolution - 1);
        Vector3 pointOnUnitCube = normal + (percent.x - .5f) * 2 * tangent + (percent.y - .5f) * 2 * biTangent;
        Vector3 pointOnUnitSphere = Vector3.Normalize(pointOnUnitCube);
        vertices[index] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

        if (x != resolution - 1 && y != resolution - 1)
        {
          triangles[triIndex] = index;
          triangles[triIndex + 1] = index + resolution + 1;
          triangles[triIndex + 2] = index + resolution;

          triangles[triIndex + 3] = index;
          triangles[triIndex + 4] = index + 1;
          triangles[triIndex + 5] = index + resolution + 1;
          triIndex += 6;
        }
      }
    }

    mesh.Clear();
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();
  }
}
