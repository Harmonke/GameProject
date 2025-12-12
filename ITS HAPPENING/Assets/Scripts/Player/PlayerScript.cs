using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    
    
    
    PlayerMovement playerMovement;
    Gravity gravity;
    PlayerState playerState;
    PlayerRotation playerRotation;
    
    [SerializeField] GameObject spaceShipCockpit;
    SpaceShipInteract spaceShipInteract;
    SpaceMovement spaceMovement;



    

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.

        //Finds the CameraRotation object/script within the Camera object.
        playerState = GetComponent<PlayerState>();
        playerMovement = GetComponent<PlayerMovement>();
        gravity = GetComponent<Gravity>();
        playerRotation = GetComponent<PlayerRotation>();
        
        
        spaceShipInteract = spaceShipCockpit.GetComponent<SpaceShipInteract>();
        spaceMovement = GetComponent<SpaceMovement>();
        

        

    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spaceShipInteract.returnPlayerSitting())
        {
            
            if (playerState.OnPlanet())
            {
                applyMovement();
                //playerMovement.ApplyPlanetPosition();
            }
            applyMovement();
            
            gravity.ApplyPlanetGravity();
            
            
            
        }
    }

    void LateUpdate()
    {
        if (!spaceShipInteract.returnPlayerSitting())
        {
            ApplyRotation();
        }
    }

    
    //Applies the calculated rotation.
    void ApplyRotation()
    {
        if (playerState.OnPlanet())
        {
            playerRotation.ApplyRotation();
        }
        else
        {
            spaceMovement.ApplySpaceRotation();
        }
    }


    //Applies movement forces calculated in PlayerMovement.
    void applyMovement()
    {
        if (playerState.OnPlanet())
        {
            //playerMovement.ApplyPlanetVelocity();
            playerMovement.ApplyMovement();
            playerMovement.SetLinearDamping();
        }
        else
        {
            spaceMovement.ApplySpaceMovement();
            spaceMovement.SetLinearDamping();
        }
    }

}
