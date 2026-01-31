using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI colourSwapTimer;
    [SerializeField] TextMeshProUGUI score;

    ColourManager playerCM;
    bool startTimer;
    bool swapCurrentlyActive;
    float remainingTime = 5f;
    float elapsedTime = 0f;
    float currentScore;

    private void Start()
    {
        playerCM = GameObject.FindGameObjectWithTag("Player").GetComponent<ColourManager>();
        colourSwapTimer.text = "";
        InvokeRepeating("addTimePoint", 0, 1);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime % 15 <= 1 && !swapCurrentlyActive)
        {
            swapCurrentlyActive = true;
            StartCoroutine(colourSwapCountdown());
        }

        if (startTimer)
        {
            remainingTime -= Time.deltaTime;
            colourSwapTimer.text = string.Format("NEXT COLOUR IN: {0:00}", remainingTime);
        }
        else
        {
            colourSwapTimer.text = "";
        }

        updateUI();

    }

    private IEnumerator colourSwapCountdown()
    {
        startTimer = true;
        remainingTime = 5f;
        yield return new WaitForSeconds(5f);
        playerCM.swapColour();
        startTimer = false;
        swapCurrentlyActive = false;
    }

    private void updateUI()
    {
        score.text = "Score: " + currentScore;
    }

    void addTimePoint()
    {
        currentScore++;
    }
}
