using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining;
    public TextMeshProUGUI timeText;
    public bool timerPause = false;
    public GameObject timeIsUp;


    private void Start()
    {
        GameManager.Instance.countdownTimer = this;
    }
    void Update()
    {
        UpdateTimer();
    }
    public void UpdateTimer()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timeRemaining > 0 && !timerPause)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if(timeRemaining <= 0)
        {
            timeText.text = "00:00";
            timeIsUp.SetActive(true);
            GameManager.Instance.EndLevel();
            //Show fail pop up
        }
    }
    public void PauseTimer()
    {
        timerPause = true;
    }
    public void ResumeTimer()
    {
        timerPause = false;
    }
    
}