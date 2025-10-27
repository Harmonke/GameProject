using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject player;
    private PlayerInteract playerInteract;
    Gravity gravity;
    SpaceMovement spaceMovement;
    AirResistance airResistance;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInteract = player.GetComponent<PlayerInteract>();

        gravity = GetComponent<Gravity>();

        spaceMovement = GetComponent<SpaceMovement>();

        airResistance = GetComponent<AirResistance>();

        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once every physics tick.
    void FixedUpdate()
    {
        airResistance.ApplyAirResistance();
        gravity.ApplyPlanetGravity();
        spaceMovement.ApplySpaceRotation();
        spaceMovement.ApplySpaceMovement();
    }

    
    //Returns spaceship position.
    public Vector3 ReturnPosition()
    {
        return transform.position;
    }

    
    

}
