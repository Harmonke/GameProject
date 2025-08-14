using System;
using System.Runtime.InteropServices.WindowsRuntime;
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

    void OnTriggerExit(Collider other)
    {
        planet = findDeepSpace.GetComponent<Planet>();
    }

    void Update()
    {
        GroundPlayerDistance();
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
    void GroundPlayerDistance()
    {
        planetPosition = planet.returnPosition();
        playerPosition = transform.position;
        planetPlayerDistance = playerPosition - planetPosition;
    }

    //Returns planetPlayerDistance to be used in PlayerScript.
    public Vector3 PlanetPlayerDistance()
    {
        return planetPlayerDistance;
    }

    public Boolean returnDeepSpace()
    {
        return planet.returnDeepSpace();
    }
}
