using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour {

	[SerializeField]
    Sprite _default;
    [SerializeField]
    Sprite _nearby;
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
