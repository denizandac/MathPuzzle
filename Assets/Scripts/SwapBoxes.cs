using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapBoxes : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        MathHandler.Instance.SwapBoxes();
    }
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            //nonselected
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
            eventData.pointerDrag.GetComponent<BoxHandler>().inSpace = false;
        }
    }
}