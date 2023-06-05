using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (!isOccupied){
            if (eventData.pointerDrag){
                if(eventData.pointerDrag.GetComponent<BoxHandler>().typeBool == typeBool){
                    eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                    eventData.pointerDrag.transform.SetParent(transform);
                    if(typeBool){
                        data = eventData.pointerDrag.GetComponent<BoxHandler>().data;
                    }
                    else{
                    sign = eventData.pointerDrag.GetComponent<BoxHandler>().sign;
                    }
                    isOccupied = true;
                    eventData.pointerDrag.GetComponent<BoxHandler>().inSpace = true;
                }
                else{
                    eventData.pointerDrag.transform.SetParent(Unimage.Instance.transform);
                    eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
                    eventData.pointerDrag.GetComponent<BoxHandler>().inSpace = false;
                }
            }
        }
        else{
            eventData.pointerDrag.transform.SetParent(Unimage.Instance.transform);
            eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
        }
    }
}