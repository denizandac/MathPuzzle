using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    // All instances in 1 (in gameManager)
    // void Awake()
    // {
    //     GameManager.Instance.manageScene = this;
    // }
    public void OpenLevel(int levelName)
    {
        string levelstr = levelName.ToString();
        SceneManager.LoadScene("L " + levelstr);
        GameManager.Instance.gameState = GameManager.GameState.Playing;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
