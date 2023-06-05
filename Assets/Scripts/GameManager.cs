using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }
    void Awake(){
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
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.Playing)
            {
                gameState = GameState.Paused;
                Time.timeScale = 0;
            }
            else if (gameState == GameState.Paused)
            {
                gameState = GameState.Playing;
                Time.timeScale = 1f;
            }
        }
    }
    public void EndGame(){
        gameState = GameState.GameOver;
        SceneManager.LoadScene("Start");
        Application.Quit();
    }

    
}