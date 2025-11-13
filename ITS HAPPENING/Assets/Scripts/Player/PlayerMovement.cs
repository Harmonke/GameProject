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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputObject = GameObject.FindWithTag("InputObject");
        inputFile = inputObject.GetComponent<Inputs>();
        getPlanet = GetComponent<GetPlanet>();
        rb = GetComponent<Rigidbody>();
        oldPlanetPos = new Vector3(1347f, 0f, 0f);

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
    
    public bool onGround;
    //cooldowns to make consecutive jumps slower.
    IEnumerator OnGroundPause()
    {

        yield return new WaitForSeconds(0.1f); // 100 ms delay — adjust as needed
        onGround = true;
        
    }


    //Checks if player is grounded
    bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.1f))
        {
            StartCoroutine(OnGroundPause());
        }
        else
        {
            onGround = false;
        }
        return onGround;

    }


    [SerializeField] float jumpMovementForce;
    //Applies jump force based on up direction.
    public Vector3 CalculateJumpForce()
    {
        float jumpInput = inputFile.MoveUp();
        Vector3 jumpForce = new Vector3(0f, 0f, 0f);
        if (CheckGrounded())
        {
            jumpForce = transform.up * jumpMovementForce * jumpInput;
        }

        return jumpForce;
    }

    Vector3 oldPlanetPos;
    public void ApplyPlanetPosition()
    {

        rb.MovePosition(rb.position + (getPlanet.PlanetPosition() - oldPlanetPos)) ;
        oldPlanetPos = getPlanet.PlanetPosition();
    }

    [SerializeField] Vector3 planetVelocity;
    //Applies the planets velocity on top of the players velocity.
    public void ApplyPlanetVelocity()
    {
        planetVelocity = getPlanet.ReturnPlanetVelocity();
        rb.linearVelocity = planetVelocity;


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
