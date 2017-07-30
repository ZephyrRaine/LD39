using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UIDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    Vector2 _startPosition;
    Transform _parent;
    int _siblingOrder;



    bool _startedDragging;
    Image _image;

    Wiggle _wiggle;
    TMP_Text _text;
    void Start()
    {
        _image = GetComponent<Image>();
        _text = GetComponent<TMP_Text>();
        _wiggle = GetComponent<Wiggle>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _startedDragging = true;
        _startPosition = transform.position;
        if(_image != null) _image.raycastTarget = false;
        if(_text != null) _text.raycastTarget = false;
        if(_wiggle != null) _wiggle._stopped = true;
        _parent = transform.parent;
        _siblingOrder = transform.GetSiblingIndex();

        transform.SetParent(_parent.parent);
        eventData.selectedObject = gameObject;
        
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _startedDragging = false;
		transform.position = _startPosition;
        if(_image != null) _image.raycastTarget = true;
        if(_text != null) _text.raycastTarget = true;
        if(_wiggle != null) _wiggle._stopped = false;
        transform.SetParent(_parent);
        transform.SetSiblingIndex(_siblingOrder);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
