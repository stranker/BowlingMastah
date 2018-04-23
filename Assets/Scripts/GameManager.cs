using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private static int amountOfShoots;
    public static int remainingShoots;
    [SerializeField]
    public static bool gameOver;
    public Bola ball;
    public static List<Pin> pins = new List<Pin>();
    public static int score = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    internal static void Initialize()
    {
        amountOfShoots = 3;
        remainingShoots = amountOfShoots;
        gameOver = false;
        score = 0;
    }

    // Update is called once per frame
    void Update () {
        if (remainingShoots == 0 || getActivePins() == 0)
        {
            gameOver = true;
        }
            
	}

    private int getActivePins()
    {
        int count = 0;
        if(pins.Count > 0)
            foreach (Pin pin in pins)
                if (!pin.IsOnFloor())
                    count++;
        return count;
    }

    internal static void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ResetGame()
    {
        Initialize();
        ChangeSceneTo("MainScene");
    }

    public static bool IsGameOver()
    {
        return gameOver;
    }

    public void ExitApp()
    {
        ExitApp();
    }

    internal static List<Pin> GetPins()
    {
        return pins;
    }

    public static int RemainingShoots()
    {
        return remainingShoots;
    }

    public static int AmountOfShoots()
    {
        return amountOfShoots;
    }
}
