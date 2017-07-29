using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public abstract class UIComponent : MonoBehaviour 
{
    protected bool _startedDragging;
    protected Outline _outline;

    protected Image _image;
    // Use this for initialization
    void Start () {
        _outline = GetComponent<Outline>();
        _outline.enabled = (false);
        _image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
