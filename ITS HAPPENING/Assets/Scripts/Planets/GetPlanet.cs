using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GetPlanet : MonoBehaviour
{
    public GameObject findDeepSpace;
    private Planet planet;

    GameObject[] planets;
    Planet[] gravityPlanets;


    //This game object is the game object that every other planet orbits.
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Planet" tag and assigns it to the findPlanet field.
        findDeepSpace = GameObject.FindWithTag("DeepSpace");
        //Finds the Planet object/script within the Planet object.
        planet = findDeepSpace.GetComponent<Planet>();

        planets = GameObject.FindGameObjectsWithTag("Planet");
        gravityPlanets = new Planet[planets.Length];
        for (int i = 0; i < planets.Length; i += 1)
        {
            gravityPlanets[i] = planets[i].GetComponent<Planet>();
        }


        
    }

    PlanetMovement planetDelta;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GravityCollider")
        {
            planet = other.GetComponentInParent<Planet>();
            planetDelta = other.GetComponentInParent<PlanetMovement>();
        }
    }

    public Vector3 ReturnPlanetDelta()
    {
        return planetDelta.ReturnOrbit();
    }

    
    void OnTriggerExit(Collider other)
    {
        planet = findDeepSpace.GetComponent<Planet>();
        
        firstContact = false;
    }

    Boolean firstContact;
    Boolean resetBaseRotation;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            if (!firstContact)
            {
                resetBaseRotation = true;
            }

            firstContact = true;
            

            
        }
    }



    void LateUpdate()
    {
        resetBaseRotation = false;
    }

    //Gets position from planet script.
    public Vector3[] PlanetPositions()
    {
        Vector3[] planetPositions = new Vector3[gravityPlanets.Length];
        for (int i = 0; i < gravityPlanets.Length; i += 1)
        {
            planetPositions[i] = gravityPlanets[i].returnPosition();
        }
        return planetPositions;
    }

    //Gets gravityconstant from planet script.
    public float[] PlanetGravityConstants()
    {
        float[] planetGravityConstants = new float[gravityPlanets.Length];
        for (int i = 0; i < gravityPlanets.Length; i += 1)
        {
            planetGravityConstants[i] = gravityPlanets[i].returnGravityConstant();
        }
        return planetGravityConstants;
        
    }


    //Gets radius from planet script.
    public float[] PlanetRadiuses()
    {
        float[] planetRadiuses = new float[gravityPlanets.Length];
        for (int i = 0; i < gravityPlanets.Length; i += 1)
        {
            planetRadiuses[i] = gravityPlanets[i].returnRadius();
        }
        return planetRadiuses;
        
    }


    //Calculates the distance between the planets core and the player
    public Vector3 CorePlayerDistance()
    {
        Vector3 planetPosition = planet.returnPosition();
        Vector3 playerPosition = transform.position;
        Vector3 corePlayerDistance = playerPosition - planetPosition;
        return corePlayerDistance;
    }


    //Calculates the distance between the planets core and the player and puts it in an array.
    public Vector3[] CorePlayerDistances()
    {
        Vector3[] corePlayerDistances = new Vector3[PlanetPositions().Length];
        for (int i = 0; i < PlanetPositions().Length; i += 1)
        {
            Vector3 planetPosition = PlanetPositions()[i];
            Vector3 playerPosition = transform.position;
            Vector3 corePlayerDistance = playerPosition - planetPosition;
            corePlayerDistances[i] = corePlayerDistance;
        }

        return corePlayerDistances;
    }

    

    public Boolean returnDeepSpace()
    {
        return planet.returnDeepSpace();
    }

    public Boolean returnFirstContact()
    {
        return firstContact;
    }

    public Boolean returnResetBaseRotation()
    {
        return resetBaseRotation;
    }
}
