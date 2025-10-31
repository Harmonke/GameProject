using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    GameObject inputObject;
    Inputs inputFile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputObject = GameObject.FindWithTag("InputObject");
        inputFile = inputObject.GetComponent<Inputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
