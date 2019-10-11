using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using CsProtobuf;

namespace Sever
{
    class ConnectManager
    {
        private static int myProt = 8885;
        private static Socket serverSocket;

        public static Dictionary<PlayerID, Socket> clientDict = new Dictionary<PlayerID, Socket>();


        /// <summary>
        /// 开启服务器
        /// </summary>
        public static void Start()
        {
            //创建连接
            IPAddress ip = IPAddress.Parse("10.0.117.46");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));

            //客户端最大监听数量
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            //开始监听连接
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }


        /// <summary>
        /// 监听事件
        /// </summary>
        static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();

                Console.WriteLine("new connect");
                //断线重连
                PlayerID newPlayerID = ContainsPlayer(clientSocket);
                if (newPlayerID == PlayerID.CsUndefined)
                    newPlayerID = (PlayerID)clientDict.Count; ;

                //接收客户端连接请求,并回执
                MsgPack msgPack = new MsgPack();
                msgPack.MsgType = MsgType.CsFirstHandMsg;
                msgPack.MsgFrom = PlayerID.CsServe;
                msgPack.MsgTo = newPlayerID;
                var serMsgPack = ProtoSerialize.Serialize<MsgPack>(msgPack);
                clientSocket.Send(serMsgPack);

                //新增客户端添加到客户端列表
                if (clientDict.ContainsKey(newPlayerID))
                    clientDict.Remove(newPlayerID);
                clientDict.Add(newPlayerID, clientSocket);
                Console.WriteLine("connect to client: " + clientDict.Count);

                //开启消息接收线程
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
                //开启消息发送线程
                Thread sendThread = new Thread(SendMessage);
                sendThread.Start(clientSocket);
                //开启消息处理线程
                //Thread msgListHandle = new Thread(MsgHandle);
                //msgListHandle.Start();
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = null;
            //Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                for (int i = 0; i < clientDict.Count; i++)
                {
                    myClientSocket = clientDict[(PlayerID)i];
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
                        Console.WriteLine("rescive");
                        MsgPack msg = ProtoSerialize.Deserialize<MsgPack>(resultBuffer);
                        MsgManager.receiveMsgList.Enqueue(msg);
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
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void SendMessage(object clientSocket)
        {
            Socket myClientSocket = null;
            while (true)
            {
                try
                {
                    if (MsgManager.sendMsgList.Count > 0)
                    {
                        var serMsgPack = MsgManager.sendMsgList.Dequeue();
                        var msg = ProtoSerialize.Serialize<MsgPack>(serMsgPack);

                        //筛选出消息发往的客户端
                        foreach (KeyValuePair<PlayerID, Socket> item in clientDict)
                        {
                            if (item.Key == serMsgPack.MsgTo)
                            {
                                myClientSocket = item.Value;
                                break;
                            }

                        }
                        if (myClientSocket != null)
                            myClientSocket.Send(msg);
                        else
                            Console.WriteLine("ERROR: myClientSocket is null!");
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


        /// <summary>
        /// 判断存储的客户端列表中是否已存在该客户端
        /// 存在则返回对应的Key
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <returns>新增连接</returns>
        static PlayerID ContainsPlayer(Socket clientSocket)
        {
            string[] iepC = clientSocket.RemoteEndPoint.ToString().Split(':');
            foreach (var i in clientDict)
            {
                string[] iepS = i.Value.RemoteEndPoint.ToString().Split(':');
                if (iepC[0] == iepS[0])
                    return i.Key;
            }
            return PlayerID.CsUndefined;
        }

    }
}
