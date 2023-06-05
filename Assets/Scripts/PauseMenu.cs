using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject backgroundPanel;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);

        Color backgroundColor = backgroundPanel.GetComponent<UnityEngine.UI.Image>().color;
        backgroundColor.a = 0.5f;
        backgroundPanel.GetComponent<UnityEngine.UI.Image>().color = backgroundColor;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);

        Color backgroundColor = backgroundPanel.GetComponent<UnityEngine.UI.Image>().color;
        backgroundColor.a = 0f;
        backgroundPanel.GetComponent<UnityEngine.UI.Image>().color = backgroundColor;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ClosePauseMenu()
    {
        Resume();
    }
}