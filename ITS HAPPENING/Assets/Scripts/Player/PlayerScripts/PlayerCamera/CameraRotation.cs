using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{



    public GameObject player;
    private PlayerScript playerScript;
    private Transform playerTransform;
    private Vector3 velocity = Vector3.zero;
    

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

    //Applies the position of the head object which is child to the playerobject.
    void applyPos()
    {
        Vector3 target = playerTransform.position;
        transform.position = Vector3.SmoothDamp(playerTransform.position, target, ref velocity, 0.05f);
    }

    

    //Applies calculated Rotations, takes YRotation + alignedrotation from playerScript.
    public void applyRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerScript.returnRotation(), 0.7f);
    }

    

    
}
