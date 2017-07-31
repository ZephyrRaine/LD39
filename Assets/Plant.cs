using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct DailyCheck
{
	public bool fed;
    public bool hydrated;

    public bool cleaned;

    public bool entertained;

    public bool darkness;

    public bool overwatered;

    public bool coffee;

    public bool whiskey;

}

public class Plant : MonoBehaviour {

	public SHAPE_AGE _currentAge;

    [SerializeField]
    DailyCheck check;

	void DailyReset()
	{
        check = new DailyCheck();
        check.cleaned = false;
        check.coffee = false;
        check.darkness = false;
        check.entertained = false;
        check.fed = false;
        check.hydrated = false;
        check.overwatered = false;
        check.whiskey = false;
    }

    // Use this for initialization
    void Start () 
	{
        DailyReset();
    }
	
    public void ReceiveItem(GameObject go)
    {
        if(!check.fed)
        {
            if (go.name.StartsWith("FOOD_"))
            {
                check.fed = true;
                if (go.name == "FOOD_Whiskey")
                {
                    check.whiskey = true;
                }
                else if (go.name == "FOOD_Coffee")
                {
                    check.coffee = true;
                }
            }
        }

        if(go.name.StartsWith("WATER_"))
        {
            if(!check.hydrated)
            {
                check.hydrated = true;
            }
            else
            {
                check.overwatered = true;
            }
        }

        if(go.name.StartsWith("PLAY_"))
        {
            check.entertained = true;
        }
    }
	// Update is called once per frame
	public string[] Evaluate(bool _cleaned, bool _darkness) 
	{

        string knot = "EVIL_HEALTHY";

        string[] stitches = new string[(int)DIALOGUE_TYPES.COUNT];

        for (int i = 0; i < stitches.Length; i++)
		{
            stitches[i] = "";
        }
        if (_currentAge > SHAPE_AGE.TEEN)
        {
            if (!check.fed)
            {
                stitches[(int)DIALOGUE_TYPES.HUNGRY] = knot + ".HUNGRY";
            }
            else if (check.coffee)
            {
                stitches[(int)DIALOGUE_TYPES.HUNGRY] = knot + ".COFFEE";
            }
            else if (check.whiskey)
            {
                stitches[(int)DIALOGUE_TYPES.HUNGRY] = knot + ".WHISKEY";
            }

            if(!check.entertained)
            {
                stitches[(int)DIALOGUE_TYPES.PLAY] = knot + ".PLAY";
            }

            if (!check.hydrated)
            {
                stitches[(int)DIALOGUE_TYPES.THIRSTY] = knot + ".THIRSTY";
            }
            else if (check.overwatered)
            {
                stitches[(int)DIALOGUE_TYPES.THIRSTY] = knot + ".OVERWATER";
            }

            if (!check.cleaned)
            {
                stitches[(int)DIALOGUE_TYPES.DIRTY] = knot + ".DIRTY";
            }

            if (check.darkness)
            {
                stitches[(int)DIALOGUE_TYPES.DARK] = knot + ".DARK";
            }
        }
        DailyReset();
        return stitches;
    }
}
