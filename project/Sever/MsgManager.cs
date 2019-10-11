using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using CsProtobuf;


namespace Sever
{
    class MsgManager
    {

        public static Queue<MsgPack> receiveMsgList = new Queue<MsgPack>();     //接收消息队列
        public static Queue<MsgPack> sendMsgList = new Queue<MsgPack>();        //发送消息队列


        /// <summary>
        /// 消息队列处理
        /// </summary>
        private static void MsgHandle()
        {
            while (true)
            {
                if (receiveMsgList.Count > 0)
                {
                    MsgPack rm = receiveMsgList.Dequeue();
                    MsgPack sm = new MsgPack();

                    switch (rm.MsgType)
                    {
                        case MsgType.CsInitbattlesceneReq:
                            sm = DataManager.GetInitData(rm.MsgFrom);
                            sendMsgList.Enqueue(sm);
                            break;
                        case MsgType.CsBattlestartReq:
                            sm = DataManager.InitBattlePack(rm.MsgFrom);
                            sendMsgList.Enqueue(sm);
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// 启动消息管理器
        /// </summary>
        public static void Start()
        {
            //开启消息接收线程
            Thread msgThread = new Thread(MsgHandle);
            msgThread.Start();
        }

    }
}
