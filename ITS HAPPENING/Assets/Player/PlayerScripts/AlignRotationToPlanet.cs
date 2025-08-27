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

    // Update is called once per frame
    void LateUpdate()
    {
        alignRotationToPlanet();
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


    //Calculates the playerobject rotation so that its always perpendicular to the surface, also uses left-right mouserotation.
    void alignRotationToPlanet()
    {
        planetPlayerDistance = getPlanet.PlanetPlayerDistance();
        planetDirection = planetPlayerDistance.normalized;
        transformUp = transform.up;

        /*Quaternion.FromToRotation(transform.up, -planetDirection) gives the difference in rotation, so it isnt the final rotation but rather what rotation is needed to 
        turn from transform.up to -planetDirection. This needed rotation can then be applied to baseRotation to actually transform to -planetDirection. 
        (It is done this way since we cant do Quaternion.Slerp(transform.up, -planetDirection, 0.5f) since that doesnt work with 2 Vector3s)*/
        Quaternion targetRotation = Quaternion.FromToRotation(transformUp, planetDirection) * baseRotation;
        baseRotation = Quaternion.RotateTowards(baseRotation, targetRotation, 0.5f);
        newRotation = baseRotation;
    }

    //Returns newRotation to be used in PlayerScript.
    public Quaternion returnNewRotation()
    {
        return newRotation;
    }
}
