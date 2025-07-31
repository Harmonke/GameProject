using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject findPlanet;
    private Planet planet;
    public GameObject Brain;
    private CinemachineBrain CBrain;
    public GameObject head;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Planet" tag and assigns it to the findPlanet field.
        findPlanet = GameObject.FindWithTag("DeepSpace");
        //Finds the Planet object/script within the Planet object.
        planet = findPlanet.GetComponent<Planet>();
        Brain = GameObject.FindWithTag("CinemachineBrain");
        CBrain = Brain.GetComponent<CinemachineBrain>();
        head = GameObject.FindWithTag("Head");
    }

    GameObject currentPlanet;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityCollider"))
        {
            currentPlanet = other.gameObject;
            planet = currentPlanet.GetComponentInParent<Planet>();
            CBrain.WorldUpOverride = head.transform; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GravityCollider"))
        {
            currentPlanet = other.gameObject;
            planet = currentPlanet.GetComponentInParent<Planet>();
            CBrain.WorldUpOverride = head.transform; 
        }   
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
        gravityForce = gravityConstant / Mathf.Pow(groundPlayerDistance, 0.00001f);
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

    public Boolean returnDeepSpace()
    {
        return planet.returnDeepSpace();
    }

}
