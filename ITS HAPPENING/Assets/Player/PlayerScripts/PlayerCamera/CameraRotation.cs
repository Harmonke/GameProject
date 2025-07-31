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
    public Gravity gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cam = GameObject.FindWithTag("MainCamera");
        panTilt = Cam.GetComponent<CinemachinePanTilt>();
        gravity = GetComponent<Gravity>();
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

    public void freeLook()
    {
        panTilt.TiltAxis.Range = new Vector2(-180f, 180f);
        panTilt.TiltAxis.Wrap = true;
    }

    public void defaultLook()
    {
        panTilt.TiltAxis.Range = new Vector2(-70f, 70f);
        panTilt.TiltAxis.Wrap = false;
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
