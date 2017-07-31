using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BarManager : MonoBehaviour, IPointerExitHandler  {

    Transform _currentBar;
    Image _image;
    // Use this for initialization
    void Start () {
        _image = GetComponent<Image>();
    }

	public void HideBar()
	{
		if(_currentBar != null)
		_currentBar.gameObject.SetActive(false);
        _currentBar = null;
        _image.raycastTarget = false;
    }
	public void WantYou(Transform bar)
	{
        HideBar();
        _currentBar = bar;
        _currentBar.gameObject.SetActive(true);
        _image.raycastTarget = true;
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject rc = eventData.pointerCurrentRaycast.gameObject;
        if(rc == null || !rc.transform.parent.name.EndsWith("Bar"))
		{
         HideBar();
		}
    }
}
