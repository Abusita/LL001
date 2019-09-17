using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MsgPackage : MonoBehaviour 
{
    public enum Camps
    {
        player = 0,
        enemy
    };



    #region == 初始化消息包 ==
    public class InitItemsPack
    {
        public CommonData.Camps camp;

        public int itemsCount = 0;

        public List<Card> cardItems = new List<Card>();
    };
    public InitItemsPack initItemsPack = new InitItemsPack();
#endregion


    #region == 战斗消息包 ==
    public class BattleGroup
    {
        public class AttributeResult
        {
            public CommonData.CardAttribute attr;
            public float value;
        }
        public class ActiveItem
        {
            public CommonData.Camps camps;
            public Card card;
        }
        public class StepResult
        {
            public CommonData.AtkType atkType;
            public List<AttributeResult> attrResList = new List<AttributeResult>();
        }

        public class Step
        {
            public List<ActiveItem> activeItemList = new List<ActiveItem>();
            public ActiveItem atkItem = new ActiveItem();
            public ActiveItem defItem = new ActiveItem();
            public List<StepResult> stepResList = new List<StepResult>();
        }

        public class BattleResult
        {
            //本组对战结果
        }

        public class Round
        {
            public List<Step> steps = new List<Step>();
        }


        public CommonData.PlayerID atkPlayerID;

        public List<Round> rounds = new List<Round>();
        public BattleResult groupRes = new BattleResult();
    }

    public class BattlePack
    {
        public List<BattleGroup> battleGroupList = new List<BattleGroup>();

    }
    public BattlePack battlePack = new BattlePack();
    #endregion


}
