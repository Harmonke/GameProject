using System;
using UnityEngine;

public class Planet : MonoBehaviour
{

    //Returns Planet's position.
    public Vector3 returnPosition()
    {
        return transform.position;
    }


    public float gravityConstant;
    //Returns the planet's gravity constant.
    public float returnGravityConstant()
    {

        return gravityConstant;
    }

    //Returns the Planet's radius.
    public float returnRadius()
    {
        return transform.localScale.x;
    }

    public Boolean deepSpace;

    public Boolean returnDeepSpace()
    {
        return deepSpace;
    }
}
