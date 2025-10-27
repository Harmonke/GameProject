using System;
using UnityEngine;

public class Inputs : MonoBehaviour
{

    float playerYRotation;
    public float PlayerYRotation()
    {
        //Gets current mouse movement over x-axis, applies this to the rotation in transform and returns it aswell.
        playerYRotation = Input.GetAxis("Mouse X");
        return playerYRotation;
    }

    float playerXRotation;
    //Calculates current X rotation.
    public float PlayerXRotation()
    {
        playerXRotation = Input.GetAxis("Mouse Y");
        playerXRotation = Mathf.Clamp(playerXRotation, -80f, 80f);

        return playerXRotation;
    }

    float horizontalInput;
    public float HorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        return horizontalInput;
    }

    float verticalInput;
    public float VerticalInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        return verticalInput;
    }

    

    public int LeftRollInput()
    {
        bool leftRoll = Input.GetKey("q");
        if (leftRoll)
        {
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    
    public int RightRollInput()
    {
        bool rightRoll = Input.GetKey("e");
        if (rightRoll)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    float moveUp;
    public float MoveUp()
    {
        moveUp = Input.GetAxisRaw("Jump");
        return moveUp;
    }

    
    public int MoveDown()
    {
        bool moveDown = Input.GetKey(KeyCode.LeftControl);
        if (moveDown)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    bool interact;
    public bool Interact()
    {
        interact = Input.GetKeyDown(KeyCode.F);
        return interact;
    }

    public bool StopInteract()
    {
        bool stopInteract = Input.GetKeyDown(KeyCode.LeftShift);
        return stopInteract;
    }

    [SerializeField] float sensitivity;
    public float Sensitivity()
    {
        return sensitivity;
    }
}
