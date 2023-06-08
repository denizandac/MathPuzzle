using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DropBoxes : MonoBehaviour
{
    public int id;
    public List<GameObject> cubes;
    public GameObject parent;
    public static DropBoxes instance;

    private void Awake()
    {
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void CheckBoxes()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            cubes.Add(child);
        }
    }
    public void DropAboveBoxes(int index)
    {
        id = index;
        if (index == cubes.Count)
        {
            cubes.RemoveAt(0);
            return;
        }
        else if (cubes == null) return;
        else
        {
            cubes.Remove(cubes[index]);
            for (int i = index; i < cubes.Count; i++)
            {
                cubes[i].transform.DOMoveY(cubes[i].transform.position.y - 1.5f, 0.8f).SetEase(Ease.InQuart);
            }
        }
        
    }
}
