using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSetActive : MonoBehaviour
{

    public bool ActivateOnStart = true;
    public float ActivationDelay = 5.0f;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
        if (ActivateOnStart) ActivateDelayed();
    }

    private void AfterDelay()
    {
        gameObject.SetActive(true);
    }

    /*
     * Activates the object after the configured delay
     */
    public void ActivateDelayed()
    {
        Invoke(nameof(AfterDelay), ActivationDelay);
    }

    /*
     * Activates the object after the passed custom delay
     */
    public void ActivateDelayed(float customDelay)
    {
        Invoke(nameof(AfterDelay), customDelay);
    }
    // void Start ()
    // {
    //     Debug.Log("Active Self: " + myObject.activeSelf);
    //     myObject.SetActive(true);
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
