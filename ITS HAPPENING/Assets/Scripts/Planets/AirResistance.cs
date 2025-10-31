using System;
using UnityEngine;

public class AirResistance : MonoBehaviour
{
    GetPlanet getPlanet;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getPlanet = GetComponent<GetPlanet>();
        rb = GetComponent<Rigidbody>();
    }

    

    //Calculates the planets surface position to use it to use the difference between the player and surface position along with the planets airResistanceConstant to calc airresistance.
    public void ApplyAirResistance()
    {
        //Gets the difference in position between the planet core and the player.
        Vector3 planetPlayerDistance = getPlanet.CorePlayerDistance();

        //Gets player and planet positions.
        Vector3 playerPosition = transform.position;
        Vector3 planetPosition = getPlanet.PlanetPosition();

        float planetRadius = getPlanet.PlanetRadius();
        float airResistanceConstant = getPlanet.PlanetAirResistanceConstant();

        //Calculates surface point of planet.
        Vector3 coreToPlayerDirection = planetPlayerDistance.normalized;
        Vector3 surfacePoint = planetPosition + coreToPlayerDirection * planetRadius;

        //Calculates air resistance force based on the distance between the planet surface and the player.
        float groundPlayerDistance = Vector3.Distance(surfacePoint, playerPosition);
        float airResistanceForce = airResistanceConstant / Mathf.Pow(groundPlayerDistance, 0.1f);

        //Applies airresistance
        rb.linearDamping = airResistanceForce;
    }

}
