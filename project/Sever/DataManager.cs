using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using CsProtobuf;

namespace Sever
{
    class DataManager
    {

        private static int maxCamps = 2;        //单组对战的最大阵营数(1V1)
        private static int maxItems = 16;       //战斗单位的最大数量
        private static int bornStandard = 50;

        public static Dictionary<PlayerID, BattleGroup> battleGroupDict = new Dictionary<PlayerID, BattleGroup>();

        /// <summary>
        /// 单个对战组
        /// </summary>
        public class BattleGroup
        {
            public List<CampInfo> campInfo = new List<CampInfo>();
        }

        /// <summary>
        /// 初始化场景
        /// </summary>
        /// <returns></returns>
        private static void InitData(PlayerID playerID)
        {
            BattleGroup battleGroup = new BattleGroup();
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
            if (battleGroupDict.ContainsKey(playerID))
                battleGroupDict.Remove(playerID);
            battleGroupDict.Add(playerID, battleGroup);
        }

        public static MsgPack GetInitData(PlayerID playerID)
        {
            InitData(playerID);

            MsgPack msgPack = new MsgPack();
            msgPack.MsgType = MsgType.CsInitbattlesceneRes;
            msgPack.MsgFrom = PlayerID.CsServe;
            msgPack.MsgTo = playerID;
            InitItemPack initItemPacks = new InitItemPack();

            for (int i = 0; i < battleGroupDict.Count; i++)
            {
                if ((PlayerID)i == playerID)
                {
                    List<CampInfo> campInfo = battleGroupDict[(PlayerID)i].campInfo;
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
        public static MsgPack InitBattlePack(PlayerID playerID)
        {
            MsgPack battleMsg = new MsgPack();
            for (int i = 0; i < battleGroupDict.Count; i++)
            {
                if (battleGroupDict.ContainsKey((PlayerID)i))
                {
                    BattleGroup curBattleGroup = battleGroupDict[(PlayerID)i];

                    //创建SpeedItemList，添加在场所有单位
                    List<DataBase.SpeedItemList> speedItemLists = new List<DataBase.SpeedItemList>();
                    foreach (CampInfo c in curBattleGroup.campInfo)
                    {
                        foreach (CardMsg card in c.CardItems)
                        {
                            DataBase.SpeedItemList item = new DataBase.SpeedItemList();
                            item.camp = c.Camp;
                            item.card = card;
                            speedItemLists.Add(item);
                        }
                    }

                    //SpeedItemList中元素依速度降序排序
                    speedItemLists.Sort((x, y) => { return y.card.Speed.CompareTo(x.card.Speed); });
                    foreach (DataBase.SpeedItemList s in speedItemLists)
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
                            Console.WriteLine("playerCount:  " + playerCount + "  enemyCount: " + enemyCount);
                            if ((playerCount == 0) || (enemyCount == 0))
                            {

                                isBattleEnd = true;
                                break;
                            }
                            Console.WriteLine("===========Step end========== ");
                            Console.WriteLine();
                        }
                        battleGroupPack.Rounds.Add(round);
                    }
                    battleMsg.MsgType = MsgType.CsBattlestartRes;
                    battleMsg.MsgFrom = PlayerID.CsServe;
                    battleMsg.MsgTo = playerID;
                    battleMsg.GroupPack = battleGroupPack;      
                }
            }

            return battleMsg;
        }


        public static void Start()
        {
            //初始化数据
            DataBase.Init();
        }

    }
}
