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
    public Vector3 planetGravity()
    {
        planetPlayerDistance = getPlanet.GroundPlayerDistance();
        playerPosition = transform.position;
        planetPosition = getPlanet.PlanetPosition();
        planetRadius = getPlanet.PlanetRadius();
        gravityConstant = getPlanet.PlanetGravityConstant();

        Vector3 coreToPlayerDirection = planetPlayerDistance.normalized;
        surfacePoint = planetPosition + coreToPlayerDirection * planetRadius;

        groundPlayerDistance = Vector3.Distance(surfacePoint, playerPosition);
        gravityForce = gravityConstant / Mathf.Pow(groundPlayerDistance, 0.1f);
        gravity = -coreToPlayerDirection * gravityForce;
        return gravity;

    }

    
    
    

}
