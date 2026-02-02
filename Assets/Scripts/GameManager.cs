using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI colourSwapTimer;
    [SerializeField] TextMeshProUGUI score;

    public GameObject deathUI;
    bool gameActive;

    public GameObject enemyPrefab;
    public Transform[] SpawnPoints;

    ColourManager playerCM;
    bool startTimer;
    bool swapCurrentlyActive;
    float remainingTime = 5f;
    float elapsedTime = 0f;
    float currentScore;

    private void Start()
    {
        gameActive = true;
        deathUI.SetActive(false);
        playerCM = GameObject.FindGameObjectWithTag("Player").GetComponent<ColourManager>();
        colourSwapTimer.text = "";
        InvokeRepeating("addTimePoint", 0, 3);
        InvokeRepeating("enemySpawner", 1, 5);
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
        if (!gameActive)
            return;

        currentScore++;
    }

    public void addkillScore()
    {
        if (!gameActive)
            return;

        currentScore += 5;
    }

    public void gameOver()
    {
        deathUI.SetActive(true);
        gameActive = false;
    }

    public void resetGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    private void enemySpawner()
    {
        if (!gameActive)
            return;

        int randomSpawn = UnityEngine.Random.Range(0, SpawnPoints.Length);
        GameObject tempEnemy = Instantiate(enemyPrefab, SpawnPoints[randomSpawn]);
        tempEnemy.transform.localPosition = Vector3.zero;
        tempEnemy.transform.parent = null;
        tempEnemy.GetComponent<ColourManager>().setRandomColour();
    }
}
