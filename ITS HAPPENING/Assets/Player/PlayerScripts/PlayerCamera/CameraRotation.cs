using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{



    public GameObject player;
    private PlayerMovement playerMovement;
    Vector3 playerPos;
    Vector3 cameraPos;
    Vector3 newPos;
    float camMovementInterpolant = 0.030f;
    float tiltAroundX;
    float tiltAroundY;
    Quaternion newRotation;

    
    private Vector3 velocity = Vector3.zero;
    public float playerRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Player" tag and assigns it to player.
        player = GameObject.FindWithTag("Player");
        //Finds the PlayerMovement object/script within the player object.
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        applyCamPos(); 
        applyRotation();
    }


    void Update()
    {
        XMouseRotation();
        YMouseRotation();
        
    }

    

    void applyCamPos()
    {
        playerPos = playerMovement.returnPos();
        cameraPos = transform.position;
        newPos = Vector3.SmoothDamp(cameraPos, playerPos, ref velocity, camMovementInterpolant);
        transform.position = newPos;
    }

    public void XMouseRotation()
    {
        //Gets current mouse movement over x-axis, applies this to the rotation in transform and returns it aswell.
        tiltAroundX = tiltAroundX + Input.GetAxis("Mouse X");
    }


    public void YMouseRotation()
    {
        if ((tiltAroundY + Input.GetAxis("Mouse Y")) > -70 && (tiltAroundY + Input.GetAxis("Mouse Y")) < 80)
        {
            tiltAroundY = tiltAroundY + Input.GetAxis("Mouse Y");
        }
    }

    public void applyRotation()
    {
        newRotation = Quaternion.Euler(-tiltAroundY, tiltAroundX, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.7f);
    }
    
    public float returnRotation()
    {
        playerRotation = tiltAroundX;
        return playerRotation;
    }

    
}
