using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RessourcesManager : MonoBehaviour 
{
    [SerializeField]
    List<ShapeColorLibrary> _colorLibrary;
    [SerializeField]
    PartsLibrary _partsLibrary;
    [SerializeField]
    List<ShapesLibrary> _shapesLibrary;

    public List<string> GetAllNames()
    {
        List<string> ls = new List<string>();
        foreach(Part p in _partsLibrary.partsLibrary)
        {
            ls.Add(p.naming);
        }
        return ls;
    }

    private static RessourcesManager _RM;
    
    [SerializeField]
    private List<MouthLibrary> _mouthsLibrary;

    public static RessourcesManager RM
    {
        get
        {
            return _RM;
        }
    }



    public void Awake()
    {
        _RM = this;
    }

    public Part RequestPart(string name)
    {
       foreach(Part p in _partsLibrary.partsLibrary)
       {
            if(p.naming == name)
            {
                return p;
            }
       }

        Debug.LogError("SALRGLERLG");
        return null;
    }

    public void FeedLibraries(List<ShapeColorLibrary> cLib, PartsLibrary pLib, List<ShapesLibrary> sLib, List<MouthLibrary> mLib)
    {
        _colorLibrary = cLib;
        _partsLibrary = pLib;
        _shapesLibrary = sLib;
        _mouthsLibrary = mLib;
    }

    public Color RequestColor(PART_CATEGORY pc)
    {
        List<Color> colours = _colorLibrary[(int)pc]._colorShapes;
        Color c = colours[UnityEngine.Random.Range(0, colours.Count)];
        return c;
    }

    public MouthAsset RequestMouth(PART_CATEGORY pc)
    {
        List<MouthAsset> mouths = _mouthsLibrary[(int)pc].mouthLibrary;
        MouthAsset m = mouths[UnityEngine.Random.Range(0, mouths.Count)];
        return m;
    }


    public Shape[] RequestShapes(PART_CATEGORY pc)
    {
        Shape[] shapes = new Shape[3];
        Dictionary<SHAPE_AGE, List<Shape>> dico = new Dictionary<SHAPE_AGE, List<Shape>>();
        dico[SHAPE_AGE.BABY] = new List<Shape>();
        dico[SHAPE_AGE.TEEN] = new List<Shape>();
        dico[SHAPE_AGE.ADULT] = new List<Shape>();
        if(_shapesLibrary[(int)pc]._shapesLibrary.Count > 0)
        {
            foreach (Shape s in _shapesLibrary[(int)pc]._shapesLibrary)
            {
                if (s.category == pc)
                {
                    dico[s.age].Add(s);
                }
            }
        }
        shapes[(int)SHAPE_AGE.BABY] = dico[SHAPE_AGE.BABY][UnityEngine.Random.Range(0, dico[SHAPE_AGE.BABY].Count)];
        shapes[(int)SHAPE_AGE.TEEN] = dico[SHAPE_AGE.TEEN][UnityEngine.Random.Range(0, dico[SHAPE_AGE.TEEN].Count)];
        shapes[(int)SHAPE_AGE.ADULT] = dico[SHAPE_AGE.ADULT][UnityEngine.Random.Range(0, dico[SHAPE_AGE.ADULT].Count)];
        return shapes;
    }


}
