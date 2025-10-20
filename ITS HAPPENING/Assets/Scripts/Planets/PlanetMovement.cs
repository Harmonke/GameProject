using Unity.Mathematics;
using UnityEngine;

public class RelativePlanetMovement : MonoBehaviour
{
    public GameObject player;
    GetPlanet getPlanet;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        getPlanet = player.GetComponent<GetPlanet>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        applyMovement();
    }



    Vector3 ReturnOrbit()
    {
        Vector3 center = new Vector3(0f, 0f, 0f);

        // offset vector (from center to planet)
        Vector3 offset = transform.position - center;

        // tangent vector (perpendicular to offset, in XZ plane)
        Vector3 tangent = new Vector3(-offset.z, 0f, offset.x).normalized;

        // scale by orbit speed
        return tangent;
    }

    void applyMovement()
    {
        rb.MovePosition(transform.position + ReturnOrbit() * -1f);
    }
    

    

}
