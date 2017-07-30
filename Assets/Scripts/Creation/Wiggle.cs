using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour {

    float _speed;

    Vector3 _randomDir;
    float _elapsed;

    float _ratioAngle;
    float _ratioScale;

    public bool _scale = true;

    public bool _stopped;
    // Use this for initialization
    void Start () 
	{
        _elapsed = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
        _randomDir = new Vector3(Mathf.Cos(Random.value * Mathf.PI * 2), Mathf.Sin(Random.value * Mathf.PI * 2)).normalized;
        _speed = Random.Range(-2f, 2f);
        _ratioAngle = Random.Range(.2f, .8f);
        _ratioScale = Random.Range(.2f, .8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stopped)
        {
            _elapsed += Time.deltaTime * 0.5f;
            transform.position += new Vector3(Mathf.Cos(_elapsed), Mathf.Sin(_elapsed)) * 2f + _randomDir * _speed * Mathf.Cos(_elapsed);
            if(_scale)
            {

            transform.eulerAngles += new Vector3(0f, 0f, 1f) * Mathf.Sin(_elapsed) * _ratioAngle;
            Vector2 ok = Vector2.one * (0.25f + (Mathf.Cos(_elapsed) * 0.5f + 0.5f) * _ratioScale);
                transform.localScale = new Vector3(ok.x, ok.y, 1f);
            }
        }
    }
}
