using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{



    public GameObject player;
    private PlayerRotation playerRotation;
    private Transform playerTransform;
    private Vector3 velocity = Vector3.zero;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Player" tag and assigns it to player.
        player = GameObject.FindWithTag("Player");
        //Finds the PlayerMovement object/script within the player object.
        playerRotation = player.GetComponent<PlayerRotation>();
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
        Vector3 target = playerTransform.TransformPoint(new Vector3(0f, 0.3f, 0f));
        transform.position = Vector3.SmoothDamp(playerTransform.TransformPoint(new Vector3(0f, 0.3f, 0f)), target, ref velocity, 0.05f);
    }

    

    //Applies calculated Rotations, takes YRotation + alignedrotation from playerScript.
    public void applyRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation.returnRotation(), 0.7f);
    }

    

    
}
