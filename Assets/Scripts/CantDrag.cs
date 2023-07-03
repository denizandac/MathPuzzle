using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CantDrag : MonoBehaviour, IDropHandler
{

    // Update is called once per frame
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject)
        {
            draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
            draggedObject.transform.SetParent(Unimage.Instance.transform);
            draggedObject.GetComponent<BoxHandler>().ReturnToInitialPosition();
            draggedObject.GetComponent<BoxHandler>().inSpace = false;
        }
    }
}