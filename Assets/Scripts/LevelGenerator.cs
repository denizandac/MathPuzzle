using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int spaceCount, boxCount;
    public GameObject spacePrefab, boxPrefab;
    public List<BoxHandler> boxes;
    public List<string> datas;
    public float spaceBetween;

    public void Start()
    {
        //make datas list with size of boxCount;
        for (int i = 0; i < boxCount; i++)
        {
            BoxHandler tempBox = Instantiate(boxPrefab, transform).GetComponent<BoxHandler>();
            if (datas[i] == "+")
            {
                tempBox.type = BoxHandler.Type.sign;
                tempBox.sign = "+";
                tempBox.data = 0;
                tempBox.typeBool = false;
            }
            else if (datas[i] == "-")
            {
                tempBox.type = BoxHandler.Type.sign;
                tempBox.sign = "-";
                tempBox.data = 0;
                tempBox.typeBool = false;
            }
            else
            {
                tempBox.type = BoxHandler.Type.number;
                tempBox.sign = "";
                tempBox.data = int.Parse(datas[i]);
                tempBox.typeBool = true;
            }
            if (boxCount <= 6)
            {
                tempBox.SetInitialPosition(new Vector3(-2.5f + (i * spaceBetween), 0, 0));
            }
            else
            {
                if (i < 6)
                {
                    tempBox.SetInitialPosition(new Vector3(-2.5f + (i * spaceBetween), 0, 0));
                }
                else
                {
                    tempBox.SetInitialPosition(new Vector3(-2.5f + ((i - 6) * spaceBetween), -1.5f, 0));
                }
            }
            boxes.Add(new BoxHandler());
        }
    }
}
