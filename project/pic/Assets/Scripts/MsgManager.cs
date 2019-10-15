using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;


public class MsgManager : MonoBehaviour {

    public static Queue<MsgPack> ReceiveMsgList;    //消息接收队列
    public static Queue<MsgPack> SendMsgList;       //消息发送队列
    public static PlayerID playerID;                //客户端身份
	
    /// <summary>
    /// 消息处理
    /// </summary>
    void MsgHandle()
    {
        if (ReceiveMsgList.Count > 0)
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
                    //showMsg(msg);
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

    /// <summary>
    /// 打印消息内容，待删
    /// </summary>
    /// <param name="msg"></param>
    public static void showMsg(MsgPack msg)
    {
        BattleGroupPack battleGroupPack = msg.GroupPack;
        foreach(Round r in battleGroupPack.Rounds)
        {
            foreach(Step st in r.Steps)
            {
                Debug.Log(" " + st.AtkItem.Camp + "  VS  " + st.DefItem.Camp + ": " +st.StepResList[0].AttrResList[0].ResAttr + " " + st.StepResList[0].AttrResList[0].Value);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        ReceiveMsgList = new Queue<MsgPack>();
        SendMsgList = new Queue<MsgPack>();
        playerID = PlayerID.CsUndefined;
    }
    // Update is called once per frame
    void Update () {

        MsgHandle();
    }
}
