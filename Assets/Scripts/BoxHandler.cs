using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BoxHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{

    public Vector3 _initialPosition;
    public bool typeBool, inSpace;
    public string sign;
    public int data, lastIndex;
    public Color colorInt, colorSign;
    public GameObject unimage;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _textMesh;
    public bool willReturn = false;

    private void Awake()
    {
        inSpace = false;
        _initialPosition = transform.position;
    }

    void Start()
    {
        if (typeBool)
        {
            _textMesh.text = data.ToString();
        }
        else
        {
            _textMesh.text = sign;
        }
    }


    public void ReturnToInitialPosition()
    {
        transform.position = _initialPosition;
        inSpace = false;
    }
    public void SetText(string text)
    {
        _textMesh.text = text;
    }

    public void SetInitialPosition(Vector3 position)
    {
        _initialPosition = position;
    }

    #region DragDropMechanism
    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = transform.position - Input.mousePosition;
        _canvasGroup.blocksRaycasts = false;
        if (transform.parent == unimage.transform)
        {
            lastIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
        }
        //for non-used case, image in unimage
        else
        {
            lastIndex = transform.parent.GetSiblingIndex();
            transform.parent.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _offset;
        _canvasGroup.blocksRaycasts = true;
        if (transform.parent == unimage.transform)
        {
            transform.SetSiblingIndex(lastIndex);
        }
        //for non-used case, image in unimage
        else
        {
            transform.parent.SetSiblingIndex(lastIndex);
        }
        lastIndex = 0;
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Swap function
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject)
        {
            BoxHandler boxHandler = draggedObject.GetComponent<BoxHandler>();
            if (boxHandler.typeBool == typeBool)
            {
                if (boxHandler.typeBool)
                {
                    int temp = boxHandler.data;
                    boxHandler.data = data;
                    data = temp;
                    boxHandler._textMesh.text = boxHandler.data.ToString();
                    if (inSpace == true)
                    {
                        SpaceHandler spaceHandler = transform.parent.GetComponent<SpaceHandler>();
                        if (boxHandler.inSpace == true)
                        {
                            spaceHandler.data = data;
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().data = data;
                        }
                        else
                        {
                            spaceHandler.data = data;
                        }
                    }
                    else
                    {
                        if (boxHandler.inSpace == true)
                        {
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().data = data;
                        }
                    }
                    _textMesh.text = data.ToString();
                }
                else
                {
                    string temp = boxHandler.sign;
                    boxHandler.sign = sign;
                    sign = temp;
                    boxHandler._textMesh.text = boxHandler.sign;
                    if (inSpace == true)
                    {
                        SpaceHandler spaceHandler = transform.parent.GetComponent<SpaceHandler>();
                        if (boxHandler.inSpace == true)
                        {
                            spaceHandler.sign = sign;
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().sign = sign;
                        }
                        else
                        {
                            spaceHandler.sign = sign;
                        }
                    }
                    else
                    {
                        if (boxHandler.inSpace == true)
                        {
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().sign = sign;
                        }
                    }
                    _textMesh.text = sign;
                }
                draggedObject.transform.DOScale(1.2f, 0.5f)
                .SetEase(Ease.OutElastic)
                .OnComplete(() =>
                {
                    draggedObject.transform.DOScale(1f, 0.2f);
                });
                transform.DOScale(1.2f, 0.5f)
                .SetEase(Ease.OutElastic)
                .OnComplete(() =>
                {
                    transform.DOScale(1f, 0.2f);
                });
                //sound effect - canNOT swap
            }
            else
            {
                draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
                transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
            }
        }
        draggedObject.transform.SetParent(unimage.transform);
        draggedObject.GetComponent<BoxHandler>().ReturnToInitialPosition();
    }
    #endregion
}
