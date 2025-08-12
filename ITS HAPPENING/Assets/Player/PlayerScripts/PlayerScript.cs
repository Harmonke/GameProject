using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject cam;
    private CameraRotation camRotation;
    AlignRotationToPlanet alignRotation;
    PlayerMovement playerMovement;
    Gravity gravity;
    Rigidbody m_Rigidbody;
    
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
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        applyMovement();
        applyGravity();
        
    }

    private float playerYRotation;

    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished.
    void LateUpdate()
    {
        playerYRotation = camRotation.returnYRotation();
        getNewRotation();
        applyRotation();
        //applyHeadRotation();
    }

    

    [SerializeField]
    Quaternion newRotation;
    //Adds the alignedRotation and the playerYRotation together.
    void getNewRotation()
    {
        newRotation = alignRotation.returnNewRotation() * Quaternion.Euler(0f, playerYRotation, 0f);
    }

    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        return newRotation;
    }

    //Applies the calculated rotation.
    void applyRotation()
    {
        m_Rigidbody.MoveRotation(newRotation);
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
