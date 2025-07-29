using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject findPlanet;
    private Planet planet;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Planet" tag and assigns it to the findPlanet field.
        findPlanet = GameObject.FindWithTag("Planet");
        //Finds the Planet object/script within the Planet object.
        planet = findPlanet.GetComponent<Planet>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundPlayerDistance();
        planetGravity();
    }


    Vector3 planetPosition;
    Vector3 playerPosition;
    Vector3 planetPlayerDistance;
    void GroundPlayerDistance()
    {
        planetPosition = planet.returnPosition();
        playerPosition = transform.position;
        planetPlayerDistance = playerPosition - planetPosition;
    }

    float planetRadius;
    float gravityConstant;
    Vector3 surfacePoint;
    float groundPlayerDistance;
    float gravityForce;
    Vector3 gravity;

    //Calculates the planets surface position to use it to use the difference between the player and surface position along with the planets gravity constant to calc gravity.
    void planetGravity()
    {

        planetRadius = planet.returnRadius();
        gravityConstant = planet.returnGravityConstant();

        Vector3 coreToPlayerDirection = planetPlayerDistance.normalized;
        surfacePoint = planetPosition + coreToPlayerDirection * planetRadius;

        groundPlayerDistance = Vector3.Distance(surfacePoint, playerPosition);
        gravityForce = gravityConstant / Mathf.Pow(groundPlayerDistance, 0.5f);
        gravity = -coreToPlayerDirection * gravityForce;

    }

    public Vector3 returnGravity()
    {
        return gravity;
    }

    public Vector3 returnPlanetPlayerDistance()
    {
        return planetPlayerDistance;
    }
    

}
