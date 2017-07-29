using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIHoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    protected Outline _outline;


	void Start()
	{
        _outline = GetComponent<Outline>();
		if(_outline != null)
            _outline.enabled = false;
    }
	public void OnPointerEnter(PointerEventData eventData)
    {
		if(_outline != null)	
             _outline.enabled = (true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		if(_outline != null)	
            _outline.enabled = (false);
    }
}
