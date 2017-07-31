using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {


    private static GameManager _GM;
    public static GameManager GM
    {
        get
        {
			if(_GM == null)
			{
                GameObject go = new GameObject();
                _GM = go.AddComponent<GameManager>();
            }
            return _GM;
        }
    }

    public void NewDay(int day)
    {
        switch(day)
        {
			case 1:
				SceneManager.LoadScene("scene");
				break;
            case 2:
                _sr.Evolve(SHAPE_AGE.TEEN);
                break;
				case 3:
                _sr.Evolve(SHAPE_AGE.ADULT);
                break;

        }
        if(_bh != null)
        {
            _bh.DailyInit(_sr.GetComponent<Plant>().Evaluate());
        }
        else
        {
            Debug.LogError("BH NULL");
        }
        
    }
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

    Shape[] _shapes;
    Color _color;
    Part[] _parts;

    MouthAsset _mouth;

    ShapeReceiver _sr;
    BubblesHandler _bh;
    public void Transitionning(Shape[] shapes, Color color, Part[] parts, MouthAsset mouth)
    {
        _shapes = shapes;
        _color = color;
        _parts = parts;
        _mouth = mouth;
    }


	public void FeedMe(ShapeReceiver sr)
	{
        _sr = sr;
        _sr.ReceiveShapes(_shapes, _color, _parts, _mouth);
        _bh = _sr.transform.parent.GetComponentInChildren<BubblesHandler>();
    }

	
}
