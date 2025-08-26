using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    float i = 0f;
    // Update is called once per frame
    void Update()
    {
        
        i++;
        transform.localRotation = Quaternion.AngleAxis(i, transform.up);;
          
            
    }
}
