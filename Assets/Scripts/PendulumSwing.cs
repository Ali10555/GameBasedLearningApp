using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [Header("Pendulum Parameters")]
    public float maxAngle = 45f;       // Maximum swing angle (degrees)
    public float length = 2f;          // Length of the pendulum (meters)
    public float gravity = 9.81f;      // Acceleration due to gravity

    private float time;

    void Update()
    {
        // Calculate angular frequency ω = sqrt(g / L)
        float omega = Mathf.Sqrt(gravity / length);

        // Time progression
        time += Time.deltaTime;

        // Swing angle based on sine wave
        float angle = Mathf.Sin(time * omega) * maxAngle;

        // Apply rotation
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    // Optional: Update length dynamically
    public void SetLength(float newLength)
    {
        length = Mathf.Max(0.1f, newLength); // Prevent division by zero or negative
        time = 0f; // Reset time to avoid phase issues
    }

    public float GetTimePeriod()
    {
        return 2 * Mathf.PI * Mathf.Sqrt(length / gravity);
    }
}