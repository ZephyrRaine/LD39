using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightCeiling : MonoBehaviour {

    bool _status = false;
    [SerializeField]
    Sprite _on;
    [SerializeField]
    Sprite _off;

    Image _image;
    GameObject _light;

    void Start()
	{
        _image = GetComponent<Image>();
        _light = transform.GetChild(0).gameObject;
        _light.SetActive(false);
    }
    // Use this for initialization
    public void SwitchLight()
	{
        _status = !_status;
        _image.sprite = _status ? _on : _off;
        _light.SetActive(_status);
    }
}
