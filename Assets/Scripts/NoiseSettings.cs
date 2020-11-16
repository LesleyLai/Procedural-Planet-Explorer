using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
  public float strength = 1;
  public float baseRoughness = 1;
  public float roughness = 1;
  public Vector3 center;
  [Range(1, 8)] public int layersCount = 6;
  public float persistence = 0.5f; // How the amplitude of noise decrease on each layer
}
