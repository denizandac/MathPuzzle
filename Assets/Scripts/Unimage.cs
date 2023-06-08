using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Unimage : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public static Unimage Instance;
    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject)
        {
            //nonselected
            draggedObject.transform.SetParent(transform);
            draggedObject.GetComponent<BoxHandler>().ReturnToInitialPosition();
            draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
        }
    }
}
