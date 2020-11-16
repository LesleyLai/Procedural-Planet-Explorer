using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
  ShapeSettings settings;
  NoiseFilter noiseFilter;
  public ShapeGenerator(ShapeSettings settings)
  {
    this.settings = settings;
    this.noiseFilter = new NoiseFilter(settings.noiseSettings);
  }

  public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
  {
    float elevation = noiseFilter.Evaluate(pointOnUnitSphere);
    return pointOnUnitSphere * settings.radius * (1 + elevation);
  }
}