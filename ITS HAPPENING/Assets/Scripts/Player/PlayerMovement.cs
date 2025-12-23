using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    GameObject inputObject;
    Rigidbody rb;
    Inputs inputFile;
    GetPlanet getPlanet;
    Vector3 oldVelocity;
    [SerializeField] bool canJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputObject = GameObject.FindWithTag("InputObject");
        inputFile = inputObject.GetComponent<Inputs>();
        getPlanet = GetComponent<GetPlanet>();
        rb = GetComponent<Rigidbody>();
        canJump = true;
        

    }

    void Update()
    {
        GetJumpInput();
    }
    // FixedUpdate is called once per set amount of time (for physics related stuff like moving).
    void FixedUpdate()
    {
        LockMouse();
        
    }



    Vector3 CalculateInputDirection()
    {
        //Floats storing the value of input based on inputs selected in input manager, can either be -1, 0 or 1.
        float horizontalInput = inputFile.HorizontalInput();
        float verticalInput = inputFile.VerticalInput();
        Vector3 inputDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        return inputDirection;
    }


    [SerializeField] float movementSpeed;
    //function calculating and enforcing the force on the rigidbody based on inputs and characters y-rotation.
    public Vector3 CalculateMovement()
    {
        //Calculates the movementForce.
        Vector3 movementForce = CalculateInputDirection().normalized * movementSpeed;
        return movementForce;
    }
    
    
    [SerializeField] bool onGround;
    //Checks if player is grounded
    bool CheckGrounded()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f))
        {
            
            onGround = true;
            
        }
        else
        {
            onGround = false;
            canJump = true;
            
        }
        return onGround;

    }

    int jumpInput;
    //This is seperate because if you press space outside of a physics tick, the input wont go through.
    void GetJumpInput()
    {
        if (inputFile.MoveUp() == 1)
        {
            jumpInput = 1;
        }
    }

    [SerializeField] float jumpMovementForce;
    //Applies jump force based on up direction.
    public Vector3 CalculateJumpForce()
    {
        
        Vector3 jumpForce = new Vector3(0f, 0f, 0f);
        if (CheckGrounded() && canJump)
        {
            jumpForce = transform.up * jumpMovementForce * jumpInput;
            if (jumpForce != Vector3.zero)
            {
                canJump = false;
                
            }
            
        }
        jumpInput = 0;

        return jumpForce;
    }


    public void ApplyMovement()
    {
        
        Vector3 movementForce = CalculateMovement();
        Vector3 jumpForce = CalculateJumpForce();
        rb.AddForce(movementForce, ForceMode.Force); 
        rb.AddForce(jumpForce, ForceMode.Impulse);

    }
    
    public void SetLinearDamping()
    {
        rb.linearDamping = 10f;
    }

    //Locks mouse to the middle of screen and makes it invisibile.
    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
