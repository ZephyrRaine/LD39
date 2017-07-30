using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mouth : MonoBehaviour {

    public Sprite _default;
    public Sprite _nearby;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Nearby()
	{
        Debug.Log("OH HI");
        GetComponent<Image>().sprite = _nearby;
    }

	public void Byeby()
	{
        Debug.Log("OH BYE");
        GetComponent<Image>().sprite = _default;
	}
}
