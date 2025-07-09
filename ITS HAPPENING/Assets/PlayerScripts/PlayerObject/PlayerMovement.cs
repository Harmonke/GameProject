using System;
using UnityEditor;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    CameraRotation cameraRotation;
    public Vector3 movementHorizontal;
    public Vector3 movementVertical;
    public Vector3 movement;

    float horizontalInput;
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
    public Vector3 position;
    public Quaternion rotation;

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

    //function calculating and enforcing the force to be added on the rigidbody based on inputs and characters y-rotation.
    public void Movement()
    {
        //Floats storing the calculated factor by which the vertical movement needs to be multiplied to account for character rotation.
        rotationFactorPositiveV = MathF.Cos((tiltAroundX * 3f) * MathF.PI / 180f);
        rotationFactorNegativeV = MathF.Sin((tiltAroundX * 3f) * MathF.PI / 180f);

        //Floats storing the calculated factor by which the horizontal movement needs to be multiplied to account for character rotation.
        rotationFactorPositiveH = MathF.Cos((tiltAroundX * 3f + 90f) * MathF.PI / 180f);
        rotationFactorNegativeH = MathF.Sin((tiltAroundX * 3f + 90f) * MathF.PI / 180f);

        //Floats storing the value of input based on inputs selected in input manager, can either be -1, 0 or 1.
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Vector3's storing the horizontal movement and vertical movement respectfully, movement is calculated based on input, a movementSpeed and the rotationfactor.
        movementHorizontal = new Vector3((movementSpeed * horizontalInput) * rotationFactorNegativeH, 0f, (horizontalInput * movementSpeed) * rotationFactorPositiveH);
        movementVertical = new Vector3((verticalInput * movementSpeed) * rotationFactorNegativeV, 0f, (movementSpeed * verticalInput) * rotationFactorPositiveV);
        movement = movementHorizontal + movementVertical;

        m_Rigidbody.AddForce(movement, ForceMode.Force);
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
