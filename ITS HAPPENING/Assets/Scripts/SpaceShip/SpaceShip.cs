using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    
    Gravity gravity;
    SpaceMovement spaceMovement;
    

    public GameObject cockpit;
    SpaceShipInteract spaceShipInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gravity = GetComponent<Gravity>();

        spaceMovement = GetComponent<SpaceMovement>();

        spaceShipInteract = cockpit.GetComponent<SpaceShipInteract>();
        
    }

    // FixedUpdate is called once every physics tick.
    void FixedUpdate()
    {
        gravity.ApplyPlanetGravity();

        if (spaceShipInteract.returnPlayerSitting())
        {
            spaceMovement.ApplySpaceRotation();
            spaceMovement.ApplySpaceMovement();
        }
        
    }

    
    //Returns spaceship position.
    public Vector3 ReturnPosition()
    {
        return transform.position;
    }

    
    

}
