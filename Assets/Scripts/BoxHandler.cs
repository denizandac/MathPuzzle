using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Vector3 _offset, _initialPosition;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Type _type;
    [SerializeField] private TextMeshProUGUI _textMesh;
    public bool typeBool;
    public string sign;
    public int data;
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

    public void ReturnToInitialPosition()
    {
        transform.position = _initialPosition;
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
    #region DragDropMEchanism
    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = transform.position - Input.mousePosition;
        transform.GetComponent<Image>().color = new Color(0.3f,0,0.3f, 0.6f);
        _canvasGroup.blocksRaycasts = false;
        if(transform.parent == Unimage.Instance.transform)
        {
            transform.SetAsLastSibling();
        }
        //for non-used case, image in unimage
        else{
            transform.parent.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color(1,1,1,1);
        transform.position = Input.mousePosition + _offset;
        _canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<BoxHandler>().ReturnToInitialPosition();
    }
    #endregion
}
