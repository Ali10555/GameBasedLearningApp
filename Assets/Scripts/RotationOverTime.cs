using UnityEngine;

public class RotationOverTime : MonoBehaviour
{
    public float rotationSpeed = 150;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
