using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;


public class MsgHandle : MonoBehaviour {

    public static Queue<MsgPack> ReceiveMsgList;
    public static Queue<MsgPack> SendMsgList;
    public static PlayerID playerID;

    // Use this for initialization
    void Start () {
        ReceiveMsgList = new Queue<MsgPack>();
        SendMsgList = new Queue<MsgPack>();
        playerID = PlayerID.CsUndefined;
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
                    break;
                case MsgType.CsInitbattlesceneRes:
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
