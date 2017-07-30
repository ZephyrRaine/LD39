using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SHAPE_AGE
{
	BABY,
	TEEN,
	ADULT,
	SHAPE_COUNT
}


[CreateAssetMenu(fileName = "Shape", menuName = "Plant/Shape", order = 2)]
public class Shape : ScriptableObject 
{
    public PART_CATEGORY category;
    public SHAPE_AGE age;

    public GameObject go;
}


