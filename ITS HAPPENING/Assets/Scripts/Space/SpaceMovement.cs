using UnityEngine;

public class SpaceMovement : MonoBehaviour
{

    GameObject input;
    Inputs inputScript;
    
    
    
    Rigidbody rb;

    [SerializeField] float movementSpeed;
    float spaceShipMovementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        input = GameObject.FindWithTag("InputObject");
        inputScript = input.GetComponent<Inputs>();

        

        rb = GetComponent<Rigidbody>();

        spaceShipMovementSpeed = movementSpeed * rb.mass;
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

    [SerializeField] float torqueSpeed;
    [SerializeField] float leftRollSpeed;
    [SerializeField] float rightRollSpeed;

    //Calculates and returns the spaceRotation, this is torque and needs to be called in rb.AddRelativeTorque().
    public void ApplySpaceRotation()
    {
        //Calculates and combines space ship roll.
        float leftRoll = inputScript.LeftRollInput() * leftRollSpeed;
        float rightRoll = -inputScript.RightRollInput() * rightRollSpeed;
        float roll = leftRoll + rightRoll;

        //Gets mouse rotation
        float xRotation = GetXRotation();
        float yRotation = GetYRotation();

        //Calculates and applies rotation torque.
        Vector3 torque = new Vector3(-xRotation, yRotation, roll) * torqueSpeed;
        rb.AddRelativeTorque(torque, ForceMode.Acceleration);
    }


    
    [SerializeField] float upAndDownForce;
    //Calculates and returns the spaceMovementForce based on inputs. 
    public void ApplySpaceMovement()
    {
        //Floats storing the value of input based on inputs selected in input manager, can either be -1, 0 or 1.
        float horizontalInput = inputScript.HorizontalInput();
        float verticalInput = inputScript.VerticalInput();

        //Calculates the wasd movementForce.
        Vector3 inputDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        Vector3 movementForce = inputDirection.normalized * spaceShipMovementSpeed;

        //Calculates the space/ctrl movementForce.
        float moveDown = -inputScript.MoveUp() * upAndDownForce;
        float moveUp = inputScript.MoveUp() * upAndDownForce;
        Vector3 spaceVerticalMovement = moveUp * transform.up + moveDown * transform.up;

        //Combines the forces and applies them.
        Vector3 spaceMovementForce = movementForce + spaceVerticalMovement;
        rb.AddForce(spaceMovementForce, ForceMode.Force);
    }
    
}
