using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;


public class MsgHandle : MonoBehaviour {

    public static Queue<MsgPack> ReceiveMsgList;    //消息接收队列
    public static Queue<MsgPack> SendMsgList;       //消息发送队列
    public static PlayerID playerID;                //客户端身份

    // Use this for initialization
    void Start () {
        ReceiveMsgList = new Queue<MsgPack>();
        SendMsgList = new Queue<MsgPack>();
        playerID = PlayerID.CsUndefined;
    }
	
    void showMsg(MsgPack msg)
    {
        BattleGroupPack battleGroupPack = msg.GroupPack;
        foreach(Round r in battleGroupPack.Rounds)
        {

        }
    }

	// Update is called once per frame
	void Update () {
		
        if(ReceiveMsgList.Count > 0)
        {
            MsgPack msg = ReceiveMsgList.Dequeue();
            switch (msg.MsgType)
            {
                case MsgType.CsFirstHandMsg:
                    playerID = msg.MsgTo;
                    Debug.Log(playerID);
                    break;
                case MsgType.CsInitbattlesceneRes:
                    DelegateManager.OnUpdateBattleSceneEvent(msg);
                    break;
                case MsgType.CsBattlestartRes:
                    DelegateManager.OnUpdateBattleSceneEvent(msg);
                    break;
                default:
                    break;
            }
        }
        if (SendMsgList.Count > 0)
        {
            MsgPack msg = SendMsgList.Dequeue();
            ProtoControl.SendMsg(msg);
        }
    }
}
