using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemReceiver : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData)
    {
    	Debug.Log(eventData.selectedObject);
		
    }

}
