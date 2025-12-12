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
    Rigidbody rb;

    //This game object is the game object that every other planet orbits.
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Planet" tag and assigns it to the findPlanet field.
        findDeepSpace = GameObject.FindWithTag("DeepSpace");
        //Finds the Planet object/script within the Planet object.
        planet = findDeepSpace.GetComponent<Planet>();
        rb = GetComponent<Rigidbody>();
        planets = GameObject.FindGameObjectsWithTag("Planet");
        gravityPlanets = new Planet[planets.Length];
        for (int i = 0; i < planets.Length; i += 1)
        {
            gravityPlanets[i] = planets[i].GetComponent<Planet>();
        }


        
    }

    Boolean firstContact;
    bool applyGravity;
    Boolean resetBaseRotation;
    
    PlanetMovement planetMovement;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GravityCollider")
        {
            applyGravity = false;
            planet = other.GetComponentInParent<Planet>();
            planetMovement = other.GetComponentInParent<PlanetMovement>();
        }

       

        if (other.gameObject.tag == "FirstContact")
        {
            if (!firstContact)
            {
                resetBaseRotation = true;
                rb.linearVelocity = Vector3.zero;
            }
            
            
            firstContact = true;
        }
        
        if (other.gameObject.tag == "DeepSpace")
        {
            planet = findDeepSpace.GetComponent<Planet>();
            applyGravity = false;
            firstContact = false;
        }
            
    }

    public Vector3 PlanetPosition()
    {
        return planetMovement.PlanetDelta();
    }

   
   
        

            
      



    void LateUpdate()
    {
        resetBaseRotation = false;
    }

    //Gets position from planet script.
    public Vector3[] PlanetPositions()
    {
        if (!firstContact)
        {
            Vector3[] planetPositions = new Vector3[gravityPlanets.Length];
            for (int i = 0; i < gravityPlanets.Length; i += 1)
            {
                planetPositions[i] = gravityPlanets[i].returnPosition();
            }
            return planetPositions;
        }
        else
        {
            Vector3[] planetPosition = new Vector3[1];
            planetPosition[0] = planet.returnPosition(); 
            return planetPosition;
        }
        
    }

    //Gets gravityconstant from planet script.
    public float[] PlanetGravityConstants()
    {
        if (!firstContact)
        {
            float[] planetGravityConstants = new float[gravityPlanets.Length];
            for (int i = 0; i < gravityPlanets.Length; i += 1)
            {
                planetGravityConstants[i] = gravityPlanets[i].returnGravityConstant();
            }
            return planetGravityConstants;
        }
        else
        {
            float[] planetGravityConstant = new float[1];
            planetGravityConstant[0] = planet.returnGravityConstant(); 
            return planetGravityConstant;
        }
        
        
    }


    //Gets radius from planet script.
    public float[] PlanetRadiuses()
    {
        if (!firstContact)
        {
            float[] planetRadiuses = new float[gravityPlanets.Length];
            for (int i = 0; i < gravityPlanets.Length; i += 1)
            {
                planetRadiuses[i] = gravityPlanets[i].returnRadius();
            }
            return planetRadiuses;
        }
        else
        {
            float[] planetRadius = new float[1];
            planetRadius[0] = planet.returnGravityConstant(); 
            return planetRadius;
        }
        
        
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
