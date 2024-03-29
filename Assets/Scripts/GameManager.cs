using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    public ManageScene manageScene;
    public MathHandler mathHandler;
    public InfiniteLevel infiniteLevel;
    public ScoreCounter scoreCounter;
    public CountdownTimer countdownTimer;
    public AudioManager audioManager;
    public int Score = 0;

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameState = GameState.Playing;
    }

    public void EndGame()
    {
        gameState = GameState.GameOver;
        Application.Quit();
    }
    public void EndLevel()
    {
        gameState = GameState.Paused;
    }
}