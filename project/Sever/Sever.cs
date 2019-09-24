using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using CsProtobuf;

namespace Sever
{
    class Sever
    {
        private static int myProt = 8885;

        private static int maxGroup = 2;
        private static int maxItems = 16;
        private static int bornStandard = 50;


        private static Socket serverSocket;

        public static Queue<MsgPack> receiveMsgList = new Queue<MsgPack>();
        public static Queue<MsgPack> sendMsgList = new Queue<MsgPack>();

        /// <summary>
        /// 监听事件
        /// </summary>
        static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();

                //设置客户端身份,并回执
                MsgPack msgPack = new MsgPack();
                msgPack.MsgType = MsgType.CsFirstHandMsg;
                msgPack.MsgFrom = PlayerID.CsServe;
                msgPack.MsgTo = PlayerID.CsPlayerOne;
                var serMsgPack = ProtoSerialize.Serialize<MsgPack>(msgPack);
                clientSocket.Send(serMsgPack);


                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
                Thread sendThread = new Thread(SendMessage);
                sendThread.Start(clientSocket);
                Thread msgListHandle = new Thread(MsgHandle);
                msgListHandle.Start();
            }
        }

        /// <summary>
        /// 接收消息队列处理
        /// </summary>
        private static void MsgHandle()
        {
            while (true)
            {
                if (receiveMsgList.Count != 0)
                {
                    MsgPack sm = receiveMsgList.Dequeue();

                    switch (sm.MsgType)
                    {
                        case MsgType.CsInitbattlesceneReq:
                            MsgPack m = InitData();
                            sendMsgList.Enqueue(m);
                            break;
                        case MsgType.CsBattlestartReq:
                            break;
                        case MsgType.CsBattlestartRes:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化场景
        /// </summary>
        /// <returns></returns>
        private static MsgPack InitData()
        {
            MsgPack msgPack = new MsgPack();
            msgPack.MsgType = MsgType.CsInitbattlesceneRes;
            msgPack.MsgFrom = PlayerID.CsServe;
            msgPack.MsgTo = PlayerID.CsPlayerOne;
            InitItemPack initItemPacks = new InitItemPack();

            Random rand = new Random();
            for (int i = 0; i < maxGroup; i++)
            {
                CampInfo campInfo = new CampInfo();
                int idIndex = 0;
                for (int j = 0; j < maxItems / 2; j++)
                {
                    CardMsg card = new CardMsg();
                    float rd;

                    if ((idIndex == 0) && (j == maxItems / 2 - 1))
                        rd = bornStandard + 1;
                    else
                    {

                        rd = rand.Next(0, 100);
                    }
                    if (rd > bornStandard)
                    {
                        card.IsBorn = true;
                        card.MaxHp = rd > 10 ? rd : 10;
                        card.Atk = rd > 60 ? 60 : rd;
                        card.Def = rd > 30 ? 30 : rd;
                        card.Speed = rd;
                        card.BornPos = j;
                        card.Id = idIndex;
                        idIndex++;
                        campInfo.CardItems.Add(card);

                    }
                }
                campInfo.Camp = (Camps)i;
                campInfo.ItemsCount = campInfo.CardItems.Count;
                initItemPacks.CampInfos.Add(campInfo);

            }
            msgPack.InitItemPack = initItemPacks;
            return msgPack;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    byte[] tempBuffer = new byte[1024 * 1024 * 2];
                    var effective = myClientSocket.Receive(tempBuffer);
                    byte[] resultBuffer = new byte[effective];

                    Array.Copy(tempBuffer, 0, resultBuffer, 0, effective);
                    if (effective == 0)
                    {
                        break;
                    }
                    MsgPack msg = ProtoSerialize.Deserialize<MsgPack>(resultBuffer);
                    receiveMsgList.Enqueue(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void SendMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    if (sendMsgList.Count != 0)
                    {
                        var serMsgPack = sendMsgList.Dequeue();
                        var msg = ProtoSerialize.Serialize<MsgPack>(serMsgPack);
                        myClientSocket.Send(msg);
                        Console.WriteLine("send");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }


        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }
    }
}
