using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;

public class Card : MonoBehaviour {


    public float maxHp = 0;
    [HideInInspector]
    public float atk = 0;
    [HideInInspector]
    public float def = 0;
    [HideInInspector]
    public float speed = 0;

    [HideInInspector]
    public int id = 0;
    [HideInInspector]
    public bool isBorn = false;
    [HideInInspector]
    public int bornPos = 0;
    [HideInInspector]
    public Camps camp = Camps.CsCampPlayer;
    private List<AttributeResult> attributeResults;

    public void StepAttrRes(StepResult stepRes)
    {
        foreach ( var it in stepRes.AttrResList)
        {
            if(it.Camp == camp)
            {
                attributeResults.Add(it);
            }
        }
    }

    public void OnCollisionHandle()
    {
        foreach (var item in attributeResults)
        {
            switch (item.ResAttr)
            {
                case CardAttribute.CsCardMaxhp:
                    maxHp -= 5;
                    maxHp = Mathf.Max(0, maxHp);
                    maxHp = Mathf.Min(100, maxHp);
                    if (maxHp == 0)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
                case CardAttribute.CsCardAtk:
                    break;
                case CardAttribute.CsCardDef:
                    break;
                case CardAttribute.CsCardSpeed:
                    break;
                default:
                    break;
            }
        }
    }

    // Use this for initialization
    void Start () {
        attributeResults = new List<AttributeResult>();

    }
	
	// Update is called once per frame
	void Update () {

            
	}
}
