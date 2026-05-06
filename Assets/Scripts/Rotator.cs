using UnityEngine;

// Canonical Roll-a-Ball rotator. Spins the pickup so it reads as collectible.
public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(15f, 30f, 45f);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
