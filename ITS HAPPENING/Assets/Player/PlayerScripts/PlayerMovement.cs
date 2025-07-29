using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    
    public float horizontalInput;
    float jumpInput;
    public float verticalInput;
    

    public float movementSpeed;
    public Vector3 gravity;
    public float jumpForce;
    public bool onGround;
    
    
    public Quaternion baseRotation;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // FixedUpdate is called once per set amount of time (for physics related stuff like moving).
    void FixedUpdate()
    {
        Movement();
        lockMouse();
        Jump();
    }



    Vector3 movementForce;
    
    //function calculating and enforcing the force on the rigidbody based on inputs and characters y-rotation.
    void Movement()
    {
        //Floats storing the value of input based on inputs selected in input manager, can either be -1, 0 or 1.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Calculates the movementForce.
        Vector3 inputDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        movementForce = inputDirection.normalized * movementSpeed;
    }

    public Vector3 returnMovementForce()
    {
        return movementForce;
    }

    //cooldowns to make consecutive jumps slower.
    IEnumerator onGroundPause()
    {

        yield return new WaitForSeconds(0.1f); // 100 ms delay — adjust as needed
        onGround = true;
    }

    

    //Checks if player is grounded
    bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.1f))
        {
            StartCoroutine(onGroundPause());
        }
        else
        {
            onGround = false;
        }
        return onGround;

    }

    Vector3 jumpMovementForce;
    //Applies jump force based on up direction.
    void Jump()
    {
        jumpForce = 40f;
        jumpInput = Input.GetAxisRaw("Jump");

        if (CheckGrounded())
        {
            jumpMovementForce = transform.up * jumpForce * jumpInput;
        }
        else
        {
            jumpMovementForce = new Vector3(0f, 0f, 0f);
        }
    }

    public Vector3 returnJumpMovementForce()
    {
        return jumpMovementForce;
    }


    //Locks mouse to the middle of screen and makes it invisibile.
    void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    

    

    

    

    
    

    
}
