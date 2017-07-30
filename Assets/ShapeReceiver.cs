using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ImplementMouth(Sprite open, Sprite closed)
	{
		if(transform.childCount > 0)
		{
            Transform plant = transform.GetChild(0);
            Transform partsParent = plant.transform.Find("Mouth");
            Mouth m = partsParent.GetComponentInChildren<Mouth>();
            m._default = closed;
            m._nearby = open;
            m.GetComponent<Image>().sprite = closed;
            m.GetComponent<Image>().SetNativeSize();
        }
		else
		{
            Debug.LogError("NO CHILD SHAPE");
        }
	}

    public void ImplementParts(List<Part> partsToDeliver)
    {
		if(transform.childCount > 0)
		{
            Transform plant = transform.GetChild(0);
            Debug.Log("plant Name " + plant.name);
            Transform partsParent = plant.transform.Find("Parts");
            Debug.Log("partsParent Name " + partsParent.name);
            List<Image> spots = new List<Image>();

            foreach(Transform t in partsParent)
			{
                Image i = t.GetComponentInChildren<Image>();
                spots.Add(i);
                i.enabled = false;
                Debug.Log("t Name " + t.name);
				
            }

            foreach(Part p in partsToDeliver)
			{
                Image i = spots[UnityEngine.Random.Range(0, spots.Count)];
                i.enabled = true;
                i.sprite = p.sprite;
                Debug.Log(i.gameObject.name);
                i.name = p.naming;
                i.SetNativeSize();
                spots.Remove(i);
            }
        }
		else
		{
            Debug.LogError("NO CHILD SHAPE");
        }
        
    }
}
