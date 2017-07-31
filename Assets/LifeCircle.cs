using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LifeCircle : MonoBehaviour {

    Part[] selectedParts;
    int nb = 0;

    [SerializeField]
    GameObject endPanel;

    [SerializeField]
    TextBox tb;
    // Use this for initialization
    void Start () 
	{
        selectedParts = new Part[3];
        GetComponent<Mouth>().ReceiverDelegate += GetWord;
        tb.transform.parent.gameObject.SetActive(true);
        InkOverlord.IO.RequestKnot("MONDAY_MORNING");
        if(InkOverlord.IO.canContinue)
        {
            tb.ReadLine(0f, 1f, InkOverlord.IO.NextLine());
        }
    }

    public void Proceed()
    {
        if(InkOverlord.IO.canContinue)
        {
            tb.ReadLine(0f,1f,InkOverlord.IO.NextLine());
        }
        else if(InkOverlord.IO.hasChoices)
        {

        }
    }

    void GetWord(GameObject go)
    {
        selectedParts[nb++] = RessourcesManager.RM.RequestPart(go.name);
		if(nb == 3)
		{
            Dictionary<PART_CATEGORY, int> pc = new Dictionary<PART_CATEGORY, int>();
			foreach(Part p in selectedParts)
			{
                if(pc.ContainsKey(p.category))
                {
                    pc[p.category]++;
                }
                else
                {
                    pc.Add(p.category, 1);
                }
            }
            PART_CATEGORY cat;
			if(pc.Count > 2)
			{
                cat = pc.ElementAt(UnityEngine.Random.Range(0, pc.Count)).Key;
            }
            else if(pc.Count > 1)
            {
				if(pc.ElementAt(0).Value > pc.ElementAt(1).Value)
				{
                    cat = pc.ElementAt(0).Key;
                }
				else
				{
                    cat = pc.ElementAt(1).Key;	
                }
            }
            else
            {
                cat = pc.ElementAt(0).Key;
            }

            Shape[] s = RessourcesManager.RM.RequestShapes(cat);
            Color c = RessourcesManager.RM.RequestColor(cat);
            MouthAsset m = RessourcesManager.RM.RequestMouth(cat);
            GetComponent<Mouth>().ReceiverDelegate -= GetWord;
            GameManager.GM.Transitionning(s, c, selectedParts, m);
            endPanel.SetActive(true);
        }
        Destroy(go);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
