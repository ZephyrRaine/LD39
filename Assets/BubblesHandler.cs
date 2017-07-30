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
	OVERWATER,
	THANKS,
	UPSET,
	RANDOM
}

public class BubblesHandler : MonoBehaviour {

	public void DisplayBubble(DIALOGUE_TYPES type)
	{
        transform.GetChild((int)type).gameObject.SetActive(true);
    }
}
