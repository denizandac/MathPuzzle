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
    public int result, expectedResult;
    public Camera mainCamera;
    public TextMeshProUGUI resultText, expectedResultText;
    public GameObject unimage, levelPopUp;
    public float colorScale;
    public List<string> calculationArray;
    public List<GameObject> boxList = new List<GameObject>();

   
    void Start()
    {
        GameManager.Instance.mathHandler = this;
        expectedResultText.text = expectedResult.ToString();
    }

    void Update()
    {
        UpdateTheResult();
    }
    public void SwapBoxes()
    {
        boxList.Clear();
        foreach (Transform child in unimage.transform)
        {
            if (child.gameObject.CompareTag("Box"))
            {
                boxList.Add(child.gameObject);
            }
        }
        for (int i = 0; i < boxList.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, boxList.Count);
            BoxHandler boxHandler = boxList[i].GetComponent<BoxHandler>();
            BoxHandler randomBoxHandler = boxList[randomIndex].GetComponent<BoxHandler>();
            Vector3 tempPosition = boxList[i].transform.position;
            Vector3 tempInitialPosition = boxHandler._initialPosition;
            boxList[i].transform.position = boxList[randomIndex].transform.position;
            boxList[randomIndex].transform.position = tempPosition;
            boxHandler._initialPosition = randomBoxHandler._initialPosition;
            randomBoxHandler._initialPosition = tempInitialPosition;
        }
    }
    public static List<string> RemoveNulls(List<string> array)
    {
        List<string> result = new List<string>();

        foreach (string element in array)
        {
            if (!string.IsNullOrWhiteSpace(element))
            {
                result.Add(element);
            }
        }

        return result;
    }

    public static int CalculateResult(List<string> array)
    {
        int result = 0;
        bool isAddition = true;
        bool isNumSet = false;
        if (array.Count == 0)
        {
            //Debug.Log("Error: Empty array");
            return 0;
        }
        else
        {
            string elementLast = array[array.Count - 1];
            if (elementLast == "+" || elementLast == "-")
            {
                //Debug.Log("Error: Invalid last element '" + elementLast + "' at index " + array.Length);
                return 0;
            }
        }

        for (int i = 0; i < array.Count; i++)
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
    public List<string> FetchDataFromSpaces()
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
        return dataArray;
    }

    public void UpdateTheResult()
    {
        calculationArray = FetchDataFromSpaces();
        calculationArray = RemoveNulls(calculationArray);
        result = CalculateResult(calculationArray);
        if (result != 0)
        {
            resultText.text = result.ToString();
        }
        else
        {
            resultText.text = " ";
        }
        if (result == expectedResult)
        {
            levelPopUp.SetActive(true);
            GameManager.Instance.EndLevel();
        }
    }
}