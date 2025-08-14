using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerScript : MonoBehaviour
{
    public GameObject cam;
    private CameraRotation camRotation;
    AlignRotationToPlanet alignRotation;
    PlayerMovement playerMovement;
    Gravity gravity;
    GetPlanet getPlanet;
    Rigidbody m_Rigidbody;
    [SerializeField] Quaternion baseRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        cam = GameObject.FindWithTag("MainCamera");
        //Finds the CameraRotation object/script within the Camera object.
        camRotation = cam.GetComponent<CameraRotation>();
        alignRotation = GetComponent<AlignRotationToPlanet>();
        playerMovement = GetComponent<PlayerMovement>();
        gravity = GetComponent<Gravity>();
        getPlanet = GetComponent<GetPlanet>();
        m_Rigidbody = GetComponent<Rigidbody>();
        baseRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        applyMovement();
        applyGravity();

    }

    [SerializeField] private float playerYRotation;
    private float playerXSpaceRotation;
    private float playerXRotation;

    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished.
    void LateUpdate()
    {
        playerYRotation = camRotation.returnYRotation();
        playerXSpaceRotation = camRotation.returnXSpaceRotation();
        playerXRotation = camRotation.returnPlayerXRotation();
        getNewRotation();
        applyRotation();
        //applyHeadRotation();
    }



    [SerializeField]
    Quaternion newRotation;
    Boolean deepSpace;
    [SerializeField] Quaternion yaw;
    [SerializeField] Vector3 transformUp;
    //Adds the alignedRotation and the playerYRotation together.
    void getNewRotation()
    {
        deepSpace = getPlanet.returnDeepSpace();

        if (!deepSpace)
        {
            newRotation = alignRotation.returnNewRotation() * Quaternion.Euler(0f, playerYRotation, 0f);
        }
        else if (deepSpace)
        {
            newRotation = Quaternion.AngleAxis(playerYRotation, transform.up);
        }


    }

    [SerializeField] Quaternion spaceLocalYCamRotation;
    [SerializeField] Quaternion spaceLocalXCamRotation;
    Quaternion yawQ;
    Quaternion pitchQ;
    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        if (!deepSpace)
        {
            return newRotation * Quaternion.Euler(-playerXRotation, 0f, 0f);
        }
        else
        {
            
            

            
            return newRotation * Quaternion.Euler(-playerXSpaceRotation, 0f, 0f);
        }

    }

    //Applies the calculated rotation.
    void applyRotation()
    {
        if (!deepSpace)
        {
            m_Rigidbody.MoveRotation(newRotation);
        }
        else
        {
            

            
            m_Rigidbody.MoveRotation(newRotation * Quaternion.Euler(-playerXSpaceRotation, 0f, 0f));
        }
        
    }


    [SerializeField]
    Vector3 movementForce;
    [SerializeField]
    Vector3 jumpForce;
    //Applies movement forces calculated in PlayerMovement.
    void applyMovement()
    {
        movementForce = playerMovement.returnMovementForce();
        jumpForce = playerMovement.returnJumpMovementForce();
        m_Rigidbody.AddForce(movementForce, ForceMode.Force);
        m_Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }

    //Applies gravity.
    void applyGravity()
    {
        m_Rigidbody.AddForce(gravity.returnGravity(), ForceMode.Force);
    }
}
