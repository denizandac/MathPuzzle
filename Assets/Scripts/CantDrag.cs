using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CantDrag : MonoBehaviour, IDropHandler
{
    public GameObject unimage;

    // Update is called once per frame
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject)
        {
            BoxHandler boxHandler = draggedObject.GetComponent<BoxHandler>();
            draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
            boxHandler.ReturnToInitialPosition();
            boxHandler.inSpace = false;
        }
    }
}