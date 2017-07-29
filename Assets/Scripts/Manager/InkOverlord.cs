using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class InkOverlord : MonoBehaviour {

    static InkOverlord _instance;

    public static InkOverlord IO
	{
		get 
		{
            return _instance;
        }
	}

    public void ChangeVariable(string key, object v)
    {
        _inkStory.variablesState[key] = v;
    }

    [SerializeField] TextAsset _storyScript;
    Story _inkStory; 

    public bool canContinue
	{
		get 
		{
            return _inkStory.canContinue;
        }
	}

    public bool hasChoices
    {
        get
        {
            return _inkStory.currentChoices.Count > 0;
        }
    }
	public string NextLine()
	{
       return _inkStory.Continue();
    }

	public List<Choice> GetChoices()
	{
        return _inkStory.currentChoices;
    }

	public bool MakeChoice(int index)
	{
		if(index < _inkStory.currentChoices.Count)
		{
            _inkStory.ChooseChoiceIndex(index);
            return true;
        }
        Debug.LogError("INVALID CHOICE INDEX");
        return false;
    }

    // Use this for initialization
    void Awake()
    { 
	
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _inkStory = new Story(_storyScript.text);

       _inkStory.BindExternalFunction("TRIGGEREVENT", (string id) =>
       {
           InkEventWatcher.Trigger(id);
       });
    }

	public void RequestKnot(string knotPath)
	{
        _inkStory.ChoosePathString(knotPath);
    }

}
