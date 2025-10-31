using Unity.VisualScripting;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    GameObject inputObject;
    Inputs inputFile;
    PlayerState playerState;
    AlignRotationToPlanet alignRotationToPlanet;
    Rigidbody rb;

    Quaternion newRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputObject = GameObject.FindWithTag("InputObject");
        inputFile = inputObject.GetComponent<Inputs>();
        playerState = GetComponent<PlayerState>();
        alignRotationToPlanet = GetComponent<AlignRotationToPlanet>();
        rb = GetComponent<Rigidbody>();


        newRotation = Quaternion.Euler(0f, 0f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        GetRotation();
        CalculateAccumulatedRotation();
        GetNewRotation();
    }
    
    [SerializeField] private float playerYRotation;
    [SerializeField] private float playerXRotation;
    [SerializeField] float accumulatedYRot;
    void GetRotation()
    {
        playerYRotation = inputFile.PlayerYRotation() * inputFile.Sensitivity();
        playerXRotation = inputFile.PlayerXRotation() * inputFile.Sensitivity();
        
    }

    void CalculateAccumulatedRotation()
    {
        if (playerState.OnPlanet())
        {
            accumulatedYRot += playerYRotation; 
        }
        else
        {
            accumulatedYRot = 0f;
        }
    }

    //Adds the alignedRotation and the playerYRotation together.
    void GetNewRotation()
    {
        if (playerState.OnPlanet())
        {
            newRotation = alignRotationToPlanet.alignRotationToPlanet();
        }
    }

    public void ApplyRotation()
    {
        rb.MoveRotation(newRotation * Quaternion.AngleAxis(accumulatedYRot, Vector3.up));
    }
    
    float accumulatedXRot;
    
    //Returns rotation to be used in CameraRotation file.
    public Quaternion returnRotation()
    {
        if (playerState.OnPlanet())
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
