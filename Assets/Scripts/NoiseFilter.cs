using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter
{
  private NoiseSettings settings;
  private Noise noise = new Noise();

  public NoiseFilter(NoiseSettings settings)
  {
    this.settings = settings;
  }

  public float Evaluate(Vector3 point)
  {
    float value = 0;
    float frequency = settings.baseRoughness;
    float amplitude = settings.strength;

    for (int i = 0; i < settings.layersCount; ++i)
    {
      float t = noise.Evaluate(point * frequency + settings.center);
      value += (t + 1) * 0.5f * amplitude;
      frequency *= settings.roughness;
      amplitude *= settings.persistence;
    }

    return value;
  }
}
