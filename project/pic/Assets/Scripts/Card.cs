using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;

public class Card : MonoBehaviour {

    [HideInInspector]
    public float maxHp = 0;
    [HideInInspector]
    public float curHp = 0;
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
    private Queue<AttributeResult> attrResults;


    /// <summary>
    /// 初始化卡牌
    /// </summary>
    /// <param name="card">卡牌信息</param>
    /// <param name="localCamp">卡牌所在阵营</param>
    public void Init(CardMsg card, Camps localCamp)
    {
        this.atk = card.Atk;
        this.maxHp = card.MaxHp;
        this.curHp = card.MaxHp;
        this.def = card.Def;
        this.speed = card.Speed;
        this.bornPos = card.BornPos;
        this.camp = localCamp;
    }

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
                    curHp -= item.Value;
                    curHp = Mathf.Clamp(curHp, 0, maxHp);
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
        if (curHp == 0)
        {
            Destroy(this.gameObject);
        }

    }
}
