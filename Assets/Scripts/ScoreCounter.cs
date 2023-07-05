using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        GameManager.Instance.scoreCounter = this;
    }
    public void UpdateScore()
    {
        scoreText.text = string.Format("{0}", GameManager.Instance.Score);
    }
}


