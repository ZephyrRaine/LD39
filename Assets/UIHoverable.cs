using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverable : UIComponent, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_startedDragging)
             _outline.enabled = (true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_startedDragging)
            _outline.enabled = (false);
    }
}
