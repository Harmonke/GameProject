using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject player;
    private PlayerInteract playerInteract;
    Gravity gravity;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInteract = player.GetComponent<PlayerInteract>();
        gravity = GetComponent<Gravity>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        applyGravity();
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 2f);
    }

    //Applies gravity.
    void applyGravity()
    {
        rb.AddForce(gravity.planetGravity(), ForceMode.Force);
    }

    public Vector3 ReturnPosition()
    {
        return transform.position;
    }

    
    

}
