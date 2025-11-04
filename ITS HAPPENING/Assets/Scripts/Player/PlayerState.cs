using UnityEngine;

public class PlayerState : MonoBehaviour
{
    GetPlanet getPlanet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getPlanet = GetComponent<GetPlanet>();
    }

    
    public bool OnPlanet()
    {
        if (!getPlanet.returnDeepSpace() && getPlanet.returnFirstContact())
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool InAtmosphere()
    {
        if (!getPlanet.returnDeepSpace() && getPlanet.returnFirstContact())
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool InSpace()
    {
        if (getPlanet.returnDeepSpace() && getPlanet.returnFirstContact())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
