using UnityEngine;

public class AlignRotationToPlanet : MonoBehaviour
{

    public Quaternion baseRotation;
    GetPlanet getPlanet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseRotation = transform.rotation;
        getPlanet = GetComponent<GetPlanet>();
    }

    void Update()
    {
        resetBaseRotation();
    }

    

    void resetBaseRotation()
    {
        if (getPlanet.returnResetBaseRotation())
        {
            baseRotation = transform.rotation;

        }
    }

    public Vector3 planetDirection;
    Quaternion newRotation;
    public Vector3 transformUp;
    Vector3 planetPlayerDistance;
    Quaternion targetRotation;


    //Calculates the playerobject rotation so that its always perpendicular to the surface, also uses left-right mouserotation.
    public Quaternion alignRotationToPlanet()
    {
        planetPlayerDistance = getPlanet.CorePlayerDistance();
        planetDirection = planetPlayerDistance.normalized;
        transformUp = transform.up;

        /*Quaternion.FromToRotation(transform.up, -planetDirection) gives the difference in rotation, so it isnt the final rotation but rather what rotation is needed to 
        turn from transform.up to -planetDirection. This needed rotation can then be applied to baseRotation to actually transform to -planetDirection. 
        (It is done this way since we cant do Quaternion.Slerp(transform.up, -planetDirection, 0.5f) since that doesnt work with 2 Vector3s)*/
        targetRotation = Quaternion.FromToRotation(transformUp, planetDirection) * baseRotation;
        baseRotation = Quaternion.RotateTowards(baseRotation, targetRotation, 0.5f);
        newRotation = Quaternion.Normalize(baseRotation);
        return newRotation;
    }



    //Returns newRotation to be used in PlayerScript.
    public Quaternion returnNewRotation()
    {
        return newRotation;
    }

    public Quaternion ReturnTargetRotation()
    {
        return targetRotation;
    }

    public Vector3 ReturnPlanetDirection()
    {
        return planetDirection;
    }
}
