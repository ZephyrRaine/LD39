using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapesLibrary))]
public class ShapesLibraryEditor : Editor  
{
    ShapesLibrary sl;

    void OnEnable()
    {
        sl = target as ShapesLibrary;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("REMINDER");
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
		{
            EditorGUILayout.LabelField(i.ToString() + " = " + ((PART_CATEGORY)i).ToString());
        }
        base.OnInspectorGUI();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
