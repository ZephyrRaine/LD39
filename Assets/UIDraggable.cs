﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIDraggable : UIComponent, IBeginDragHandler, IEndDragHandler, IDragHandler {

    Vector2 _startPosition;
    Transform _parent;
    int _siblingOrder;

    GameObject _placeHolder;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startedDragging = true;
        _startPosition = transform.position;
        _image.raycastTarget = false;
        _parent = transform.parent;
        _siblingOrder = transform.GetSiblingIndex();

        transform.SetParent(_parent.parent);
        eventData.selectedObject = gameObject;

        _placeHolder = new GameObject();
        _placeHolder.transform.SetParent(_parent);
        _placeHolder.transform.SetSiblingIndex(_siblingOrder);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _startedDragging = false;
		transform.position = _startPosition;
        _image.raycastTarget = true;
        transform.SetParent(_parent);
        transform.SetSiblingIndex(_siblingOrder);
        Destroy(_placeHolder);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
