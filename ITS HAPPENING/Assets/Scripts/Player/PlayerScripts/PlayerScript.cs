using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public GameObject cam;
    
    AlignRotationToPlanet alignRotation;
    PlayerMovement playerMovement;
    Gravity gravity;
    GetPlanet getPlanet;
    Rigidbody m_Rigidbody;
    
    GameObject inputObject;
    Inputs inputFile;
    [SerializeField] GameObject spaceShipCockpit;
    SpaceShipInteract spaceShipInteract;


    

    

    [SerializeField]
    Quaternion newRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        cam = GameObject.FindWithTag("MainCamera");
        //Finds the CameraRotation object/script within the Camera object.
        alignRotation = GetComponent<AlignRotationToPlanet>();
        playerMovement = GetComponent<PlayerMovement>();
        gravity = GetComponent<Gravity>();
        getPlanet = GetComponent<GetPlanet>();
        m_Rigidbody = GetComponent<Rigidbody>();
        inputObject = GameObject.FindWithTag("InputObject");
        inputFile = inputObject.GetComponent<Inputs>();
        newRotation = Quaternion.Euler(0f, 0f, 0f);
        spaceShipInteract = spaceShipCockpit.GetComponent<SpaceShipInteract>();
        

        

    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spaceShipInteract.returnPlayerSitting())
        {
            applyMovement();
            applyGravity();
        }
        
        
        
        
        
        
    }

    void Update()
    {
        if (!spaceShipInteract.returnPlayerSitting())
        {
            verticalDeepSpaceMovement();
            GetAlignment();
            GetNewRotation();
        
            GetRotation();
            getAlignModeValues();
            ApplyRotation();
        }
    }

    [SerializeField] private float playerYRotation;
    
    
    [SerializeField] private float playerXRotation;


    

    void GetRotation()
    {
        playerYRotation = inputFile.PlayerYRotation() * inputFile.Sensitivity();
        playerXRotation = inputFile.PlayerXRotation() * inputFile.Sensitivity();
        accumulatedYRot += playerYRotation;
    }

    Quaternion alignment;
    void GetAlignment()
    {
        alignment = alignRotation.alignRotationToPlanet();
    }

    [SerializeField] float accumulatedYRot;
    
    //Adds the alignedRotation and the playerYRotation together.
    void GetNewRotation()
    {
        
        if (!deepSpace && firstContact)
        {
            newRotation = alignment;
        }
        else
        {
            accumulatedYRot = 0f;
        }
    }

    

    Boolean deepSpace;
    [SerializeField] Boolean firstContact;
    //Gets the values that determines if player should be aligned.
    void getAlignModeValues()
    {
        deepSpace = getPlanet.returnDeepSpace();
        firstContact = getPlanet.returnFirstContact();
    }
    
    float accumulatedXRot;
    Quaternion planetXRotation;
    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        if (!deepSpace && firstContact)
        {
            accumulatedXRot += -playerXRotation;
            accumulatedXRot = Mathf.Clamp(accumulatedXRot, -80f, 80f);
            planetXRotation = Quaternion.Euler(accumulatedXRot, 0f, 0f);
            return transform.rotation * planetXRotation;
        }
        else
        {
            accumulatedXRot = 0f;
            return transform.rotation;
        }

    }

   
    
    //Applies the calculated rotation.
    void ApplyRotation()
    {
        if (!deepSpace && firstContact)
        {
            m_Rigidbody.MoveRotation(newRotation * Quaternion.AngleAxis( accumulatedYRot, Vector3.up) );
        }
        else
        {

            Boolean leftRoll = inputFile.LeftRollInput();
            Boolean rightRoll = inputFile.RightRollInput();

            m_Rigidbody.transform.localRotation *= Quaternion.AngleAxis(playerYRotation, Vector3.up);
            m_Rigidbody.transform.localRotation *= Quaternion.AngleAxis(-playerXRotation, Vector3.right);

            if (leftRoll)
            {
                m_Rigidbody.transform.localRotation *= Quaternion.AngleAxis(60f * Time.deltaTime, Vector3.forward);
            }

            if (rightRoll)
            {
                m_Rigidbody.transform.localRotation *= Quaternion.AngleAxis(-60f * Time.deltaTime, Vector3.forward);
            }
            
            
        }
        
    }

    float moveUp;
    Boolean moveDown;
    void verticalDeepSpaceMovement()
    {
        moveUp = inputFile.MoveUp();
        moveDown = inputFile.MoveDown();
    }

    [SerializeField]
    Vector3 movementForce;
    [SerializeField]
    Vector3 jumpForce;
    [SerializeField] Vector3 inputDirection;
    Vector3 spaceMovementForce;
    float moveDownFloat;

    //Applies movement forces calculated in PlayerMovement.
    void applyMovement()
    {
        Vector3 spaceVerticalMovement = moveUp * transform.up + moveDownFloat * transform.up;
        movementForce = playerMovement.Movement();
        if (!deepSpace && firstContact)
        {
            spaceMovementForce = new Vector3(0f, 0f, 0f);
            jumpForce = playerMovement.returnJumpMovementForce();
            m_Rigidbody.AddForce(movementForce, ForceMode.Force);
            m_Rigidbody.AddForce(jumpForce, ForceMode.Impulse);



        }
        else if (!firstContact)
        {

            if (moveDown)
            {
                moveDownFloat = -1;
            }
            else
            {
                moveDownFloat = 0;
            }

            
            spaceMovementForce += playerMovement.Movement() * 0.1f + spaceVerticalMovement * 10f;
            m_Rigidbody.AddForce(spaceMovementForce, ForceMode.Force);

        }
        // else if (!firstContact && !deepSpace)
        // {
        //     spaceMovementForce = playerMovement.Movement() * 0.1f + spaceVerticalMovement * 10f;
        //     m_Rigidbody.AddForce(spaceMovementForce, ForceMode.Force);
        // }

        
    }

    //Applies gravity.
    void applyGravity()
    {
        m_Rigidbody.AddForce(gravity.planetGravity(), ForceMode.Force);
    }
}
