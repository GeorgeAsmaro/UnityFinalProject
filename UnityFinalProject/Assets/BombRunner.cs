using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRunner : MonoBehaviour
{
    public GameObject targetObject;
    public float interactionTime = 5f;

    private bool isLookingAtObject;
    private float interactionTimer;
    public GameObject Runner;
    void Update()
    {
        // Check if the player is looking at the target object
        targetObject = GameObject.Find("SM_Prop_C4_01 Variant 1(Clone)");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == targetObject)
            {
                isLookingAtObject = true;
            }
            else
            {
                isLookingAtObject = false;
            }
        }
        else
        {
            isLookingAtObject = false;
        }

        // Handle interaction input
        if (isLookingAtObject && Input.GetKey(KeyCode.E))
        {
            interactionTimer += Time.deltaTime;

            if (interactionTimer >= interactionTime)
            {
                // Perform the desired action when the interaction time is reached
                PerformInteraction();
            }
        }
        else
        {
            interactionTimer = 0f;
        }
    }

    void PerformInteraction()
    {
        // Do something when the interaction time is reached
        Runner.GetComponent<Runner>().setDefused(true);
    }
}
