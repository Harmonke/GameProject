using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GetPlanet : MonoBehaviour
{
    public GameObject findDeepSpace;
    private Planet planet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Planet" tag and assigns it to the findPlanet field.
        findDeepSpace = GameObject.FindWithTag("DeepSpace");
        //Finds the Planet object/script within the Planet object.
        planet = findDeepSpace.GetComponent<Planet>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GravityCollider")
        {
            planet = other.GetComponentInParent<Planet>();
        }
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

    void OnTriggerExit(Collider other)
    {
        planet = findDeepSpace.GetComponent<Planet>();
        firstContact = false;
    }

    

    void LateUpdate()
    {
        resetBaseRotation = false;
    }

    //Gets position from planet script.
    public Vector3 PlanetPosition()
    {
        return planet.returnPosition();
    }

    //Gets gravityconstant from planet script.
    public float PlanetGravityConstant()
    {
        return planet.returnGravityConstant();
    }

    //Gets radius from planet script.
    public float PlanetRadius()
    {
        return planet.returnRadius();
    }

    Vector3 planetPosition;
    Vector3 playerPosition;
    [SerializeField] Vector3 planetPlayerDistance;
    //Calculates the distance between the planets surface and the player.
    public Vector3 GroundPlayerDistance()
    {
        planetPosition = planet.returnPosition();
        playerPosition = transform.position;
        planetPlayerDistance = playerPosition - planetPosition;
        return planetPlayerDistance;
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
