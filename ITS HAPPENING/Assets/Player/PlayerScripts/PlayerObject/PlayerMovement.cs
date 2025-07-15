using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//SOMETHING WRONG WITH WHERE SENSITIVITY IS USED
public class PlayerMovement : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    CameraRotation cameraRotation;
    public Vector3 movementHorizontal;
    public Vector3 movementVertical;
    public Vector3 movement;


    public float horizontalInput;
    float jumpInput;
    public float verticalInput;
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
    public Vector3 jumpAirResistance;
    public GameObject cam;
    private CameraRotation camRotation;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        cam = GameObject.FindWithTag("MainCamera");
        //Finds the CameraRotation object/script within the Camera object.
        camRotation = cam.GetComponent<CameraRotation>();



    }

    // FixedUpdate is called once per set amount of time (for physics related stuff like moving).
    void FixedUpdate()
    {
        Movement();
        lockMouse();
        Gravity();
        Jump();
        
    }
    void LateUpdate()
    {
        tiltAroundX = camRotation.returnRotation();

    }




    

    //function calculating and enforcing the velocity of the rigidbody based on inputs and characters y-rotation.
    public void Movement()
    {
        //Floats storing the calculated factor by which the vertical movement needs to be multiplied to account for character rotation.
        rotationFactorPositiveV = MathF.Cos(tiltAroundX * MathF.PI / 180f);
        rotationFactorNegativeV = MathF.Sin(tiltAroundX * MathF.PI / 180f);

        //Floats storing the calculated factor by which the horizontal movement needs to be multiplied to account for character rotation.
        rotationFactorPositiveH = MathF.Cos((tiltAroundX + 90f) * MathF.PI / 180f);
        rotationFactorNegativeH = MathF.Sin((tiltAroundX + 90f) * MathF.PI / 180f);

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
    }

    void Gravity()
    {
        gravity = new Vector3(0f, -200f, 0f);
        if (!CheckGrounded())
        {
             m_Rigidbody.AddForce(gravity, ForceMode.Force);
        }
           
    }

     
      
    

    IEnumerator onGroundPause()
    {
        
        yield return new WaitForSeconds(0.1f); // 100 ms delay — adjust as needed
        onGround = true;
    }

     IEnumerator JumpCooldown()
    {
        
        yield return new WaitForSeconds(0.1f); // 100 ms delay — adjust as needed
        
    }

    bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.001f))
        {
            StartCoroutine(onGroundPause());
        }
        else
        {
            onGround = false;
        }
        return onGround;
            
    }

    void Jump()
    {
        jumpForce = new Vector3(0f, 70f, 0f);
        jumpInput = Input.GetAxisRaw("Jump");

        if (CheckGrounded() && jumpInput == 1)
        {
            m_Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
            JumpCooldown();
        }
    }

    void airResistance()
    {
        jumpAirResistance = new Vector3(0f, 0.5f * 1.2f * (m_Rigidbody.linearVelocity.y * Mathf.Abs(m_Rigidbody.linearVelocity.y)), 0f);
        m_Rigidbody.AddForce(-jumpAirResistance, ForceMode.Force);
    }

    //Locks mouse to the middle of screen and makes it invisibile.
    void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 returnPos()
    {
        return new Vector3(m_Rigidbody.position.x, m_Rigidbody.position.y + 0.3f , m_Rigidbody.position.z);
    }
    

    
}
