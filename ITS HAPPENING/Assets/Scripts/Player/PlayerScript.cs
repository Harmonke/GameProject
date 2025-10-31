using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    
    
    AlignRotationToPlanet alignRotation;
    PlayerMovement playerMovement;
    Gravity gravity;
    GetPlanet getPlanet;
    Rigidbody m_Rigidbody;
    
    
    [SerializeField] GameObject spaceShipCockpit;
    SpaceShipInteract spaceShipInteract;
    SpaceMovement spaceMovement;



    

    

    [SerializeField]
    Quaternion newRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        
        //Finds the CameraRotation object/script within the Camera object.
        alignRotation = GetComponent<AlignRotationToPlanet>();
        playerMovement = GetComponent<PlayerMovement>();
        gravity = GetComponent<Gravity>();
        getPlanet = GetComponent<GetPlanet>();
        m_Rigidbody = GetComponent<Rigidbody>();
        
        newRotation = Quaternion.Euler(0f, 0f, 0f);
        spaceShipInteract = spaceShipCockpit.GetComponent<SpaceShipInteract>();
        spaceMovement = GetComponent<SpaceMovement>();
        

        

    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spaceShipInteract.returnPlayerSitting())
        {
            applyMovement();
            gravity.ApplyPlanetGravity();
        }
    }

    void Update()
    {
        if (!spaceShipInteract.returnPlayerSitting())
        {
            GetNewRotation();
        
            GetRotation();
            
            ApplyRotation();
        }
    }

    [SerializeField] private float playerYRotation;
    [SerializeField] private float playerXRotation;
    [SerializeField] float accumulatedYRot;
    void GetRotation()
    {
        playerYRotation = inputFile.PlayerYRotation() * inputFile.Sensitivity();
        playerXRotation = inputFile.PlayerXRotation() * inputFile.Sensitivity();
        accumulatedYRot += playerYRotation;
    }

    

    
    
    //Adds the alignedRotation and the playerYRotation together.
    void GetNewRotation()
    {
        
        if (!GetDeepSpaceValue() && GetFirstContactValue())
        {
            newRotation = alignRotation.alignRotationToPlanet();
        }
        else
        {
            accumulatedYRot = 0f;
        }
    }

    

    
    
    //Gets the values that determines if player should be aligned.
    bool GetDeepSpaceValue()
    {
        return getPlanet.returnDeepSpace();
    }

    bool GetFirstContactValue()
    {
        return getPlanet.returnFirstContact();
    }
    
    
    //Applies the calculated rotation.
    void ApplyRotation()
    {
        if (!GetDeepSpaceValue() && GetFirstContactValue())
        {
            m_Rigidbody.MoveRotation(newRotation * Quaternion.AngleAxis( accumulatedYRot, Vector3.up) );
        }
        else
        {
            spaceMovement.ApplySpaceRotation();
        }
    }



    
    
    
    //Applies movement forces calculated in PlayerMovement.
    void applyMovement()
    {
        if (!GetDeepSpaceValue() && GetFirstContactValue())
        {
            playerMovement.applyMovement();
        }
        else if (!GetFirstContactValue())
        {
            spaceMovement.ApplySpaceMovement();
        }
    }
    
    float accumulatedXRot;
    
    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        if (!GetDeepSpaceValue() && GetFirstContactValue())
        {
            accumulatedXRot += -playerXRotation;
            accumulatedXRot = Mathf.Clamp(accumulatedXRot, -80f, 80f);
            Quaternion planetXRotation = Quaternion.Euler(accumulatedXRot, 0f, 0f);
            return transform.rotation * planetXRotation;
        }
        else
        {
            accumulatedXRot = 0f;
            return transform.rotation;
        }

    }

    
}
