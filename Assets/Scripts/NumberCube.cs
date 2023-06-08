using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberCube : MonoBehaviour
{
    [SerializeField] private TextMeshPro _numberText;
    [SerializeField] private GameObject _cube;
    [SerializeField] private int _cubeValue;

    private void Start()
    {
        //_cubeValue = Random.Range(1, 10);
        _numberText.text = _cubeValue.ToString();
    }

    public GameObject GetCubeObject()
    {
        return _cube;
    }
    
    public void SetCubeValue(int value)
    {
        _cubeValue = value;
        _numberText.text = _cubeValue.ToString();
    }
    public int GetCubeValue()
    {
        return _cubeValue;
    }
}
