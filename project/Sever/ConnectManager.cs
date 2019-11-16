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
        /// 监听事件
        /// </summary>
        private static void ListenClientConnect()
        {
            bool isStartSendListen = false;

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

                Console.WriteLine(newPlayerID);
                //新增客户端添加到客户端列表
                if (clientDict.ContainsKey(newPlayerID))
                    clientDict.Remove(newPlayerID);
                
                clientDict.Add(newPlayerID, clientSocket);
                clientSocket.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 3000, 1000), null);

                Console.WriteLine("connect to client: " + clientDict.Count);
                //开启消息接收线程
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);

                //开启消息接收线程
                //Thread connectedThread = new Thread(ListenConnected);
                //connectedThread.Start(clientSocket);



                if (!isStartSendListen)
                {
                    //开启消息发送线程
                    Thread sendThread = new Thread(SendMessage);
                    sendThread.Start();
                    isStartSendListen = true;
                }
            }

        }

        /// <summary>
        /// 客户端状态检测
        /// </summary>
        /// <param name="onOff">是否开启KeepAlive</param>
        /// <param name="keepAliveTime">开始首次KeepAlive探测前的TCP空闭时间</param>
        /// <param name="keepAliveInterval">两次KeepAlive探测间的时间间隔</param>
        /// <returns></returns>
        private static byte[] KeepAlive(int onOff, int keepAliveTime, int keepAliveInterval)
        {
            byte[] buffer = new byte[12];
            BitConverter.GetBytes(onOff).CopyTo(buffer, 0);
            BitConverter.GetBytes(keepAliveTime).CopyTo(buffer, 4);
            BitConverter.GetBytes(keepAliveInterval).CopyTo(buffer, 8);
            return buffer;
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

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void SendMessage()
        {
            Socket myClientSocket = null;
            while (true)
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
                    {
                        try
                        {
                            myClientSocket.Send(msg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            myClientSocket.Shutdown(SocketShutdown.Both);
                            myClientSocket.Close();
                            break;
                        }
                    }
                    else
                        Console.WriteLine("myClientSocket is null");
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

        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            try
            {
                IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress item in IpEntry.AddressList)
                {
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return item.ToString();
                    }
                }
                return "";
            }
            catch { return ""; }
        }

        /// <summary>
        /// 开启服务器
        /// </summary>
        public static void Start()
        {

            //创建连接
            IPAddress ip = IPAddress.Parse(GetLocalIP());
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));

            //客户端最大监听数量
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            //开始监听连接
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }


    }
}
