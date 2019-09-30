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
        /*public class SpeedListItem
        {
            public Camps camp = Camps.CsCampPlayer;
            public CardMsg card = new CardMsg();

            public int cardID = 0;
            public float speed = 0;

        };*/


        private static int myProt = 8885;

        private static int maxCamps = 2;
        private static int maxItems = 16;
        private static int bornStandard = 50;


        private static Socket serverSocket;

        public static Queue<MsgPack> receiveMsgList = new Queue<MsgPack>();
        public static Queue<MsgPack> sendMsgList = new Queue<MsgPack>();
        public static Dictionary<PlayerID, Socket> clientDict = new Dictionary<PlayerID, Socket>();
        

        /// <summary>
        /// 监听事件
        /// </summary>
        static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();

                //接收客户端连接请求,并回执
                MsgPack msgPack = new MsgPack();
                msgPack.MsgType = MsgType.CsFirstHandMsg;
                msgPack.MsgFrom = PlayerID.CsServe;
                msgPack.MsgTo = (PlayerID)clientDict.Count;
                var serMsgPack = ProtoSerialize.Serialize<MsgPack>(msgPack);
                clientSocket.Send(serMsgPack);
                
                clientDict.Add((PlayerID)clientDict.Count, clientSocket);
                Console.WriteLine("connect to client: " + clientDict.Count);

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
                if (receiveMsgList.Count > 0)
                {
                    MsgPack sm = receiveMsgList.Dequeue();

                    switch (sm.MsgType)
                    {
                        case MsgType.CsInitbattlesceneReq:
                            MsgPack m = GetInitData(sm.MsgFrom);                      
                            
                            sendMsgList.Enqueue(m);
                            break;
                        case MsgType.CsBattlestartReq:
                            Console.WriteLine("MsgType.CsBattlestartReq: " + sm.MsgFrom);
                            InitBattlePack(sm.MsgFrom);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /*
        private static MsgPack InitData()
        {
            MsgPack msgPack = new MsgPack();
            msgPack.MsgType = MsgType.CsInitbattlesceneRes;
            msgPack.MsgFrom = PlayerID.CsServe;
            msgPack.MsgTo = PlayerID.CsUndefined;
            InitItemPack initItemPacks = new InitItemPack();

            Random rand = new Random();
            for (int i = 0; i < maxCamps; i++)
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
        */

        /// <summary>
        /// 初始化场景
        /// </summary>
        /// <returns></returns>
        private static void InitData(PlayerID playerID)
        {
            Data.BattleGroup battleGroup = new Data.BattleGroup();
            Random rand = new Random();
            for (int i = 0; i < maxCamps; i++)
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
                        card.MaxHp = 30;
                        card.Atk = rand.Next(50, 80);
                        card.Def = rand.Next(30, 60);
                        card.Speed = rd;
                        card.BornPos = j;
                        card.Id = idIndex;
                        idIndex++;
                        campInfo.CardItems.Add(card);

                    }
                }
                campInfo.Camp = (Camps)i;
                campInfo.ItemsCount = campInfo.CardItems.Count;

                battleGroup.campInfo.Add(campInfo);

            }
            if(Data.battleGroupDict.ContainsKey(playerID))
                Data.battleGroupDict.Remove(playerID);
            Data.battleGroupDict.Add(playerID, battleGroup);
        }

        private static MsgPack GetInitData(PlayerID playerID)
        {
            InitData(playerID);
           
            MsgPack msgPack = new MsgPack();
            msgPack.MsgType = MsgType.CsInitbattlesceneRes;
            msgPack.MsgFrom = PlayerID.CsServe;
            msgPack.MsgTo = playerID;
            InitItemPack initItemPacks = new InitItemPack();

            for(int i = 0; i < Data.battleGroupDict.Count; i++)
            {
                if((PlayerID)i == playerID)
                {
                    List<CampInfo> campInfo = Data.battleGroupDict[(PlayerID)i].campInfo;
                    for (int j = 0; j < campInfo.Count; j++)
                    {
                        initItemPacks.CampInfos.Add(campInfo[j]);
                    }
                }
            }

            msgPack.InitItemPack = initItemPacks;
            return msgPack;
        }


        /// <summary>
        /// 初始化单组对战
        /// </summary>
        /// <param name="playerID"></param>
        private static void InitBattlePack(PlayerID playerID)
        { 
            for (int i = 0; i < Data.battleGroupDict.Count; i++)
            {
                if(Data.battleGroupDict.ContainsKey((PlayerID)i))
                {        
                    Data.BattleGroup curBattleGroup = Data.battleGroupDict[(PlayerID)i];

                    //创建SpeedItemList，添加在场所有单位
                    List<Data.SpeedItemList> speedItemLists = new List<Data.SpeedItemList>();
                    foreach(CampInfo c in curBattleGroup.campInfo)
                    {
                        foreach(CardMsg card in c.CardItems)
                        {
                            Data.SpeedItemList item = new Data.SpeedItemList();
                            item.camp = c.Camp;
                            item.card = card;
                            speedItemLists.Add(item);
                        }                          
                    }

                    //SpeedItemList中元素依速度降序排序
                    speedItemLists.Sort((x, y) => { return y.card.Speed.CompareTo(x.card.Speed); });
                    foreach (Data.SpeedItemList s in speedItemLists)
                    Console.WriteLine("camp: " + s.camp + "  pos: " + s.card.BornPos + "  speed: " + s.card.Speed + "  maxHp: " + s.card.MaxHp);


                    //创建消息包
                    bool isBattleEnd = false;
                    BattleGroupPack battleGroupPack = new BattleGroupPack();
                   
                    while (!isBattleEnd)
                    {
                        Round round = new Round();
                        for (int j = 0; j < speedItemLists.Count; j++)
                        {
                            Console.WriteLine("============================Step start=================== ");
                            Console.WriteLine("camp: " + speedItemLists[j].camp + " selfPos: " + speedItemLists[j].card.BornPos);

                            int targetIndex = Radar.GetTargetIndex(speedItemLists, Radar.RadarType.front_first, speedItemLists[j].camp, speedItemLists[j].card.BornPos);

                            float atk = speedItemLists[j].card.Atk;
                            float def = speedItemLists[targetIndex].card.Def;

                            float damage = Math.Max(10, atk - def);

                            speedItemLists[targetIndex].card.MaxHp = Math.Max(0, speedItemLists[targetIndex].card.MaxHp - damage);

                            //speedItemLists[targetIndex].card.MaxHp = Math.Max(0, speedItemLists[targetIndex].card.MaxHp - 5);

                            #region == Pack Round Msg ==
                            AttributeResult attributeRes = new AttributeResult();
                            attributeRes.Camp = speedItemLists[targetIndex].camp;
                            attributeRes.ResAttr = CardAttribute.CsCardMaxhp;
                            attributeRes.Value = damage;
                            Console.WriteLine("Damage is:  " + damage);
                            StepResult stepRes = new StepResult();
                            stepRes.AttrResList.Add(attributeRes);
                            stepRes.AtkType = AtkType.CsAtktypeAtk;

                            Step step = new Step();
                            step.AtkItem = new ActiveItem();
                            step.DefItem = new ActiveItem();
                            step.AtkItem.Camp = speedItemLists[j].camp;
                            step.AtkItem.Card = speedItemLists[j].card;
                            step.DefItem.Camp = speedItemLists[targetIndex].camp;
                            step.DefItem.Card = speedItemLists[targetIndex].card;
                            step.StepResList.Add(stepRes);

                            round.Steps.Add(step);
                            Console.WriteLine("Camp:  " + speedItemLists[targetIndex].camp + " maxHp is: " + speedItemLists[targetIndex].card.MaxHp);
                            #endregion

                            if (speedItemLists[targetIndex].card.MaxHp == 0)
                            {
                                Console.WriteLine("Remove:  " + speedItemLists[targetIndex].camp + " " + speedItemLists[targetIndex].card.BornPos);
                                speedItemLists.RemoveAt(targetIndex);
                                
                            }

                            int playerCount = 0;
                            int enemyCount = 0;
                            foreach (var it in speedItemLists)
                            {
                                if (it.camp == Camps.CsCampPlayer)
                                    playerCount++;
                                if (it.camp == Camps.CsCampEnemy)
                                    enemyCount++;
                            }
                            Console.WriteLine("playerCount:  " + playerCount+ "  enemyCount: " + enemyCount);
                            if ((playerCount == 0)||(enemyCount == 0))
                            {
                                
                                isBattleEnd = true;
                                break;
                            }
                            Console.WriteLine("===========Step end========== ");
                            Console.WriteLine();
                        }
                        battleGroupPack.Rounds.Add(round);
                    }
                    MsgPack msg = new MsgPack();
                    msg.MsgType = MsgType.CsBattlestartRes;
                    msg.MsgFrom = PlayerID.CsServe;
                    msg.MsgTo = playerID;
                    msg.GroupPack = battleGroupPack;

                    sendMsgList.Enqueue(msg);
                }

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
            {   for(int i = 0; i < clientDict.Count; i++)
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
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void SendMessage(object clientSocket)
        {
            Socket myClientSocket = null;
            //Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    if (sendMsgList.Count > 0)
                    {
                        var serMsgPack = sendMsgList.Dequeue();
                        var msg = ProtoSerialize.Serialize<MsgPack>(serMsgPack);

                        foreach (KeyValuePair<PlayerID, Socket> item in clientDict)
                        {
                            Console.WriteLine(item.Key);
                            Console.WriteLine(serMsgPack.MsgTo);
                            Console.WriteLine(serMsgPack.MsgType);
                            if (item.Key == serMsgPack.MsgTo)
                            {
                                myClientSocket = item.Value;
                                break;
                            }
                                
                        }
                        if(myClientSocket != null)
                            myClientSocket.Send(msg);
                        else
                            Console.WriteLine("ERROR: myClientSocket is null!");
                        //Console.WriteLine("send");
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
            Data.Init();
            IPAddress ip = IPAddress.Parse("10.0.118.46");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }
    }
}
