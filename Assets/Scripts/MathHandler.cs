using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class MathHandler : MonoBehaviour
{
    public int expextedResult, result;
    public Camera mainCamera;
    public TextMeshProUGUI resultText;
    public GameObject unimage;
    public static MathHandler Instance;
    public float colorScale;
    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if(unimage == null)
        {
            unimage = GameObject.Find("Unimage");
        }
    }

    void Start(){
        result = 0;
    }
    void Update(){
        if(result == expextedResult){
            GameManager.Instance.EndGame();
        }
    }

    public void UpdateTheScore(){
        result = 0;
        foreach (Transform element in unimage.transform)
        {
            if(element.CompareTag("willCalculated") ){
                result += element.GetComponent<SpaceHandler>().data;
            }
        }
        resultText.text = "=   " + result.ToString();
    }

    public void UpdateTheColor(){
        colorScale = (float)result / (float)expextedResult;
        mainCamera.backgroundColor = new Color(0f,colorScale/2,0f,colorScale);
    }
}
