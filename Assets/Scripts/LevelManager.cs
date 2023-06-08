using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public static LevelManager Instance;

    public void Awake()
    {
        if (Instance == null) {
            Instance = this; }
    }
}