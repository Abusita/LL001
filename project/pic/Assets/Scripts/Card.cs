﻿using System.Collections;
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
    //private List<AttributeResult> attributeResults;
    private Queue<AttributeResult> attrResults;


    /// <summary>
    /// 本步结果筛选
    /// </summary>
    /// <param name="stepRes">本步结果数据</param>
    public void StepAttrRes(StepResult stepRes)
    {
        foreach ( var it in stepRes.AttrResList)
        {
            if(it.Camp == camp)
            {
                attrResults.Enqueue(it);
            }
        }
    }

    /// <summary>
    /// 碰撞处理
    /// </summary>
    public void OnCollisionHandle()
    {
        if(attrResults.Count > 0)
        {
            var item = attrResults.Dequeue();
            switch (item.ResAttr)
            {
                case CardAttribute.CsCardMaxhp:
                    maxHp -= item.Value;
                    maxHp = Mathf.Clamp(maxHp, 0, 100);
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
        attrResults = new Queue<AttributeResult>();

    }
	
	// Update is called once per frame
	void Update () {
        if (maxHp == 0)
        {
            Destroy(this.gameObject);
        }

    }
}
