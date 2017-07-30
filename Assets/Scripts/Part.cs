using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PART_CATEGORY
{
	HUMAN,
	ANIMAL,
	FISH,
	TREE,
	CACTUS,
	OTHER,

	PART_COUNT
}

[CreateAssetMenu(fileName = "Part", menuName = "Plant/Part", order = 1)]
public class Part : ScriptableObject 
{
    public string naming;
    public PART_CATEGORY category;

    public Sprite sprite;
}
