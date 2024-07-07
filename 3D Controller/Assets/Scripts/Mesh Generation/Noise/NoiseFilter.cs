using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter
{
    private Noise noise;

    private ShapeSettings shapeSettings;

    public NoiseFilter(ShapeSettings _shapeSettings)
    {
        noise = new Noise();
        shapeSettings = _shapeSettings;
    }

    public Vector3 SetNoisePosition(Vector3 _vertexPosition)
    {
        //  float noiseValue = noise.Evaluate(_vertexPosition);
        //  Vector3 transformedVertexPosition = _vertexPosition + (Vector3.up)
        float noiseValue = 0;

        foreach (var layer in shapeSettings.NoiseLayers)
        {
            if (!layer.enabled) { continue; }

            NoiseSettings noiseSettings = layer.NoiseSettings;
            float layerValue = 0;
            float currentFrequency = noiseSettings.BaseRoughness;
            float currentAmplitude = noiseSettings.Strength;

            float roughness = noiseSettings.Roughness;
            float persistence = noiseSettings.Persistence;

            for (int i = 0; i < noiseSettings.LayerCount; i++)
            {
                float currentValue = noise.Evaluate(noiseSettings.NoiseCenter + (_vertexPosition * currentFrequency));
                currentValue = (currentValue + 1) * 0.5f; //remaps the value between 0 and 1

                layerValue += currentValue * currentAmplitude;

                currentFrequency *= roughness;
                currentAmplitude *= persistence;
            }

            layerValue = Mathf.Max(0, layerValue - noiseSettings.GroundLevel);
            noiseValue += layerValue;
        }
        // OCE Version return _vertexPosition * (1 + noiseValue)
        return (_vertexPosition * shapeSettings.size) + (Vector3.up * (noiseValue ));
    }
}
