using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpongeReceive : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.selectedObject.name.StartsWith("CLEAN_"))
		{
            transform.parent.gameObject.SetActive(false);
        }
    }

  
}
