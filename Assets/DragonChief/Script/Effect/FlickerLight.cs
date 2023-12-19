using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light lanternLight;
    public float intensityRange = 0.3f;
    public float speed = 1f;
    public float noiseScale = 0.2f;
    private float baseIntensity;
    private float timeOffset;

    void Start()
    {
        baseIntensity = lanternLight.intensity;
        timeOffset = Random.value * 10f;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(timeOffset, 0f);
        float intensity = Mathf.Lerp(baseIntensity - intensityRange, baseIntensity + intensityRange, noise);
        lanternLight.intensity = intensity;
        timeOffset += Time.deltaTime * speed * noiseScale;
    }
}
