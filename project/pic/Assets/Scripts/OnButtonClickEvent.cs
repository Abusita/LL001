using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;


public class OnButtonClickEvent : MonoBehaviour {


    /// <summary>
    /// 初始化场景
    /// </summary>
    public void OnButtonInit()
    {
        //GameObject.Find("BattleControl").GetComponent<BattleControl>().Init();

        MsgPack msg = new MsgPack();
        msg.MsgType = MsgType.CsInitbattlesceneReq;
        msg.MsgFrom = MsgHandle.playerID;
        msg.MsgTo = PlayerID.CsServe;
        MsgHandle.SendMsgList.Enqueue(msg);
    }

    /// <summary>
    /// 重置场景
    /// </summary>
    public void OnButtonReSet()
    {
        GameObject.Find("BattleControl").GetComponent<BattleControl>().ReSet();
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    public void OnButtonBattle()
    {
        GameObject.Find("BattleControl").GetComponent<BattleControl>().Battle();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
