using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource swapSound, cantDragSound, winSound, loseSound;
    void Start()
    {
        GameManager.Instance.audioManager = this;
    }
    public void PlaySwapSound()
    {
        swapSound.Play();
    }
    public void PlayCantDragSound()
    {
        cantDragSound.Play();
    }
    public void PlayWinSound()
    {
        winSound.Play();
    }
    public void PlayLoseSound()
    {
        loseSound.Play();
    }

}
