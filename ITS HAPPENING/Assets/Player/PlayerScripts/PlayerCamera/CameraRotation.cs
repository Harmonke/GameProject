using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{



    public GameObject player;
    private PlayerScript playerScript;
    Vector3 playerPos;
    Vector3 cameraPos;
    Vector3 newPos;
    float camMovementInterpolant = 0.03f;
    public float playerYRotation;
    public float playerXRotation;
    


    private Vector3 velocity = Vector3.zero;
    public float playerRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Player" tag and assigns it to player.
        player = GameObject.FindWithTag("Player");
        //Finds the PlayerMovement object/script within the player object.
        playerScript = player.GetComponent<PlayerScript>();
    }

    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished. 
    void LateUpdate()
    {
        applyCamPos();
        applyRotation();
    }

    // Update is called once per frame
    void Update()
    {


        PlayerYRotation();
        PlayerXRotation();

    }


    //Applies the camera position according to the player object position.
    void applyCamPos()
    {
        playerPos = playerScript.returnPos();
        cameraPos = transform.position;
        newPos = Vector3.SmoothDamp(cameraPos, playerPos, ref velocity, camMovementInterpolant);
        transform.position = newPos;
    }

    //Calculates current Y rotation.
    public void PlayerYRotation()
    {
        
        //Gets current mouse movement over x-axis, applies this to the rotation in transform and returns it aswell.
        playerYRotation = playerYRotation + Input.GetAxis("Mouse X");
    }

    //Calculates current X rotation.
    public void PlayerXRotation()
    {
        
        if ((playerXRotation + Input.GetAxis("Mouse Y")) > -70 && (playerXRotation + Input.GetAxis("Mouse Y")) < 80)
        {
        playerXRotation = playerXRotation + Input.GetAxis("Mouse Y");
        }
    }

    //Applies calculated Rotations.
    public void applyRotation()
    {
        Quaternion addXRotation = Quaternion.Euler(-playerXRotation, 0f, 0f);


        transform.rotation = Quaternion.Slerp(transform.rotation, playerScript.returnRotation() * addXRotation, 0.7f);
    }

    //Returns calculated Y rotation to be used in PlayerMovement.
    public float returnYRotation()
    {
        playerRotation = playerYRotation;
        return playerRotation;
    }

    //Returns calculated X rotation to be used in PlayerMovement.
    public float returnXRotation()
    {
        return playerXRotation;
    }

    
}
