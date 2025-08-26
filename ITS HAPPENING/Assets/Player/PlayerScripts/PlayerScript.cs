using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public GameObject cam;
    private CameraRotation camRotation;
    AlignRotationToPlanet alignRotation;
    PlayerMovement playerMovement;
    Gravity gravity;
    GetPlanet getPlanet;
    Rigidbody m_Rigidbody;
    Transform head;
    [SerializeField] Quaternion baseRotation;

    [SerializeField]
    Quaternion newRotation;

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
        head = GetComponentInParent<Transform>();
        baseRotation = transform.rotation;
        newRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        applyMovement();
        applyGravity();
        if (deepSpace)
        {
            applyRotation();
        }
        
    }

    [SerializeField] private float playerYRotation;
    private float playerXSpaceRotation;
    private float playerYSpaceRotation;
    
    [SerializeField] private float playerXRotation;
    

    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished.
    void LateUpdate()
    {
        playerYRotation = camRotation.returnYRotation();
        playerXSpaceRotation = camRotation.returnXSpaceRotation();
        playerYSpaceRotation = camRotation.returnYSpaceRotation();
        playerXRotation = camRotation.returnPlayerXRotation();

        getNewRotation();
        if (!deepSpace)
        {
            applyRotation();
        }
        //applyHeadRotation();
    }



    
    Quaternion rotationDif;
    Boolean deepSpace;
    [SerializeField] Quaternion pitch;
    [SerializeField] Vector3 transformUp;
    //Adds the alignedRotation and the playerYRotation together.
    void getNewRotation()
    {
        deepSpace = getPlanet.returnDeepSpace();
        newRotation = alignRotation.returnNewRotation() * Quaternion.Euler(0f, playerYRotation, 0f);
    }

    [SerializeField] Quaternion spaceLocalYCamRotation;
    [SerializeField] Quaternion spaceLocalXCamRotation;
    
    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        if (!deepSpace)
        {
            return newRotation * Quaternion.Euler(-playerXRotation, 0f, 0f);
        }
        else
        {
            return transform.rotation;
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

            //TRY TO GET THIS WORKING WITH A BASE ROTATION OF SOME SORT~~~~~~~~~~~~~~
            //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * newRotation * Quaternion.AngleAxis(-playerXSpaceRotation, transform.right));
            
            m_Rigidbody.transform.localRotation *= Quaternion.AngleAxis(playerYSpaceRotation, Vector3.up);
            m_Rigidbody.transform.localRotation *= Quaternion.AngleAxis(-playerXSpaceRotation, Vector3.right) ;
            
            
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
