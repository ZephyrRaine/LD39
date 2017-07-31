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
    int nextDay;
    public void NewDay(int day)
    {
        nextDay = day;
        if(_fader == null)
        {
            _fader = GameObject.FindObjectOfType(typeof(Fader)) as Fader;
            _fader.allBlackDelegate += ApplyDay;
        }
        _fader.Init();
    }

    void ApplyDay()
    {
        switch(nextDay)
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
            bool cleaned = true;
            bool darkness = false;
            List<GameObject> poopPool = new List<GameObject>();
            foreach(Transform t in _poopySpot)
            {
                if(t.gameObject.activeSelf)
                {
                    Debug.Log(t.gameObject.name + " is some active poop");
                    cleaned = false;
                }
                else
                {
                    poopPool.Add(t.gameObject);
                }
            }

            poopPool[UnityEngine.Random.Range(0, poopPool.Count)].SetActive(true);

            darkness = !_light._status;

            _bh.DailyInit(_sr.GetComponent<Plant>().Evaluate(cleaned, darkness));
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

    LightCeiling _light;
    Transform _poopySpot;

    Fader _fader;
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
        _poopySpot = _sr.transform.parent.Find("Desk").Find("PoopySpots");
        _light = _sr.transform.parent.GetComponentInChildren<LightCeiling>();
        _fader = _sr.transform.parent.Find("Fading").GetComponent<Fader>();
        _fader.allBlackDelegate += ApplyDay;
    }

	
}
