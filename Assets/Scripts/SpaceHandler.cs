using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SpaceHandler : MonoBehaviour, IDropHandler
{
    public bool isOccupied = false;
    public int data;
    public string sign;
    public Type _type;
    public bool typeBool;
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
    void Update(){
        if(transform.childCount > 0){
            isOccupied = true;
        }
        else{
            isOccupied = false;
        }
    }
    void LateUpdate(){
        if(!isOccupied){
            data = 0;
            sign = "";
        }
    }

    public void OnDrop(PointerEventData eventData){
        if (!isOccupied){
            if (eventData.pointerDrag){
                if(eventData.pointerDrag.GetComponent<BoxHandler>().typeBool == typeBool){
                    eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                    eventData.pointerDrag.transform.SetParent(transform);
                    data = eventData.pointerDrag.GetComponent<BoxHandler>().data;
                    isOccupied = true;
                }
                else{
                    eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
                }
            }
        }
        else{
            eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
        }
        MathHandler.Instance.UpdateTheScore();
        MathHandler.Instance.UpdateTheColor();
    }
}
