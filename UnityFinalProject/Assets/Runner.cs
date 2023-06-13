using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Runner : MonoBehaviour
{
    float timer = 15;
    float goTimer = 3;
    float speedToSet;
    float roundTimer = 120;
    GameObject player1;
    public TMP_Text freezeTime;
    public TMP_Text roundTime;
    public Color startColor = Color.red;
    public Color middleColor = Color.yellow;
    public Color endColor = Color.green;

    void Start()
    {
        player1 = GameObject.Find("Player");
        speedToSet = player1.GetComponent<PlayerMovement>().Speed;
    }

    void Update()
    {
        if (timer > 0)
        {
            roundTime.text = "";
        }

        timer -= Time.deltaTime;
        string num;
        if (timer >= 10)
        {
            num = timer.ToString().Substring(0, 4);
        }
        else
        {
            num = timer.ToString().Substring(0, 3);
        }
        freezeTime.text = num;

        if (timer > 0)
        {
            player1.GetComponent<PlayerMovement>().setSpeed(0);
        }

        if (timer <= 0)
        {
            player1.GetComponent<PlayerMovement>().setSpeed(speedToSet);
            timer = 0;
            freezeTime.text = "GO!";
            goTimer -= Time.deltaTime;

            roundTimer -= Time.deltaTime;

            if(roundTimer <= 0)
            {
                roundTimer = 0;
            }

            if((int)roundTimer % 60 > 9)
            {
                roundTime.text = ((int)roundTimer / 60) + ":" + ((int)roundTimer % 60);

            }
            else
            {
                roundTime.text = ((int)roundTimer / 60) + ":0" + ((int)roundTimer % 60);
            }

            if (goTimer <= 0)
            {
                freezeTime.enabled = false;
            }
        }

        if(roundTimer <= 11)
        {
            roundTime.color = startColor;
        }

        // Calculate the color based on timer value
        float t = Mathf.Clamp01(1 - (timer / 15f));
        Color lerpedColor = Color.Lerp(startColor, endColor, t);

        if (t < 0.5f)
        {
            lerpedColor = Color.Lerp(startColor, middleColor, t * 2);
        }
        else
        {
            lerpedColor = Color.Lerp(middleColor, endColor, (t - 0.5f) * 2);
        }

        // Apply the color to the text
        freezeTime.color = lerpedColor;
    }
}
