using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxStage1;
    public Material skyboxStage2;
    public Material skyboxStage3;

    public Light directionalLight; // Reference to the Directional Light
    public Light[] lights; // Manually assign the Lights here
    public GameObject particleSystem; // Add this line to reference the Particle System

    public enum SkyboxState
    {
        Day,
        Blend,
        Night
    }

    public SkyboxState currentSkyboxState;

    void Start()
    {
        // Initialize to the first stage
        SetSkyboxStage(SkyboxState.Day);
    }

    void Update()
    {
        SetSkyboxStage(currentSkyboxState);
    }

    void SetSkyboxStage(SkyboxState newState)
    {
        Color newLightColor = Color.white; // Default color (full daytime)

        switch (newState)
        {
            case SkyboxState.Day:
                RenderSettings.skybox = skyboxStage1;
                AdjustDirectionalLight(1.0f, newLightColor); // Set full daytime intensity and color

                // Turn off all the lights
                foreach (Light light in lights)
                {
                    light.enabled = false;
                }
                break;

            case SkyboxState.Blend:
                RenderSettings.skybox = skyboxStage2;
                AdjustDirectionalLight(0.5f, newLightColor); // Set intermediate intensity and color

                // Turn on all the lights
                foreach (Light light in lights)
                {
                    light.enabled = true;
                }
                break;

            case SkyboxState.Night:
                RenderSettings.skybox = skyboxStage3;
                newLightColor = new Color(0.2f, 0.2f, 0.4f); // Adjust the color for nighttime
                AdjustDirectionalLight(0.2f, newLightColor); // Set nighttime intensity and color

                // Turn on all the lights
                foreach (Light light in lights)
                {
                    light.enabled = true;
                }

                // Enable the particle system at night
                EnableParticleSystemAtNight();
                break;
        }

        currentSkyboxState = newState;
    }

    // Add this method
    void EnableParticleSystemAtNight()
    {
        // Check if the particleSystem variable has been set in the inspector
        if (particleSystem != null)
        {
            if (currentSkyboxState == SkyboxState.Night)
            {
                particleSystem.SetActive(true);
            }
            else
            {
                particleSystem.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("ParticleSystem is not assigned in the inspector.");
        }
    }

    void AdjustDirectionalLight(float intensity, Color color)
    {
        // Adjust the intensity and color of the Directional Light
        if (directionalLight != null)
        {
            directionalLight.intensity = intensity;
            directionalLight.color = color;
        }
    }
}