using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceShipInteract : Interactable
{
    GameObject input;
    Inputs inputScript;

    GameObject player;
    Transform playerTransform;
    bool playerSitting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GameObject.FindWithTag("InputObject");
        inputScript = input.GetComponent<Inputs>();
        player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerSitting = false;
    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator PlayerSit()
    {
        while (!inputScript.StopInteract())
        {
            playerSitting = true;
            playerTransform.position = transform.position;
            playerTransform.rotation = transform.rotation;
            yield return null;
        }
        playerSitting = false;
    }
    
    public bool returnPlayerSitting()
    {
        return playerSitting;
    }
    
    public override void Interact()
    {
        StartCoroutine(PlayerSit());
    }

}
