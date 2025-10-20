using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public GameObject cam;
    private CameraRotation camRotation;
    GameObject inputObject;
    Inputs inputFile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds GameObject in unity with the "MainCamera" tag and assigns it to the cam field.
        cam = GameObject.FindWithTag("MainCamera");
        //Finds the CameraRotation object/script within the Camera object.
        camRotation = cam.GetComponent<CameraRotation>();
        inputObject = GameObject.FindWithTag("InputObject");
        inputFile = inputObject.GetComponent<Inputs>();
    }

    void Update()
    {
        interactPrompt();
        RunInteractable();
    }

    // Update is called once per frame
    GameObject interactableGameObj;
    Interactable interactableScript;
    [SerializeField] bool interactable;
    //This function handles showing an interact prompt when youre looking at an interactable object up close.
    void interactPrompt()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                interactable = true;
                interactableGameObj = hit.collider.gameObject;
                interactableScript = interactableGameObj.GetComponent<Interactable>();
            }
            else
            {
                interactable = false;
            }
        }
        else
        {
            interactable = false;
        }
    }

    public bool returnInteractable()
    {
        return interactable;
    }

    void RunInteractable()
    {
        if (interactable && inputFile.Interact())
        interactableScript.Interact();
    }

   
}
