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

    Boolean leftRoll;

    public Boolean LeftRollInput()
    {
        leftRoll = Input.GetKey("q");
        return leftRoll;
    }

    Boolean rightRoll;
    public Boolean RightRollInput()
    {
        rightRoll = Input.GetKey("e");
        return rightRoll;
    }

    float moveUp;
    public float MoveUp()
    {
        moveUp = Input.GetAxisRaw("Jump");
        return moveUp;
    }

    Boolean moveDown;
    public Boolean MoveDown()
    {
        moveDown = Input.GetKey(KeyCode.LeftControl);
        return moveDown;
    }

    bool interact;
    public bool Interact()
    {
        interact = Input.GetKeyDown(KeyCode.F);
        return interact;
    }

}
