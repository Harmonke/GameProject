using UnityEngine;

public class Planet : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns Planet's position.
    public Vector3 returnPosition()
    {
        return transform.position;
    }

    [SerializeField] float gravityConstant;
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

    [SerializeField] bool deepSpace;

    public bool returnDeepSpace()
    {
        return deepSpace;
    }
}
