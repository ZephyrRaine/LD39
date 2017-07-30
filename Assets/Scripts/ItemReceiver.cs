using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemReceiver : MonoBehaviour, IDropHandler
 {
     void Start()
     {
    }
    public void OnDrop(PointerEventData eventData)
    {
    	Debug.Log(eventData.selectedObject);
        Mouth m = transform.parent.GetComponent<Mouth>();
        if(m != null)
        {
            Mouth.GameObjectDelegate d = m.ReceiverDelegate;
            if(d != null)
            {
                transform.parent.GetComponent<Mouth>().ReceiverDelegate(eventData.selectedObject);
            }
        }

    }

}
