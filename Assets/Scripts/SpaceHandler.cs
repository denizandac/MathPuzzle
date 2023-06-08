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
    public Type _type;
    public CanvasGroup canvasGroup;
    public enum Type
    {
        number,
        sign
    }
    void Awake(){
        if (gameObject.GetComponent<CanvasGroup>() == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    void Start(){
        if(_type == Type.number){
            transform.GetComponent<Image>().color = new Color(0f,0.1f,0.5f,0.2f);
        }
        else{
            transform.GetComponent<Image>().color = new Color(0.5f,0f,0.1f,0.2f);
        }
    }
    void Update(){
        if(transform.childCount > 0){
            isOccupied = true;
        }
        else{
            isOccupied = false;
        }
        if(!isOccupied){
            data = 0;
            sign = "";
        }
    }
    public void DataUpdate(){
        if(typeBool){
            data = transform.GetChild(0).GetComponent<BoxHandler>().data;
        }
        else{
            sign = transform.GetChild(0).GetComponent<BoxHandler>().sign;
        }
    }

    public void OnDrop(PointerEventData eventData){
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject){
            if (!isOccupied){
                if(draggedObject.GetComponent<BoxHandler>().typeBool == typeBool){
                    draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                    draggedObject.transform.SetParent(transform);
                    draggedObject.transform.DOScale(1.2f, 0.5f)
                        .SetEase(Ease.OutElastic)
                        .OnComplete(()=>{
                    draggedObject.transform.DOScale(1f, 0.2f);
                });
                    if(typeBool){
                        data = draggedObject.GetComponent<BoxHandler>().data;
                    }
                    else{
                    sign = draggedObject.GetComponent<BoxHandler>().sign;
                    }
                    isOccupied = true;
                    draggedObject.GetComponent<BoxHandler>().inSpace = true;
                }
                else{
                    draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
                    draggedObject.transform.SetParent(Unimage.Instance.transform);
                    draggedObject.GetComponent<BoxHandler>().ReturnToInitialPosition();
                    draggedObject.GetComponent<BoxHandler>().inSpace = false;
                }
                
            }
            else{
                draggedObject.transform.SetParent(Unimage.Instance.transform);
                draggedObject.GetComponent<BoxHandler>().ReturnToInitialPosition();
            }
        }
    }
}