using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeReceiver : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameManager.GM.FeedMe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Evolve(SHAPE_AGE age)
    {
        foreach (GameObject go in _plants)
        {
            go.SetActive(false);
        }
        GetComponent<Plant>()._currentAge = age;
        _plants[(int)age].SetActive(true);
    }

    GameObject[] _plants;
    public void ReceiveShapes(Shape[] shapes, Color color, Part[] parts, MouthAsset mouth)
    {
        if (shapes != null)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            _plants = new GameObject[3];

            for (int i = 0; i < shapes.Length; i++)
            {
                GameObject plant = GameObject.Instantiate(shapes[i].go, transform.position, Quaternion.identity, transform) as GameObject;
                _plants[i] = (plant);
            }

            for (int i = 0; i < _plants.Length; i++)
            {
                GameObject plant = _plants[i];
                plant.GetComponent<Image>().color = color;
                if (i > 0)
                {
                    ImplementMouth(mouth._open, mouth._closed, i);
                    if (i > 1)
                    {
                        ImplementParts(parts);
                    }
                    plant.SetActive(false);
                }
            }

        }
    }


public void ImplementMouth(Sprite open, Sprite closed, int index = 0)
	{
		if(transform.childCount > index)
		{
            Debug.Log(index);
            Transform plant = _plants[index].transform;
            Debug.Log(plant);
            Transform partsParent = plant.transform.Find("Mouth");
            Debug.Log(partsParent);
            Mouth m = partsParent.GetComponentInChildren<Mouth>();
            m.ReceiverDelegate += GetComponent<Plant>().ReceiveItem;
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

    public void ImplementParts(Part[] partsToDeliver)
    {
		if(transform.childCount > 2)
		{
            Transform plant = _plants[2].transform;
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
