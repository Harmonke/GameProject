using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{

    GameObject input;
    Inputs inputScript;
    public GameObject cockpit;
    SpaceShipInteract spaceShipInteract;
    Rigidbody rb;

    [SerializeField] float movementSpeed;
    float spaceShipMovementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        input = GameObject.FindWithTag("InputObject");
        inputScript = input.GetComponent<Inputs>();
        spaceShipInteract = cockpit.GetComponent<SpaceShipInteract>();
        rb = GetComponent<Rigidbody>();
        spaceShipMovementSpeed = movementSpeed * rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpaceRotation();
        SpaceMovement();
    }


    float GetYRotation()
    {
        float spaceShipYRotation = inputScript.PlayerYRotation() * inputScript.Sensitivity();
        return spaceShipYRotation;
    }

    float GetXRotation()
    {
        float spaceShipXRotation = inputScript.PlayerXRotation() * inputScript.Sensitivity();
        return spaceShipXRotation;
    }

    [SerializeField] bool spaceRotation;
    float xRotation;
    float yRotation;
    float roll;

    [SerializeField] float torqueSpeed;

    void SpaceRotation()
    {
        spaceRotation = spaceShipInteract.returnPlayerSitting();
        if (spaceShipInteract.returnPlayerSitting())
        {
            bool leftRoll = inputScript.LeftRollInput();
            bool rightRoll = inputScript.RightRollInput();

            xRotation = GetXRotation();
            yRotation = GetYRotation();

            roll = 0f;
            if (leftRoll)
            {
                roll = -0.3f;
            }

            if (rightRoll)
            {
                roll = 0.3f;
            }


            Vector3 torque = new Vector3(xRotation, yRotation, roll) * torqueSpeed;
            rb.AddRelativeTorque(torque, ForceMode.Acceleration);
        }
    }

    
    Vector3 spaceMovementForce;
    void SpaceMovement()
    {

        spaceRotation = spaceShipInteract.returnPlayerSitting();
        if (spaceShipInteract.returnPlayerSitting())
        {
            //Floats storing the value of input based on inputs selected in input manager, can either be -1, 0 or 1.
            float horizontalInput = inputScript.HorizontalInput();
            float verticalInput = inputScript.VerticalInput();

            //Calculates the movementForce.
            Vector3 inputDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
            Vector3 movementForce = inputDirection.normalized * spaceShipMovementSpeed;

            float moveDown;
            if (inputScript.MoveDown())
            {
                moveDown = -1 * rb.mass;
            }
            else
            {
                moveDown = 0;
            }

            Vector3 spaceVerticalMovement = inputScript.MoveUp() * transform.up + moveDown * transform.up;

            spaceMovementForce += movementForce * 0.1f + spaceVerticalMovement * 10f;
            rb.AddForce(spaceMovementForce, ForceMode.Force);
        }
    
    }
    
}
