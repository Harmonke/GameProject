using Unity.Mathematics;
using UnityEngine;

public class RelativePlanetMovement : MonoBehaviour
{
    public GameObject orbitPlanet;
    Transform orbitPlanetTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orbitPlanetTransform = orbitPlanet.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public Vector3 ReturnOrbit()
    {
        Vector3 center = orbitPlanetTransform.position;

        // offset vector (from center to planet)
        Vector3 offset = transform.position - center;

        // tangent vector (perpendicular to offset, in XZ plane)
        Vector3 tangent = new Vector3(-offset.z, 0f, offset.x).normalized;

        // scale by orbit speed
        return tangent;
    }
    

}
