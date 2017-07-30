using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    Vector2 _startPosition;
    Transform _parent;
    int _siblingOrder;


    bool _startedDragging;
     Image _image;
    void Start()
    {
        
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startedDragging = true;
        _startPosition = transform.position;
        if(_image != null) _image.raycastTarget = false;
        _parent = transform.parent;
        _siblingOrder = transform.GetSiblingIndex();

        transform.SetParent(_parent.parent);
        eventData.selectedObject = gameObject;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _startedDragging = false;
		transform.position = _startPosition;
        if(_image != null) _image.raycastTarget = true;
        transform.SetParent(_parent);
        transform.SetSiblingIndex(_siblingOrder);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
