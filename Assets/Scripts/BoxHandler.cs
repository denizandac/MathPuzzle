using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BoxHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Type _type;
    [SerializeField] private TextMeshProUGUI _textMesh;
    public Vector3 _initialPosition;
    public bool typeBool;
    public bool inSpace;
    public string sign;
    public int data;
    public int lastIndex;
    public enum Type
    {
        number,
        sign
    }
    public bool willReturn = false;

    private void Awake()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if(gameObject.GetComponentInChildren<TextMeshProUGUI>() == null)
        {
            _textMesh = gameObject.AddComponent<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        
        inSpace = false;
        _initialPosition = transform.position;
        if(_type == Type.number)
        {
            typeBool = true;
            _textMesh.text = data.ToString();
        }
        else if(_type == Type.sign)
        {
            _textMesh.text = sign;
            typeBool = false;
        }
    }

    void Update(){
        if(typeBool){
            transform.GetComponent<Image>().color = new Color(0f,0.1f,0.5f,0.5f);
        }
        else{
            transform.GetComponent<Image>().color = new Color(0.5f,0f,0.1f,0.5f);
        }
    }
    
    public void ReturnToInitialPosition()
    {
        transform.position = _initialPosition;
        inSpace = false;
    }


    // #region Highlighting
    // public void OnMouseEnter()
    // {
    //     if (_type == Type.number)
    //     {
    //         gameManager.Instance.HighlightNumber();
    //     }
    //     else if (_type == Type.sign)
    //     {
    //         gameManager.Instance.HighlightSign();
    //     }
    // }
    // #endregion
    #region DragDropMechanism
    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = transform.position - Input.mousePosition;
        transform.GetComponent<Image>().color = new Color(0,0.5f,0.5f, 0.8f);
        _canvasGroup.blocksRaycasts = false;
        if(transform.parent == Unimage.Instance.transform)
        {
            lastIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
        }
        //for non-used case, image in unimage
        else{
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
        if(transform.parent == Unimage.Instance.transform)
        {
            transform.SetSiblingIndex(lastIndex);
        }
        //for non-used case, image in unimage
        else{
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
            if (draggedObject.GetComponent<BoxHandler>().typeBool == typeBool)
            {
                if (draggedObject.GetComponent<BoxHandler>().typeBool)
                {
                    int temp = draggedObject.GetComponent<BoxHandler>().data;
                    draggedObject.GetComponent<BoxHandler>().data = data;
                    data = temp;
                    draggedObject.GetComponent<BoxHandler>()._textMesh.text = draggedObject.GetComponent<BoxHandler>().data.ToString();
                    if(inSpace == true){
                        if(draggedObject.GetComponent<BoxHandler>().inSpace == true){
                            transform.parent.GetComponent<SpaceHandler>().data = data;
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().data = data;
                        }
                        else{
                            transform.parent.GetComponent<SpaceHandler>().data = data;
                        }
                    }
                    else{
                        if(draggedObject.GetComponent<BoxHandler>().inSpace == true){
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().data = data;
                        }
                    }
                    _textMesh.text = data.ToString();
                }
                else
                {
                    string temp = draggedObject.GetComponent<BoxHandler>().sign;
                    draggedObject.GetComponent<BoxHandler>().sign = sign;
                    sign = temp;
                    draggedObject.GetComponent<BoxHandler>()._textMesh.text = draggedObject.GetComponent<BoxHandler>().sign;
                    if(inSpace == true){
                        if(draggedObject.GetComponent<BoxHandler>().inSpace == true){
                            transform.parent.GetComponent<SpaceHandler>().sign = sign;
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().sign = sign;
                        }
                        else{
                            transform.parent.GetComponent<SpaceHandler>().sign = sign;
                        }
                    }
                    else{
                        if(draggedObject.GetComponent<BoxHandler>().inSpace == true){
                            draggedObject.transform.parent.GetComponent<SpaceHandler>().sign = sign;
                        }
                    }
                    _textMesh.text = sign;
                }
                draggedObject.transform.DOScale(1.2f, 0.5f)
                .SetEase(Ease.OutElastic)
                .OnComplete(()=>{
                    draggedObject.transform.DOScale(1f, 0.2f);
                });
                transform.DOScale(1.2f, 0.5f)
                .SetEase(Ease.OutElastic)
                .OnComplete(()=>{
                    transform.DOScale(1f, 0.2f);
                });
                //sound effect - canNOT swap
            }
            else{
                draggedObject.transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
            transform.DOShakePosition(0.5f, 3f, 10, 0f, true, false);
            }
        }
        draggedObject.transform.SetParent(Unimage.Instance.transform);
        draggedObject.GetComponent<BoxHandler>().ReturnToInitialPosition();
    }
    #endregion
}
