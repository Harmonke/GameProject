using UnityEngine;

public class RelativePlanetMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(new Vector3(0f, 1f, 0f), Vector3.up, 0.1f);
    }
}
