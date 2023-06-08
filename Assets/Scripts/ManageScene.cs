using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public static ManageScene _instance;
    void Awake(){
        if(_instance == null){
            _instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Update is called once per frame

    public void OpenLevel(int levelName){
        string levelstr = levelName.ToString();
        SceneManager.LoadScene("lvl"+levelstr);
        LevelManager.Instance.level = levelName;
        GameManager.Instance.gameState = GameManager.GameState.Playing;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
