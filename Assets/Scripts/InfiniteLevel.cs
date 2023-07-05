using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevel : MonoBehaviour
{
    public int temp1, temp2, temp3, expectedResult, result;
    public float timeAdded;
    public GameObject unimage;
    public List<GameObject> boxList = new List<GameObject>();
    

    void Start()
    {
        GameManager.Instance.infiniteLevel = this;
        GetNewSet();
    }

    public void GetNewSet()
    {
        expectedResult = Random.Range(-30, 30);
        GameManager.Instance.mathHandler.expectedResult = expectedResult;
        GameManager.Instance.mathHandler.expectedResultText.text = expectedResult.ToString();
        boxList.Clear();
        temp1 = Random.Range(-20, 20);
        expectedResult -= temp1;
        temp2 = Random.Range(-20, 20);
        expectedResult += temp2;
        temp3 = expectedResult;
        foreach (Transform child in unimage.transform)
        {
            if (child.gameObject.CompareTag("Box"))
            {
                boxList.Add(child.gameObject);
            }
        }
        for (int i = 0; i < boxList.Count; i++)
        {
            BoxHandler boxHandler = boxList[i].GetComponent<BoxHandler>();
            if (boxHandler.typeBool)
            {
                if (temp1 != 0)
                {
                    boxHandler.data = temp1;
                    boxHandler.SetText(temp1.ToString());
                    temp1 = 0;
                }
                else if (temp2 != 0)
                {
                    boxHandler.data = temp2;
                    boxHandler.SetText(temp2.ToString());
                    temp2 = 0;
                }
                else if (temp3 != 0)
                {
                    boxHandler.data = temp3;
                    boxHandler.SetText(temp3.ToString());
                    temp3 = 0;
                }
            }
        }
    }
}