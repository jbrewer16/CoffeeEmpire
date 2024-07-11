using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandleCheatButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdTime = 5.0f; // Time in seconds to hold the button
    public CheatsManager cheatsManager;
    private bool isHeld = false;
    private float holdCounter = 0f;

    // Event trigger for when the button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        holdCounter = 0f;
    }

    // Event trigger for when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        holdCounter = 0f;
    }

    void Update()
    {
        if (isHeld)
        {
            holdCounter += Time.deltaTime;
            if (holdCounter >= holdTime)
            {
                PerformAction();
                isHeld = false; // Reset to prevent the action from being performed multiple times
                holdCounter = 0f;
            }
        }
    }

    private void PerformAction()
    {
        Debug.Log("Button held for " + holdTime + " seconds. Opening cheat panel!");
        cheatsManager.OpenCheatPanel();
    }
}
