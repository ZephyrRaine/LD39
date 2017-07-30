using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RessourcesManager))]
public class RessourcesManagerEditor : Editor  
{
    RessourcesManager sl;

    void OnEnable()
    {
        sl = target as RessourcesManager;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("REMINDER");
        for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
		{
            EditorGUILayout.LabelField(i.ToString() + " = " + ((PART_CATEGORY)i).ToString());
        }
        base.OnInspectorGUI();

        if(GUILayout.Button("INSTANTIATE AND POPULATE LIBRARIES"))
        {
            List<ShapeColorLibrary> sclList = new List<ShapeColorLibrary>();
            Dictionary<PART_CATEGORY, List<Color>> dicoColor = PlantEditorUtility.LoadColors();
            for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
            {
                ShapeColorLibrary colorLib = CreateInstance<ShapeColorLibrary>();
                colorLib._colorShapes = new List<Color>();
                if (dicoColor.ContainsKey((PART_CATEGORY)i))
                {
                    colorLib._colorShapes = dicoColor[(PART_CATEGORY)i];
                }
                AssetDatabase.CreateAsset(colorLib, PATHLIBRARY.SHAPES_PATH + ((PART_CATEGORY)i).ToString() + "_ColorLibrary.asset");
                sclList.Add(colorLib);
                
                
            }

            PartsLibrary pl = CreateInstance<PartsLibrary>();
            pl.partsLibrary = PlantEditorUtility.LoadParts();
            AssetDatabase.CreateAsset(pl, PATHLIBRARY.PARTS_PATH + "PartsLibrary.asset");


            List<ShapesLibrary> slList = new List<ShapesLibrary>();
            Dictionary<PART_CATEGORY, List<Shape>> dicoShapes = PlantEditorUtility.LoadShapes();
            for (int i = 0; i < (int)PART_CATEGORY.PART_COUNT; i++)
            {
                ShapesLibrary sLib = CreateInstance<ShapesLibrary>();
                sLib._shapesLibrary = new List<Shape>();
                if (dicoColor.ContainsKey((PART_CATEGORY)i))
                {
                    sLib._shapesLibrary = dicoShapes[(PART_CATEGORY)i];
                }
                AssetDatabase.CreateAsset(sLib, PATHLIBRARY.SHAPES_PATH + ((PART_CATEGORY)i).ToString() + "_ShapesLibrary.asset");
                slList.Add(sLib);
            }
        


            AssetDatabase.SaveAssets();

            sl.FeedLibraries(sclList, pl, slList);
            EditorUtility.SetDirty(sl);
        }
    }
    
}
