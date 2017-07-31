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
	
	// Update is called once per frame
	public string[] Evaluate() 
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
