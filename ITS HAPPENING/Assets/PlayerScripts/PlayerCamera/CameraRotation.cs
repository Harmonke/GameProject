using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{

    

    public GameObject player;
    private PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "Player" tag and assigns it to player.
        player = GameObject.FindWithTag("Player");
        //Finds the PlayerMovement object/script within the player object.
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        applyCamPos();
        Rotation();
    }
    
    //Applies the cam rotation calculated in PlayerMovement to the camera.
    void Rotation()
    {
        transform.rotation = playerMovement.returnRotation();
    }

    //Applies the cam position calculated in PlayerMovement to the camera.
    void applyCamPos()
    {
        transform.position = playerMovement.returnPos();
    }
    
    

    
}
