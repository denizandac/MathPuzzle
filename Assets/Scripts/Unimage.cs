using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (eventData.pointerDrag)
        {
            //nonselected
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
            eventData.pointerDrag.GetComponent<BoxHandler>().inSpace = false;
        }
    }
}
