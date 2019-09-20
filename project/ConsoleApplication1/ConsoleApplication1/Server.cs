using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using Google.Protobuf;
using Jaihk662;
using CsProtobuf;


namespace ConsoleApplication1
{
    class Server
    {
        private static byte[] result = new byte[1024];
        private static int myProt = 8885;
        static Socket serverSocket;

        private static int maxGroup = 2;
        private static int maxItems = 16;
        private static int bornStandard = 50;

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));
            serverSocket.Listen(10); 
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();

            //Console.ReadLine();
        }


        /// <summary>
        /// 监听客户端链接
        /// </summary>
        static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                //clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
                Thread sendThread = new Thread(SendMessage);
                sendThread.Start(clientSocket);
            } 
        }
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据 
                    int receiveNumber = myClientSocket.Receive(result);
                    //Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
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

        private static MsgPack InitData()
        {
            MsgPack msgPack = new MsgPack();
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
                campInfo.ItemsCount = campInfo.CardItems.Count;
                initItemPacks.CampInfos.Add(campInfo);


            }
            msgPack.InitItemPack = initItemPacks;
            return msgPack;
        }

        private static void SendMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            //while (true)
            //{
                try
                {
                    MsgPack msgPack = InitData();

                    var serMsgPack = ProtoSerialize.Serialize<MsgPack>(msgPack);
                    myClientSocket.Send(serMsgPack);


                    #region == test for person ==
                    /*byte[] personMsg = new byte[1024];
                    TestForProto toServer = new TestForProto
                    {
                        Name = "test for Scoket",
                        Age = 10
                    };
                    for (int i = 3; i <= 7; i++)
                        toServer.Pos.Add(i);

                    var message = ProtoSerialize.Serialize<TestForProto>(toServer); // 得到byte[]的message
                    //将对象转换成字节数组
                    byte[] databytes = toServer.ToByteArray();
                    Console.WriteLine(Encoding.Default.GetString(message));
                    myClientSocket.Send(message);*/
                    #endregion


                    //break;
                    /*string userInput = Console.ReadLine();
                    string sendMessage = "client send Message Hellp " + DateTime.Now + ": " + userInput;
                    myClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Console.WriteLine("向客户端发送消息：{0}" + sendMessage)*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    //break;
                }
            //}
        } 

    }
}
