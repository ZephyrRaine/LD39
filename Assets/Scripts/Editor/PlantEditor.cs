using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class PlantEditor : EditorWindow
{
	[MenuItem("Window/Plant Editor")]
	static void OpenWindow() 
	{
		PlantEditor mg = EditorWindow.GetWindow<PlantEditor>(true, "Plant Editor");
        
        mg.babyShapeModel = AssetDatabase.LoadAssetAtPath<GameObject>(PATHLIBRARY.SHAPES_PATH + "BABY_MODEL.prefab");
        mg.teenShapeModel = AssetDatabase.LoadAssetAtPath<GameObject>(PATHLIBRARY.SHAPES_PATH + "TEEN_MODEL.prefab");
        mg.adultShapeModel = AssetDatabase.LoadAssetAtPath<GameObject>(PATHLIBRARY.SHAPES_PATH + "ADULT_MODEL.prefab");
        mg.LoadAccesories();
        mg.LoadShapes();
        mg.Show();
	}

    int currentTab = 0;
    List<Part> partsList;
    
    private List<Shape> shapesList;
    string newName = "";
    PART_CATEGORY newCat = PART_CATEGORY.ANIMAL;
    private Sprite newSprite;
    private SHAPE_AGE newShape = SHAPE_AGE.BABY;

    private GameObject teenShapeModel;
    private GameObject adultShapeModel;
    private GameObject babyShapeModel;

    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Shapes", "Parts" });

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
        if(partsList == null)
        {
            partsList = new List<Part>();
        }

        partsList.Clear();
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
        {
            string[] fold = new string[]{ PATHLIBRARY.PARTS_PATH + ((PART_CATEGORY)i).ToString() };
            string[] guids = AssetDatabase.FindAssets("t:Part", fold);
            foreach (string guid in guids)
            {
                partsList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Part)) as Part);
			}
        }
    }

    private void OnGUIParts()
    {
        foreach (Part a in partsList)
        {
            EditorGUILayout.LabelField(a.naming + " - " + a.category.ToString());
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
            AssetDatabase.CreateAsset(a, PATHLIBRARY.PARTS_PATH + newCat.ToString() + "/" + "PART_" + newCat.ToString() + "_" + newName + ".asset");
            AssetDatabase.SaveAssets();
            LoadAccesories();
        }
    }

    
    void LoadShapes()
	{
        if(shapesList == null)
        {
            shapesList = new List<Shape>();
        }
        shapesList.Clear();
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
        {
            string[] fold = new string[] { PATHLIBRARY.SHAPES_PATH + ((PART_CATEGORY)i).ToString() };
            string[] guids = AssetDatabase.FindAssets("t:Shape", fold);
            foreach (string guid in guids)
            {
                shapesList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Shape)) as Shape);
            }
        }
    }

    private void OnGUIShapes()
    {
        foreach (Shape a in shapesList)
        {
            Debug.Log(shapesList.Count);
            EditorGUILayout.LabelField(a.category.ToString() + " - " + a.age.ToString() + " - " + a.name);
        }

        EditorGUILayout.Separator();

        newCat = (PART_CATEGORY) EditorGUILayout.EnumPopup("Shape category : ", newCat);
        newShape = (SHAPE_AGE)EditorGUILayout.EnumPopup("Shape age", newShape);
        newSprite = EditorGUILayout.ObjectField("Shape Sprite", newSprite, typeof(Sprite), false) as Sprite;
        if(GUILayout.Button("Add new"))
		{
            Shape a = Part.CreateInstance<Shape>();
            a.category = newCat;
            a.age = newShape;
            GameObject toInstantiate;
            switch(newShape)
            {
                case SHAPE_AGE.TEEN:
                    toInstantiate = teenShapeModel;
                    break;
                    case SHAPE_AGE.ADULT:
                    toInstantiate = adultShapeModel;
                    break;
                    default:
                    toInstantiate = babyShapeModel;
                    break;
            }
            Debug.Log(toInstantiate);
            GameObject go = InstantiatePrefabOnPlant(toInstantiate);
    
            Image i = go.GetComponent<Image>();
            i.sprite = newSprite;
            i.SetNativeSize();
            string pathToUse = PATHLIBRARY.SHAPES_PATH + newCat.ToString() + "/GameObjects/" + "GO_" + newCat.ToString() + "_" + newShape.ToString() +  ".prefab";
            
            
            a.go = PrefabUtility.CreatePrefab(AssetDatabase.GenerateUniqueAssetPath(pathToUse), go);

            pathToUse = PATHLIBRARY.SHAPES_PATH + newCat.ToString() + "/" + "SHAPE_" + newCat.ToString() + "_" +  newShape.ToString() + ".asset";
            AssetDatabase.CreateAsset(a, AssetDatabase.GenerateUniqueAssetPath(pathToUse));
            AssetDatabase.SaveAssets();
            DestroyImmediate(go);
            InstantiatePrefabOnPlant(a.go);
            LoadShapes();
        }
    }

    private GameObject InstantiatePrefabOnPlant(GameObject prefab)
    {
        GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        go.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform, false);
        return go;
    }
}
