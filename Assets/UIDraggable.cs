using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIDraggable : UIComponent, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

    Vector2 _startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startedDragging = true;
        _startPosition = transform.position;
        _image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _startedDragging = false;
		transform.position = _startPosition;
        _image.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
