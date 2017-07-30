using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

static public class PlantEditorUtility
{
    static public Dictionary<PART_CATEGORY, List<Color>> LoadColors()
    {
        Dictionary<PART_CATEGORY, List<Color>> colorDico = new Dictionary<PART_CATEGORY, List<Color>>();
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
        {
            List<Color> colors = new List<Color>();
            TextAsset ta = AssetDatabase.LoadAssetAtPath<TextAsset>(PATHLIBRARY.SHAPES_PATH + ((PART_CATEGORY)i).ToString() + "/COLOURS.txt");
            if (ta != null)
            {
                string[] colorsStr = ta.text.Split('-');
                List<string> colorStrings = new List<string>(colorsStr);
                foreach (string s in colorStrings)
                {
                    Color c = Color.white;
                    ColorUtility.TryParseHtmlString(s, out c);
                    colors.Add(c);
                }
                colorDico[((PART_CATEGORY)i)] = colors;
            }

        }

        return colorDico;
    }

    static public List<Part> LoadParts()
    {

        List<Part> partsList = new List<Part>();

        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
        {
            string[] fold = new string[] { PATHLIBRARY.PARTS_PATH + ((PART_CATEGORY)i).ToString() };
            string[] guids = AssetDatabase.FindAssets("t:Part", fold);
            foreach (string guid in guids)
            {
                partsList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Part)) as Part);
            }
        }

        return partsList;
    }

    static public Dictionary<PART_CATEGORY, List<Shape>> LoadShapes()
	{
        Dictionary<PART_CATEGORY, List<Shape>> shapesDico = new Dictionary<PART_CATEGORY, List<Shape>>();

        
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
        {
            List<Shape> list = new List<Shape>();
            string[] fold = new string[] { PATHLIBRARY.SHAPES_PATH + ((PART_CATEGORY)i).ToString() };
            string[] guids = AssetDatabase.FindAssets("t:Shape", fold);
            foreach (string guid in guids)
            {
                list.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Shape)) as Shape);
            }
            shapesDico[((PART_CATEGORY)i)] = list;
        }

        return shapesDico;
    }

    static public List<Shape> GetNonSortedShapesList()
    {
          Dictionary<PART_CATEGORY, List<Shape>> dico = PlantEditorUtility.LoadShapes();
    List<Shape> shapesList = new List<Shape>();
        foreach(KeyValuePair<PART_CATEGORY, List<Shape>> kvp in dico)
        {
            List<Shape> l = dico[kvp.Key];
            shapesList.AddRange(l);
        }

        return shapesList;
    }
}

public class PlantEditor : EditorWindow
{
	[MenuItem("Window/Plant Editor")]
	static void OpenWindow() 
	{
		PlantEditor mg = EditorWindow.GetWindow<PlantEditor>(true, "Plant Editor");
        
        mg.babyShapeModel = AssetDatabase.LoadAssetAtPath<GameObject>(PATHLIBRARY.SHAPES_PATH + "BABY_MODEL.prefab");
        mg.teenShapeModel = AssetDatabase.LoadAssetAtPath<GameObject>(PATHLIBRARY.SHAPES_PATH + "TEEN_MODEL.prefab");
        mg.adultShapeModel = AssetDatabase.LoadAssetAtPath<GameObject>(PATHLIBRARY.SHAPES_PATH + "ADULT_MODEL.prefab");
        mg.partsList = PlantEditorUtility.LoadParts();

        mg.shapesList = PlantEditorUtility.GetNonSortedShapesList();
        mg.colorDico = PlantEditorUtility.LoadColors();
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

    private Dictionary<PART_CATEGORY, List<Color>> colorDico;

    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Shapes", "Parts", "Creator" });

        switch(currentTab)
        {
            case 0:
                OnGUIShapes();
                break;
            case 1:
                OnGUIParts();
                break;
                case 2:
                OnGUICreator();
                break;
        }
    }

    private void OnGUICreator()
    {
        if (GUILayout.Button("RANDOMIZE!!!"))
        {
            Shape s = shapesList[UnityEngine.Random.Range(0, shapesList.Count)];
            Debug.Log(s.category);
            List<Color> availableColors = colorDico[s.category];
            GameObject plant = InstantiatePrefabOnPlant(s.go, availableColors[UnityEngine.Random.Range(0, availableColors.Count)]);
           // Debug.Break();
            List<Part> listToPickFrom = new List<Part>(partsList);
            List<Part> partsToDeliver = new List<Part>();
            for (int i = 0; i < 3; i++)
            {
                Part random = listToPickFrom[UnityEngine.Random.Range(0, listToPickFrom.Count)];
                Debug.Log(random);
                partsToDeliver.Add(random);
                listToPickFrom.Remove(random);
            }
            if (s.age == SHAPE_AGE.ADULT)
            {
                ShapeReceiver sr = GameObject.FindGameObjectWithTag("Player").GetComponent<ShapeReceiver>();
               sr.ImplementParts(partsToDeliver);
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
            partsList = PlantEditorUtility.LoadParts();
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
            shapesList = PlantEditorUtility.GetNonSortedShapesList();
        }
    }

    private GameObject InstantiatePrefabOnPlant(GameObject prefab, Color color)
    {
        GameObject go = InstantiatePrefabOnPlant(prefab);
        go.GetComponent<Image>().color = color;
        return go;
    }

    private GameObject InstantiatePrefabOnPlant(GameObject prefab)
    {
        GameObject pot = GameObject.FindGameObjectWithTag("Player");
        if(pot.transform.childCount > 0)
        {
            DestroyImmediate(pot.transform.GetChild(0).gameObject);
        }
        GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        go.transform.SetParent(pot.transform, false);
        return go;
    }
}
