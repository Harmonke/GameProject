using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Cinemachine;


public class CameraRotation : MonoBehaviour
{



    
    [SerializeField] public float playerYRotation;
    [SerializeField] public float playerXRotation;

    public GameObject Cam;
    private CinemachinePanTilt panTilt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cam = GameObject.FindWithTag("MainCamera");
        panTilt = Cam.GetComponent<CinemachinePanTilt>();
    }

    //LateUpdate() is called once per frame, just like Update(), but after all Update() calls have finished. 
    void LateUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerYRotation();
        PlayerXRotation();
    }


    

    //Calculates current Y rotation.
    public void PlayerYRotation()
    {
        
        //Gets current mouse movement over x-axis, applies this to the rotation in transform and returns it aswell.
        playerYRotation = panTilt.PanAxis.Value;
    }

    //Calculates current X rotation.
    public void PlayerXRotation()
    {
        playerXRotation = panTilt.TiltAxis.Value;
    }

    

    //Returns calculated Y rotation to be used in PlayerMovement.
    public float returnYRotation()
    {
         
        return playerYRotation;
    }

    //Returns calculated X rotation to be used in PlayerMovement.
    public float returnXRotation()
    {
        return playerXRotation;
    }

    
}
