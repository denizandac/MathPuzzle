using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class MathHandler : MonoBehaviour
{
    public static MathHandler Instance;
    public int expextedResult, result;
    public Camera mainCamera;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI expectedText;
    public GameObject unimage;
    public float colorScale;
    public string[] calculationArray;
    public List<GameObject> boxList = new List<GameObject>();
    public List<BoxHandler> spaceList = new List<BoxHandler>();
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
        // array = RemoveNulls(array);
        // result = CalculateResult(array);
        // resultText.text = "=   " + result.ToString();
        expectedText.text = expextedResult.ToString();
    }
    void Update(){      
        if(result == expextedResult){
            GameManager.Instance.EndGame();
        }
        UpdateTheScore();
        UpdateTheColor();
    }

    public void SwapBoxes(){
        boxList.Clear();
        foreach (Transform child in unimage.transform)
        {
            if(child.gameObject.CompareTag("Box")){
                boxList.Add(child.gameObject);
            }
        }
        for (int i = 0; i < boxList.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, boxList.Count);
            Vector3 tempPosition = boxList[i].transform.position;
            Vector3 tempInitialPosition = boxList[i].GetComponent<BoxHandler>()._initialPosition;
            boxList[i].transform.position = boxList[randomIndex].transform.position;
            boxList[randomIndex].transform.position = tempPosition;
            boxList[i].GetComponent<BoxHandler>()._initialPosition = boxList[randomIndex].GetComponent<BoxHandler>()._initialPosition;
            boxList[randomIndex].GetComponent<BoxHandler>()._initialPosition = tempInitialPosition;
        }
    }

    // public void PullBackBoxes(){
    //     return;
    // }
        
    public static string[] RemoveNulls(string[] array)  
        {
            List<string> result = new List<string>();

            foreach (string element in array)
            {
                if (!string.IsNullOrWhiteSpace(element))
                {
                    result.Add(element);
                }
            }

            return result.ToArray();
        }

    public static int CalculateResult(string[] array)
    {
        int result = 0;
        bool isAddition = true;
        bool isNumSet = false;
        if(array.Length == 0){
            //Debug.Log("Error: Empty array");
            return 0;
        }
        else{string elementLast = array[array.Length - 1];
            if(elementLast == "+" || elementLast == "-"){
                //Debug.Log("Error: Invalid last element '" + elementLast + "' at index " + array.Length);
                return 0;
            }
        }
        
        for (int i = 0; i < array.Length; i++)
        {
            string element = array[i];

            if (element == "+")
            {
                if (i > 0 && array[i - 1] != "+" && array[i - 1] != "-")
                {
                    isAddition = true;
                    isNumSet = false;
                }
                else
                {
                    //Debug.Log("Error: Invalid consecutive operator '+' at index " + i);
                    return 0;
                }
            }
            else if (element == "-")
            {
                if (i > 0 && array[i - 1] != "+" && array[i - 1] != "-")
                {
                    isAddition = false;
                    isNumSet = false;
                }
                else
                {
                    //Debug.Log("Error: Invalid consecutive operator '-' at index " + i);
                    return 0;
                }
            }
            else
            {
                if (int.TryParse(element, out int num))
                {
                    if (!isNumSet)
                    {
                        if (isAddition)
                        {
                            result += num;
                        }
                        else
                        {
                            result -= num;
                        }
                        isNumSet = true;
                    }
                    else
                    {
                        //Debug.Log("Error: Invalid consecutive integer at index " + i);
                        return 0;
                    }
                }
                else
                {
                    //Debug.Log("Error: Invalid element '" + element + "' at index " + i);
                    return 0;
                }
            }
        }
        return result;
    }
    public string[] FetchDataFromSpaces()
{
    List<string> dataArray = new List<string>();

    foreach (Transform child in unimage.transform)
    {
        SpaceHandler spaceHandler = child.GetComponent<SpaceHandler>();
        if (spaceHandler != null)
        {
            if (spaceHandler.typeBool)
            {
                if (spaceHandler.data != 0)  // Ignore null (zero) values
                {
                    dataArray.Add(spaceHandler.data.ToString());
                }
            }
            else
            {
                dataArray.Add(spaceHandler.sign);
            }
        }
    }
    return dataArray.ToArray();
}

    public void UpdateTheScore(){
        calculationArray = FetchDataFromSpaces();
        calculationArray = RemoveNulls(calculationArray);
        result = CalculateResult(calculationArray);
        if(result != 0){
            resultText.text = result.ToString();
        }
        else{
            resultText.text = " ";
        }
    }

    public void UpdateTheColor(){
        if(expextedResult != 0){
        colorScale = (Math.Abs((float)(expextedResult-result)) / (Math.Abs((float)expextedResult)));
        }
        mainCamera.DOColor(new Color(colorScale/4, 1-colorScale, colorScale/20, 0.5f), 2f);
    }
}
