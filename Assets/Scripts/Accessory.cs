using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACCESSORY_CATEGORY
{
	HUMAN,
	ANIMAL,
	FISH,
	TREE,
	CACTUS,
	OTHER,

	ACCESSORY_COUNT
}

[CreateAssetMenu(fileName = "Accessory", menuName = "Plant", order = 1)]
public class Accessory : ScriptableObject 
{
    public string naming;
    public ACCESSORY_CATEGORY category;
}
