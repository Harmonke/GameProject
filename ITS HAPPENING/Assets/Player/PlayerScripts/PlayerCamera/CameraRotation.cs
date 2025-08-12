using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{



    public GameObject player;
    private PlayerScript playerScript;
    public float playerYRotation;
    public float playerXRotation;
    public GameObject head;
    private Transform headTransform;
    private Vector3 velocity = Vector3.zero;
    public float playerRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Player" tag and assigns it to player.
        player = GameObject.FindWithTag("Player");
        //Finds the PlayerMovement object/script within the player object.
        playerScript = player.GetComponent<PlayerScript>();

        head = GameObject.FindWithTag("Head");
        headTransform = head.GetComponent<Transform>();
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
        Vector3 target = headTransform.position;
        transform.position = Vector3.SmoothDamp(headTransform.position, target, ref velocity, 0.05f);
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
        
        
        playerXRotation = playerXRotation + Input.GetAxis("Mouse Y");
        playerXRotation = Mathf.Clamp(playerXRotation, -80f, 80f);
    }

    //Applies calculated Rotations, takes YRotation + alignedrotation from playerScript.
    public void applyRotation()
    {
        Quaternion addRotation = Quaternion.Euler(-playerXRotation, 0f, 0f);


        transform.rotation = Quaternion.Slerp(transform.rotation, playerScript.returnRotation() * addRotation, 0.7f);
    }

    //Returns calculated Y rotation to be used in PlayerScript.
    public float returnYRotation()
    {
        playerRotation = playerYRotation;
        return playerRotation;
    }

    
}
