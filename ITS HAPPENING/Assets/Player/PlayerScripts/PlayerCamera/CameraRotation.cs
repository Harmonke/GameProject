using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{



    public GameObject player;
    private PlayerScript playerScript;
    private Transform playerTransform;
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
        playerTransform = player.GetComponent<Transform>();

        
    }


    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished. 
    void LateUpdate()
    {
        applyRotation();
        applyPos();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerYRotation();
        PlayerXRotation();
    }




    //Applies the position of the head object which is child to the playerobject.
    void applyPos()
    {
        Vector3 target = playerTransform.position;
        transform.position = Vector3.SmoothDamp(playerTransform.position, target, ref velocity, 0.05f);
    }

    float playerYSpaceRotation;
    //Calculates current Y rotation.
    public void PlayerYRotation()
    {

        //Gets current mouse movement over x-axis, applies this to the rotation in transform and returns it aswell.
        playerYRotation = Mathf.Repeat(playerYRotation + Input.GetAxis("Mouse X"), 360f);
        playerYSpaceRotation = Input.GetAxis("Mouse X");
    }


    float playerXSpaceRotation;
    //Calculates current X rotation.
    public void PlayerXRotation()
    {


        playerXRotation = playerXRotation + Input.GetAxis("Mouse Y");
        playerXRotation = Mathf.Clamp(playerXRotation, -80f, 80f);

        playerXSpaceRotation = Input.GetAxis("Mouse Y");
    }

    //Applies calculated Rotations, takes YRotation + alignedrotation from playerScript.
    public void applyRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerScript.returnRotation(), 0.7f);
    }

    //Returns calculated Y rotation to be used in PlayerScript.
    public float returnYRotation()
    {
        playerRotation = playerYRotation;
        return playerRotation;
    }

    public float returnXSpaceRotation()
    {
        return playerXSpaceRotation;
    }

    public float returnYSpaceRotation()
    {
        return playerYSpaceRotation;
    }

    public float returnPlayerXRotation()
    {
        return playerXRotation;
    }

    
}
