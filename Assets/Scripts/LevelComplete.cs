using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelComplete : MonoBehaviour
{
    public GameObject star1, star2, star3;
    public CountdownTimer countdownTimer;

    public void GetBack()
    {
        SceneManager.LoadScene("Start");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ShowStars()
    {
        float ratio = countdownTimer.timeRemaining / countdownTimer.totalTime;
        if(ratio >= 0.5f)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            star1.transform.DOScale(1.3f, 0.8f).OnComplete(() => star2.transform.DOScale(1.3f, 0.8f).OnComplete(() => star3.transform.DOScale(1.3f, 0.8f)));
            
        }
        else if(ratio >= 0.25f)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
            star1.transform.DOScale(1.3f, 0.8f).OnComplete(() => star2.transform.DOScale(1.3f, 0.8f));
        }
        else
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
            star1.transform.DOScale(1.3f, 0.8f);
        }
    }
}
