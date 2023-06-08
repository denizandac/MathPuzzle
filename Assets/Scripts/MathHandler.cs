using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MathHandler : MonoBehaviour
{
    public static MathHandler Instance;
    public int expextedResult, result;
    public Camera mainCamera;
    public TextMeshProUGUI resultText;
    public GameObject unimage;
    public float colorScale;
    public string[] calculationArray;
    public List<GameObject> boxList = new List<GameObject>();
    public List<BoxHandler> spaceList = new List<BoxHandler>();
    public GameObject resultList;
    public GameObject brokenCube;
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
    }
    void LateUpdate(){      
        // if(result == expextedResult){
        //     GameManager.Instance.EndGame();
        // }
        UpdateTheResult();
        //UpdateTheColor();
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

    public void UpdateTheResult(){
        calculationArray = FetchDataFromSpaces();
        calculationArray = RemoveNulls(calculationArray);
        result = CalculateResult(calculationArray);
        if(result != 0){
            resultText.text = result.ToString();
        }
        else{
            resultText.text = " ";
        }
        foreach(Transform cube in resultList.transform){
            int index = 0;
            if(cube != null){
                if(cube.GetComponent<NumberCube>().GetCubeValue() == result){
                    Destroyable.instance.gameObject.SetActive(true);
                    Destroyable.instance.InstantiateTheCube(cube.transform.position);
                    Destroyable.instance.Activate();
                    // resultList.RemoveAt(index);
                    Destroy(cube.gameObject);
                }
            }
            else{
                //Debug.Log("All cubes Destroyed");
                // level ending animation
                SceneManager.LoadScene("Start"); //EDIT THIS
            }
            index++;
        }
    }

    // public void UpdateTheColor(){
    //     if(expextedResult != 0){
    //     colorScale = (Math.Abs((float)(expextedResult-result)) / (Math.Abs((float)expextedResult)));
    //     }
    //     mainCamera.DOColor(new Color(colorScale/4, 1-colorScale, colorScale/20, 0.5f), 2f);
    // }
}
