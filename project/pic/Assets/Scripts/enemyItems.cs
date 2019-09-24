using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;

public class enemyItems : MonoBehaviour {

    public GameObject cardItem;             //卡牌Prefab
    public GameObject[] items;              //站位

    public CommonData.PlayerID playerID;    //玩家标识符
    public CommonData.Camps localCamp;


    private void Init(CampInfo campInfo)
    {
        if (campInfo != null)
        {
            foreach (CardMsg it in campInfo.CardItems)
            {
                int bornPos = it.BornPos;

                GameObject card = Instantiate(cardItem, items[bornPos].transform.position, Quaternion.identity);
                card.transform.parent = items[bornPos].transform;
                card.transform.localPosition = Vector3.zero;

            }
        }
    }

    public void InitCard(CampInfo campInfo)
    {
        Init(campInfo);
    }


    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="msg"></param>
    public void MsgHandle(MsgPackage msg)
    {
        if ((msg.initItemsPack != null) && (msg.initItemsPack.cardItems != null))
        {
            foreach (Card it in msg.initItemsPack.cardItems)
            {
                if (it.isBorn)
                {
                    int bornPos = it.bornPos;

                    GameObject card = Instantiate(cardItem, items[bornPos].transform.position, Quaternion.identity);
                    card.transform.parent = items[bornPos].transform;
                    card.transform.localPosition = Vector3.zero;

                    items[bornPos].GetComponentInChildren<Card>().atk = it.atk;
                    items[bornPos].GetComponentInChildren<Card>().maxHp = it.maxHp;
                    items[bornPos].GetComponentInChildren<Card>().def = it.def;
                    items[bornPos].GetComponentInChildren<Card>().speed = it.speed;
                    items[bornPos].GetComponentInChildren<Card>().bornPos = it.bornPos;
                }
            }

        }
    }


    /// <summary>
    /// 销毁在场单位
    /// </summary>
    public void ReSet()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].transform.childCount > 0)
            {
                items[i].GetComponentInChildren<CardItem>().DestoryItem();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
