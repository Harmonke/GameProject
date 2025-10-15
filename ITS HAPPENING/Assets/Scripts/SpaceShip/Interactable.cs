using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class Interactable : MonoBehaviour
{

    GameObject player;

    bool doorClosed;
    bool corRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        doorClosed = true;
        corRunning = false;
    }



    public IEnumerator Coroutine()
    {
        if(!corRunning)
        {
            if (doorClosed)
            {
                corRunning = true;
                float openRotation = 0f;
                float total = 0f;
                while (total > -100f)
                {
                    openRotation = -60f * Time.deltaTime;
                    transform.Rotate(0f, 0f, openRotation);
                    total += openRotation;
                    yield return null;
                }
                
                doorClosed = false;
                corRunning = false;

            }
            else if (!doorClosed)
            {
                corRunning = true;
                float openRotation = 0f;
                float total = 100f;
                while (total > 0f)
                {
                    openRotation = 60f * Time.deltaTime;
                    transform.Rotate(0f, 0f, openRotation);
                    total -= openRotation;
                    yield return null;
                }
                
                doorClosed = true;
                corRunning = false;
            }
        }
    }
    
    public void InteractBehavior()
    {
        StartCoroutine(Coroutine());
    }
}
