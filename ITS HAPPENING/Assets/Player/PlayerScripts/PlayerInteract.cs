using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public GameObject cam;
    private CameraRotation camRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        cam = GameObject.FindWithTag("MainCamera");
        //Finds the CameraRotation object/script within the Camera object.
        camRotation = cam.GetComponent<CameraRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        interactPrompt();
    }


     
    //This function handles showing an interact prompt when youre looking at an interactable object up close.
    void interactPrompt()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Jarvis, Jork it a little");
            }
        }

        

        
        
    }
}
