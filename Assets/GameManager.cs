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
    }
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

    Shape[] _shapes;
    Color _color;
    Part[] _parts;

    ShapeReceiver _sr;
    public void Transitionning(Shape[] shapes, Color color, Part[] parts)
    {
        _shapes = shapes;
        _color = color;
        _parts = parts;
    }


	public void FeedMe(ShapeReceiver sr)
	{
        _sr = sr;
        _sr.ReceiveShapes(_shapes, _color, _parts);

    }

	
}
