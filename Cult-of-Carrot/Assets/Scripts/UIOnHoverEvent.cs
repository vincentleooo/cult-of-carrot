using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOnHoverEvent : MonoBehaviour {
//  , IPointerEnterHandler, IPointerExitHandler
    Vector3 cachedScale;

    void Start() {

        cachedScale = transform.localScale;
    }

    // public void OnPointerEnter(PointerEventData eventData) {
    //     Debug.Log("Cursor Entering " + name + " GameObject");
    //     transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    // }

    // public void OnPointerExit(PointerEventData eventData) {
    //     Debug.Log("Cursor Exiting " + name + " GameObject");
    //     transform.localScale = cachedScale;
    // }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        transform.localScale = cachedScale;
    }
 }