using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    CameraRotation camRotation;
    AlignRotationToPlanet alignRotation;
    PlayerMovement playerMovement;
    Gravity gravity;
    Rigidbody m_Rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        
        //Finds the CameraRotation object/script within the Camera object.
        camRotation = GetComponent<CameraRotation>();
        alignRotation = GetComponent<AlignRotationToPlanet>();
        playerMovement = GetComponent<PlayerMovement>();
        gravity = GetComponent<Gravity>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        applyMovement();
        getNewRotation();
        applyRotation();
        applyGravity();
    }



    [SerializeField] private float playerYRotation;
    [SerializeField] private float playerXRotation;

    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished.
    void LateUpdate()
    {
        playerYRotation = camRotation.returnYRotation();
        playerXRotation = camRotation.returnXRotation();
        
    }

    
    [SerializeField] Quaternion newPlanetRotation;
    [SerializeField] Quaternion newSpaceRotation;
    void getNewRotation()
    {

        newSpaceRotation = Quaternion.Euler(0f, playerYRotation, 0f);
        newPlanetRotation = alignRotation.returnNewRotation() * Quaternion.Euler(0f, playerYRotation, 0f);
        
        
    }

    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        return newPlanetRotation;
    }

    //Applies the calculated rotation.
    void applyRotation()
    {
        if (gravity.returnDeepSpace())
        {
            camRotation.freeLook();
            transform.rotation = newSpaceRotation;
        }
        else if (!gravity.returnDeepSpace())
        {
            camRotation.defaultLook();
            transform.rotation = newPlanetRotation;
        }
        
    }

    //Returns playerposition to be used in CameraRotation.
    public Vector3 returnPos()
    {
        return new Vector3(m_Rigidbody.position.x, m_Rigidbody.position.y + 0.3f, m_Rigidbody.position.z);
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

    void applyGravity()
    {
        m_Rigidbody.AddForce(gravity.returnGravity(), ForceMode.Force);
    }
}
