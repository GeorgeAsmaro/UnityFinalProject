using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlant : MonoBehaviour
{
    // Start is called before the first frame update
    bool isTSide = true;
    bool hasBomb = true;
    public GameObject objectToInstantiate;
    public GameObject Runner;
    public Transform spawnPoint;
    public float holdTime = 3.5f;
    private float heldDuration = 0f;

    void Update()
    {
        if (isTSide && hasBomb)
        {
            if (Input.GetKey(KeyCode.E))
            {
                heldDuration += Time.deltaTime;
                if (heldDuration >= holdTime)
                {
                    Instantiate(objectToInstantiate, spawnPoint.position, spawnPoint.rotation);
                    heldDuration = 0f;
                    Runner.GetComponent<Runner>().setPlanted(true);
                    hasBomb = false;
                }
            }
            else
            {
                heldDuration = 0f;
            }
        }
    }
}
