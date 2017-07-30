using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordsSpawner : MonoBehaviour 
{

    [SerializeField] 
	GameObject _wordModel;
    // Use this for initialization
    void Start () 
	{
        List<string> list = RessourcesManager.RM.GetAllNames();
        foreach (string s in list)
        {
            GameObject go = Instantiate(_wordModel, transform.position+Vector3.zero, Quaternion.identity, transform);
            go.name = s;
            go.GetComponent<TMP_Text>().text = s;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
