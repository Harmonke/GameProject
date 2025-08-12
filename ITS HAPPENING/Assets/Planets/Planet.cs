using UnityEngine;

public class Planet : MonoBehaviour
{

    //Returns Planet's position.
    public Vector3 returnPosition()
    {
        return transform.position;
    }

    //Returns the planet's gravity constant.
    public float returnGravityConstant()
    {
        float gravityConstant = 98.1f;
        return gravityConstant;
    }

    //Returns the Planet's radius.
    public float returnRadius()
    {
        return transform.localScale.x;
    }
}
