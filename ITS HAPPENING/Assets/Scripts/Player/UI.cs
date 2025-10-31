using UnityEngine;
using TMPro;
using System;

public class NewMonoBehaviourScript : MonoBehaviour
{

    PlayerInteract playerInteract;
    void Start()
    {
        playerInteract = GetComponent<PlayerInteract>();
    }

    void Update()
    {
        DisplayText();
    }

    //I havent set up how to make different keybinds yet.
    String GetInteractBind()
    {
        return "F";
    }

    public TextMeshProUGUI output;
    //Displays text on screen.
    void DisplayText()
    {
        if (playerInteract.returnInteractable())
        {
            output.text = "Interact (" + GetInteractBind() + ")";
        }
        else
        {
            output.text = "";
        }
        
    }
    
}
