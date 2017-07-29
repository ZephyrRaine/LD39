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
        mg.accesoriesList = new List<Accessory>();
        mg.LoadAccesories();
        mg.Show();
	}

    List<Accessory> accesoriesList;
    int currentTab = 0;
    string newName = "";
    ACCESSORY_CATEGORY newCat = ACCESSORY_CATEGORY.ANIMAL;
    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Shapes", "Accessories" });
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
                OnGUIAccessories();
                break;
        }
    }

    void LoadAccesories()
	{
        accesoriesList.Clear();
        for (int i = 0; i < (int)ACCESSORY_CATEGORY.ACCESSORY_COUNT; i++)
        {
            string[] fold = new string[]{ PATHLIBRARY.ACCESORIES_PATH + ((ACCESSORY_CATEGORY)i).ToString() };
            string[] guids = AssetDatabase.FindAssets("t:Accessory", fold);
            Debug.Log("LOOK IN " + fold[0]);
            foreach (string guid in guids)
            {
                Debug.Log("FOUND");
                accesoriesList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Accessory)) as Accessory);
			}
        }
    }

    private void OnGUIAccessories()
    {
        foreach (Accessory a in accesoriesList)
        {
            EditorGUILayout.LabelField(a.name + " - " + a.category.ToString());
        }

        EditorGUILayout.Separator();

        newName = EditorGUILayout.TextField("Accessory name : ", newName);
        newCat = (ACCESSORY_CATEGORY) EditorGUILayout.EnumPopup("Accessory category : ", newCat);
        if(GUILayout.Button("Add new"))
		{
            Accessory a = Accessory.CreateInstance<Accessory>();
            a.category = newCat;
            a.name = name;
            AssetDatabase.CreateAsset(a, PATHLIBRARY.ACCESORIES_PATH + newCat.ToString() + "/" + newName + ".asset");
            AssetDatabase.SaveAssets();
            LoadAccesories();
        }
    }

    private void OnGUIShapes()
    {
       
    }
}
