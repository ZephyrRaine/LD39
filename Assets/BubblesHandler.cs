using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PERSONNALITY_TYPES
{
    HAPPY_HEALTHY,
    HAPPY_SICK,
    SAD_HEALTHY,
    SAD_SICK,
    EVIL_HEALTHY,
	EVIL_SICK
}
public enum DIALOGUE_TYPES
{
	HUNGRY,
	THIRSTY,
	DIRTY,
	PLAY,
	DARK,
	THANKS,
	UPSET,
	RANDOM,

	COUNT
}

public class BubblesHandler : MonoBehaviour {

    [SerializeField]
    TextBox tb;

    string[] knots;
    void DisplayBubble(DIALOGUE_TYPES type)
	{
        transform.GetChild((int)type).gameObject.SetActive(true);
    }

	void HideBubble(DIALOGUE_TYPES type)
	{
        transform.GetChild((int)type).gameObject.SetActive(false);
    }

	public void DailyInit(string[] _knots)
	{
        knots = _knots;
		for(int i = 0; i < knots.Length; i++)
		{
            string s = _knots[i];
            if(s != string.Empty)
			{
                DisplayBubble((DIALOGUE_TYPES)i);
            }
			else
			{
                HideBubble((DIALOGUE_TYPES)i);
            }
		}
    }

	public void DisplayText(int i)
	{
        InkOverlord.IO.RequestKnot(knots[i]);
        tb.transform.parent.gameObject.SetActive(true);
		if(InkOverlord.IO.canContinue)
		{
            tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
        }
        HideBubble((DIALOGUE_TYPES)i);
    }

	public void Proceed()
	{
        if (!tb._isReading)
        {
            if (InkOverlord.IO.canContinue)
            {
                tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
            }
            else
            {
                tb.transform.parent.gameObject.SetActive(false);
            }
        }
		else
		{
            tb.DisplayImmediate();
        }
    }
}
