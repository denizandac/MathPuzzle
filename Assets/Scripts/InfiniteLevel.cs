using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevel : MonoBehaviour
{
    public int temp1, temp2, temp3, expectedResult, result, score;
    public float time;
    public GameObject unimage;
    public List<GameObject> boxList = new List<GameObject>();

    void Start()
    {
        GetNewSet();
    }

    void GetNewSet()
    {
        expectedResult = Random.Range(-30, 30);
        MathHandler.Instance.expextedResult = expectedResult;
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
            if (boxList[i].GetComponent<BoxHandler>().typeBool)
            {
                if (temp1 != 0)
                {
                    boxList[i].GetComponent<BoxHandler>().data = temp1;
                    boxList[i].GetComponent<BoxHandler>().SetText(temp1.ToString());
                    temp1 = 0;
                }
                else if (temp2 != 0)
                {
                    boxList[i].GetComponent<BoxHandler>().data = temp2;
                    boxList[i].GetComponent<BoxHandler>().SetText(temp2.ToString());
                    temp2 = 0;
                }
                else if (temp3 != 0)
                {
                    boxList[i].GetComponent<BoxHandler>().data = temp3;
                    boxList[i].GetComponent<BoxHandler>().SetText(temp3.ToString());
                    temp3 = 0;
                }
            }
            boxList[i].GetComponent<BoxHandler>().ReturnToInitialPosition();
            //MathHandler.Instance.SwapBoxes();
        }
    }
}