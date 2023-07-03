using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SpaceHandler : MonoBehaviour, IDropHandler
{
    public bool isOccupied = false;
    public int data;
    public string sign;
    public bool typeBool;
    public CanvasGroup canvasGroup;
    public Color colorInt, colorSign;
    public GameObject unimage;
    void Update()
    {
        CheckIfOccupied();
    }
    public void CheckIfOccupied()
    {
        if (transform.childCount == 0)
        {
            isOccupied = false;
            data = 0;
            sign = "";
        }
        else
        {
            isOccupied = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject)
        {
            BoxHandler boxHandler = draggedObject.GetComponent<BoxHandler>();
            if (!isOccupied)
            {
                if (boxHandler.typeBool == typeBool)
                {
                    draggedObject.transform.position = transform.position;
                    draggedObject.transform.SetParent(transform);
                    draggedObject.transform.DOScale(1.2f, 0.5f)
                        .SetEase(Ease.OutElastic)
                        .OnComplete(() =>
                        {
                            draggedObject.transform.DOScale(1f, 0.2f);
                        });
                    if (typeBool)
                    {
                        data = boxHandler.data;
                    }
                    else
                    {
                        sign = boxHandler.sign;
                    }
                    isOccupied = true;
                    boxHandler.inSpace = true;
                }
                else
                {
                    draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
                    draggedObject.transform.SetParent(unimage.transform);
                    boxHandler.ReturnToInitialPosition();
                    boxHandler.inSpace = false;
                }

            }
            else
            {
                draggedObject.transform.SetParent(unimage.transform);
                boxHandler.ReturnToInitialPosition();
            }
        }
    }
}