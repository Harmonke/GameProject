using UnityEngine;

public class FollowPlanet : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }

    Transform planetTransform;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GravityCollider")
        {
            planet = other.GetComponentInParent<Planet>();
            planetTransform = planet.GetComponent<Transform>();
        }
    }

    
    void OnTriggerExit(Collider other)
    {
        planet = findDeepSpace.GetComponent<Planet>();
    }
}
