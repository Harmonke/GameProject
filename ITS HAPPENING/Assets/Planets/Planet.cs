using UnityEngine;

public class Planet : MonoBehaviour, PlanetInterface
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns Planet's position.
    public Vector3 returnPosition()
    {
        return transform.position;
    }

    //Returns the planet's gravity constant.
    public float returnGravityConstant()
    {
        float gravityConstant = 98.1f;
        return gravityConstant;
    }

    //Returns the Planet's radius.
    public float returnRadius()
    {
        return transform.localScale.x;
    }
}
