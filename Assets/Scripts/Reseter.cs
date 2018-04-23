using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reseter : MonoBehaviour {
    [SerializeField]
    private Bola ball;
    [SerializeField]
    private Canvas finalScore;
    [SerializeField]
    private Text textFinalScore;
    [SerializeField]
    private Text textScore;
    [SerializeField]
    private Text remainingFinal;
    private float resetTime = 5;
    private float timeThrowed = 20;
    private bool isTriggered = false;
    [SerializeField]
    private GameObject pista;
    private Vector3 pistaSize;
    private float pinInstanceTime = 3;
    private bool magic = false;
    public Pin pinMagico;
    // Update is called once per frame

    private void Awake()
    {
        finalScore.enabled = false;
        pistaSize = pista.GetComponent<Renderer>().bounds.size;
    }

    void Update () {

        if (isTriggered)
        {
            resetTime -= Time.deltaTime;
        }

        textScore.text = "SCORE " + GameManager.score;

        if (resetTime <= 0)
        {
            if (!GameManager.IsGameOver())
            {
                ResetShoot();
            }
            else
            {
                finalScore.enabled = true;
                textFinalScore.text = "FINAL SCORE " + GameManager.score;
                remainingFinal.text = "REMAINING SHOOTS " + GameManager.remainingShoots;
            }

        }

        if (ball.IsThrowed())
            timeThrowed -= Time.deltaTime;

        if (timeThrowed < 0)
        {
            timeThrowed = 20;
            ResetShoot();
        }

        if (magic)
        {
            pinInstanceTime -= Time.deltaTime;
        }

        if (pinInstanceTime < 0)
        {
            createPin();
            pinInstanceTime = 3;
        }

	}

    private void ResetShoot()
    {
        ball.ResetBall();
        resetTime = 5;
        isTriggered = false;
    }

    public void ResetGame() {
        GameManager.ResetGame();
    }

    public void GoToMenu() {
        GameManager.ChangeSceneTo("MainMenuScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bola")
        {
            isTriggered = true;
        }
    }

    public void MagicButton()
    {
        magic = true;
    }

    public void createPin()
    {
        Vector3 pPos = Vector3.zero;
        pPos.x = UnityEngine.Random.Range(-pistaSize.x/2, pistaSize.x / 2);
        pPos.y = 1;
        pPos.z = UnityEngine.Random.Range(-pistaSize.z / 2, pistaSize.z / 2);
        Pin clon = Instantiate(pinMagico, pPos, transform.rotation);
    }
}
