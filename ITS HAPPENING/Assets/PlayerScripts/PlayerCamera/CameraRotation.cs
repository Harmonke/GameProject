using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{

    

    public GameObject player;
    private PlayerMovement playerMovement;
    Vector3 playerPos;
    Vector3 cameraPos;
    Vector3 newPos;
    float camMovementInterpolant = 0.5f;

    Quaternion playerRotation;
    Quaternion camRotation;
    Quaternion newRotation;
    float camRotationInterpolant = 0.5f;

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
        Rotation();
    }

    //Uses both the cam and player rotation to linearly interpolate between the 2 and apply this new rotation .
    void Rotation()
    {
        playerRotation = playerMovement.returnRotation();
        camRotation = transform.rotation;
        newRotation = Quaternion.Lerp(camRotation, playerRotation, camRotationInterpolant);
        transform.rotation = newRotation;
    }

    //Uses both the cam and player position to linearly interpolate between the 2 and apply this new position.
    void applyCamPos()
    {
        playerPos = playerMovement.returnPos();
        cameraPos = transform.position;
        newPos = Vector3.Lerp(cameraPos, playerPos, camMovementInterpolant);
        transform.position = newPos;
    }
    
    

    
}
