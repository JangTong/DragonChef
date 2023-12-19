using UnityEngine;

public class Ship : MonoBehaviour
{
    public float waveFrequency = 1.0f;  // Frequency of the wave motion
    public float waveAmplitude = 0.1f;  // Amplitude of the wave motion

    private float timeCounter = 0f;     // Counter to track time for wave motion

    void Update()
    {
        // Increment the time counter based on time
        timeCounter += Time.deltaTime;

        // Calculate the horizontal and vertical offsets using sine and cosine functions
        float offsetX = Mathf.Sin(timeCounter * waveFrequency) * waveAmplitude;
        float offsetY = Mathf.Cos(timeCounter * waveFrequency) * waveAmplitude;

        // Apply the wave motion to the ship's position
        transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
    }
}