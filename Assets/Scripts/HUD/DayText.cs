using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DAYS
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public class DayText : MonoBehaviour 
{
    int _day = 1;
    TMP_Text _textComponent;
    public int _CurrentDay
	{
		get
		{
            return _day;
        }
		set
		{
            _day = value;
            UpdateText();
        }
	}

	void Start()
	{
        _textComponent = GetComponent<TMP_Text>();
        UpdateText();
    }
    public void UpdateText()
	{
        _textComponent.text = ((DAYS) _day).ToString();
    }

	public void NextDay()
	{
        _CurrentDay++;
		if(_CurrentDay >= 7)
            _CurrentDay = 0;

        GameManager.GM.NewDay(_CurrentDay);
    }
}
