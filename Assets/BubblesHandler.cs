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
    [SerializeField]

    BasicChoiceManager bcm;

    string[] knots;
    void DisplayBubble(DIALOGUE_TYPES type)
	{
        transform.GetChild((int)type).gameObject.SetActive(true);
    }

	void HideBubble(DIALOGUE_TYPES type)
	{
        transform.GetChild((int)type).gameObject.SetActive(false);
    }

    void Start()
    {
        tb.transform.parent.gameObject.SetActive(true);
        InkOverlord.IO.RequestKnot("FRIDAY_NOON");
        if(InkOverlord.IO.canContinue)
        {
            tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
        }
        bcm.Input += MadeChoice;
    }

    int nextDay;
    bool waitingEndDay = false;
    void EndOfDay(int i)
    {
        tb.transform.parent.gameObject.SetActive(true);
        nextDay = i;
        switch(i)
        {
            case 2:
                InkOverlord.IO.RequestKnot("FRIDAY_NIGHT");
                break;
        }
        if(InkOverlord.IO.canContinue)
        {
            tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
        }

        waitingEndDay = true;
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

    public void MadeChoice(int i)
    {
        Debug.Log("YOOOO");
        bcm.ClearChoices();
        InkOverlord.IO.MakeChoice(i);
        tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
    }

    public void Proceed()
    {
        if (!bcm.IsBusy)
        {
            if (InkOverlord.IO.canContinue)
            {
                Debug.Log("CONTINUE");
                tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
            }
            else if (InkOverlord.IO.hasChoices)
            {
                Debug.Log("HAS CHOICES");
                bcm.FeedChoices(InkOverlord.IO.GetChoices());
                bcm.DisplayChoices();
            }
            else
            {
                Debug.Log("BYE BYE");
                tb.transform.parent.gameObject.SetActive(false);
                if(waitingEndDay)
                {
                    GameManager.GM.NewDay(nextDay);
                }
            }
        }
    }
}
