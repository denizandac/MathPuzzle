using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ResetBoxes : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("lvl3");
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