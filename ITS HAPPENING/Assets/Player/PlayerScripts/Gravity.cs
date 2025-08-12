using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    
    GetPlanet getPlanet;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getPlanet = GetComponent<GetPlanet>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        planetGravity();
    }






    Vector3 planetPosition;
    Vector3 playerPosition;
    Vector3 planetPlayerDistance;
    float planetRadius;
    float gravityConstant;
    Vector3 surfacePoint;
    float groundPlayerDistance;
    float gravityForce;
    [SerializeField] Vector3 gravity;

    //Calculates the planets surface position to use it to use the difference between the player and surface position along with the planets gravity constant to calc gravity.
    void planetGravity()
    {
        planetPlayerDistance = getPlanet.PlanetPlayerDistance();
        playerPosition = transform.position;
        planetPosition = getPlanet.PlanetPosition();
        planetRadius = getPlanet.PlanetRadius();
        gravityConstant = getPlanet.PlanetGravityConstant();

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

    
    

}
