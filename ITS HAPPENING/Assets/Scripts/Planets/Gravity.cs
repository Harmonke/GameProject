using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    
    GetPlanet getPlanet;
    Rigidbody rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getPlanet = GetComponent<GetPlanet>();
        rb = GetComponent<Rigidbody>();
    }
    [SerializeField] Vector3 gravity;

    //Calculates the planets surface position to use it to use the difference between the player and surface position along with the planets gravity constant to calc gravity.
    public void ApplyPlanetGravity()
    {
        //Gets the difference in position between the planet core and the player.
        Vector3 planetPlayerDistance = getPlanet.CorePlayerDistance();

        //Gets player and planet positions.
        Vector3 playerPosition = transform.position;
        Vector3 planetPosition = getPlanet.PlanetPosition();

        float planetRadius = getPlanet.PlanetRadius();
        float gravityConstant = getPlanet.PlanetGravityConstant();

        //Calculates surface point of planet.
        Vector3 coreToPlayerDirection = planetPlayerDistance.normalized;
        Vector3 surfacePoint = planetPosition + coreToPlayerDirection * planetRadius;

        //Calculates gravity force based on the distance between the planet surface and the player.
        float groundPlayerDistance = Vector3.Distance(surfacePoint, playerPosition);
        float gravityForce = gravityConstant / Mathf.Pow(groundPlayerDistance, 0.1f);

        //Calculates and applies gravity based on the mass of the object.
        gravity = -coreToPlayerDirection * gravityForce;
        rb.AddForce(gravity * rb.mass, ForceMode.Force);
    }

    
    
    

}
