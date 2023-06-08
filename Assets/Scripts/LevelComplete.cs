using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelComplete : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelComplete instance;


    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    public void GetBack()
    {
        SceneManager.LoadScene("Start");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.Instance.level++;
    }
}
