using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    CameraRotation cameraRotation;
    public Vector3 movementHorizontal;
    public Vector3 movementVertical;
    public Vector3 movement;
    

    public float horizontalInput;
    float jumpInput;
    float verticalInput;
    [SerializeField]
    private float tiltAroundX;
    [SerializeField]
    private float tiltAroundY;

    public float sensitivity;
    public float rotationFactorPositiveH;
    public float rotationFactorNegativeH;
    public float rotationFactorPositiveV;
    public float rotationFactorNegativeV;
    public float movementSpeed;
    public Vector3 movementSpeedCap;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 gravity;
    public Vector3 jumpForce;
    public bool onGround;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();




    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Rotation();
        lockMouse();

        
        
    }


    IEnumerator DelayGrounded()
    {
        yield return new WaitForSeconds(0.1f); // Adjust delay as needed
        onGround = true;
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag "Floor".
        if (collision.gameObject.tag == "Floor")
        {
            StartCoroutine(DelayGrounded());
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }

    //function calculating and enforcing the velocity of the rigidbody based on inputs and characters y-rotation.
    public void Movement()
    {
        //Floats storing the calculated factor by which the vertical movement needs to be multiplied to account for character rotation.
        rotationFactorPositiveV = MathF.Cos((tiltAroundX * 3f) * MathF.PI / 180f);
        rotationFactorNegativeV = MathF.Sin((tiltAroundX * 3f) * MathF.PI / 180f);

        //Floats storing the calculated factor by which the horizontal movement needs to be multiplied to account for character rotation.
        rotationFactorPositiveH = MathF.Cos((tiltAroundX * 3f + 90f) * MathF.PI / 180f);
        rotationFactorNegativeH = MathF.Sin((tiltAroundX * 3f + 90f) * MathF.PI / 180f);

        //Floats storing the value of input based on inputs selected in input manager, can either be -1, 0 or 1.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");



        //Vector3's storing the horizontal movement and vertical movement respectfully, movement is calculated based on input, a movementSpeed and the rotationfactor.
        movementHorizontal = new Vector3((movementSpeed * horizontalInput) * rotationFactorNegativeH, 0f, (horizontalInput * movementSpeed) * rotationFactorPositiveH);
        movementVertical = new Vector3((verticalInput * movementSpeed) * rotationFactorNegativeV, 0f, (movementSpeed * verticalInput) * rotationFactorPositiveV);



        //If statements to account for moving too fast when pressing both vertical and horizontal inputs.
        if ((horizontalInput != 0) && (verticalInput == 0))
        {
            movement = movementHorizontal;
        }
        else if ((horizontalInput == 0) && (verticalInput != 0))
        {
            movement = movementVertical;
        }
        else if ((horizontalInput != 0) && (verticalInput != 0))
        {
            movement = new Vector3((movementHorizontal.x + movementVertical.x) / 1.25f, 0f, (movementHorizontal.z + movementVertical.z) / 1.25f);
        }
        else
        {
            movement = new Vector3(0f, 0f, 0f);
        }

        //Applies the velocity to the rigidbody.
        m_Rigidbody.linearVelocity = new Vector3(movement.x, m_Rigidbody.linearVelocity.y, movement.z);

        gravity = new Vector3(0f, -300f, 0f);
        m_Rigidbody.AddForce(gravity, ForceMode.Force);

        jumpForce = new Vector3(0f, 3000f, 0f);
        jumpInput = Input.GetAxisRaw("Jump");

        float jumpAirResistance = 0.5f * 1.2f * (m_Rigidbody.linearVelocity.y * m_Rigidbody.linearVelocity.y) * 2f;

        if (m_Rigidbody.linearVelocity.y < 0f)
        {
            m_Rigidbody.AddForce(new Vector3(0f, jumpAirResistance, 0f), ForceMode.Force);
        }
        else
        {
            m_Rigidbody.AddForce(new Vector3(0f, -jumpAirResistance, 0f), ForceMode.Force);
        }
        
        if (onGround && jumpInput == 1)
        {
            m_Rigidbody.AddForce(jumpForce, ForceMode.Force);
        }

        
        

    }

    //function calculating the characters y-rotation from 0-120 degrees.
    public float Rotation()
    {
        //Gets current mouse movement over x-axis, applies this to the rotation in transform and returns it aswell.
        tiltAroundX = tiltAroundX + Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(0f, tiltAroundX * sensitivity, 0f);
        return tiltAroundX;
    }


    //Returns the position of the player to the camera so that it can stay in the same position as the player.
    public Vector3 returnPos()
    {
        position = transform.position;
        return position;
    }


    //returns both the mouse x and y rotation to the player cam so that it can rotate on the x axis independantly from the player.
    public Quaternion returnRotation()
    {
        if ((tiltAroundY + Input.GetAxis("Mouse Y")) > -22 && (tiltAroundY + Input.GetAxis("Mouse Y")) < 28)
        {
            tiltAroundY = tiltAroundY + Input.GetAxis("Mouse Y");
        }
        rotation = Quaternion.Euler(-tiltAroundY * sensitivity, tiltAroundX * sensitivity, 0f);
        return rotation;
    }


    //Locks mouse to the middle of screen and makes it invisibile.
    void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
