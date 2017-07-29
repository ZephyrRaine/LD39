using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DEBUG_TABS
{
    PERFORMANCES,
    STORY,
    SCENE
}

public class MultiDebug : MonoBehaviour
{
    [SerializeField]
    private bool displayDebugInfo = true;
    bool uiActive = true;
    
    
    static void EasyCreate()
    {
        if(Debug.isDebugBuild)
        {
            MultiDebug md = FindObjectOfType<MultiDebug>();
            if(md == null)
            {
                GameObject go = new GameObject("[MultiDebug]", typeof(MultiDebug));
                DontDestroyOnLoad(go);
            }
        }
    }
    
    void OnGUI()
    {
        if (displayDebugInfo = GUI.Toggle(new Rect(10, 10, 200, 30), displayDebugInfo, "Display debug info"))
        {
			
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) displayDebugInfo = !displayDebugInfo;
    }

    void LateUpdate()
    {
       
    }
}
