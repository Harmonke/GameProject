using System;
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
    


    [SerializeField] Boolean Interactable;
    //This function handles showing an interact prompt when youre looking at an interactable object up close.
    public Boolean interactPrompt()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable = true;
            }
            else
            {
                Interactable = false;
            }
        }
        else
        {
            Interactable = false;
        }
        return Interactable;
    }

   
}
