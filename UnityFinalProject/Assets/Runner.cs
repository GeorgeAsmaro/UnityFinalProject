using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    float timer = 15;
    float speedToSet;
    GameObject player1;

    void Start()
    {
        player1 = GameObject.Find("Player");
        speedToSet = player1.GetComponent<PlayerMovement>().Speed;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            player1.GetComponent<PlayerMovement>().setSpeed(0);
        }

        if (timer < 0)
        {
            player1.GetComponent<PlayerMovement>().setSpeed(speedToSet);
        }
    }
}

