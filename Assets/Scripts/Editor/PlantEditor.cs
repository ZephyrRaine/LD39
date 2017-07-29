using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class PlantEditor : EditorWindow
{
	[MenuItem("Window/Plant Editor")]
	static void OpenWindow() 
	{
		PlantEditor mg = EditorWindow.GetWindow<PlantEditor>(true, "Plant Editor");
        mg.accesoriesList = new List<Part>();
        mg.LoadAccesories();
        mg.Show();
	}

    List<Part> accesoriesList;
    int currentTab = 0;
    string newName = "";
    PART_CATEGORY newCat = PART_CATEGORY.ANIMAL;
    private Sprite newSprite;

    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Shapes", "Parts" });
        if(EditorGUI.EndChangeCheck())
        {
            Debug.Log("SALUT");
        }
        switch(currentTab)
        {
            case 0:
                OnGUIShapes();
                break;
            case 1:
                OnGUIParts();
                break;
        }
    }

    void LoadAccesories()
	{
        accesoriesList.Clear();
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
        {
            string[] fold = new string[]{ PATHLIBRARY.PARTS_PATH + ((PART_CATEGORY)i).ToString() };
            string[] guids = AssetDatabase.FindAssets("t:Part", fold);
            Debug.Log("LOOK IN " + fold[0]);
            foreach (string guid in guids)
            {
                Debug.Log("FOUND");
                accesoriesList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Part)) as Part);
			}
        }
    }

    private void OnGUIParts()
    {
        foreach (Part a in accesoriesList)
        {
            EditorGUILayout.LabelField(a.name + " - " + a.category.ToString());
        }

        EditorGUILayout.Separator();

        newName = EditorGUILayout.TextField("Part name : ", newName);
        newCat = (PART_CATEGORY) EditorGUILayout.EnumPopup("Part category : ", newCat);
        newSprite = EditorGUILayout.ObjectField("Sprite", newSprite, typeof(Sprite), false) as Sprite;
        if(GUILayout.Button("Add new"))
		{
            Part a = Part.CreateInstance<Part>();
            a.category = newCat;
            a.naming = newName;
            a.sprite = newSprite;
            AssetDatabase.CreateAsset(a, PATHLIBRARY.PARTS_PATH + newCat.ToString() + "/" + newName + ".asset");
            AssetDatabase.SaveAssets();
            LoadAccesories();
        }
    }

    private void OnGUIShapes()
    {
       
    }
}
