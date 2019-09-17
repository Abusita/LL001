using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleControl : MonoBehaviour {

    public GameObject[] Groups = null;
    
    //临时数据
    int maxGroups = 2;
    int maxItems = 16;
    int bornStandard = 50;


    int roundIndex = 0;
    int setpIndex = 0;
    BattleData battleData;
    List<MsgPackage> msgPackage; //消息包


    /// <summary>
    /// 速度列表Item
    /// </summary>
    public class SpeedListItem
    {
        public CommonData.Camps camp = CommonData.Camps.player;
        public Card card;

        public int cardID = 0;
        public float speed = 0;
    };

    
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData()
    {
        battleData = new BattleData();

        Random.InitState((int)System.DateTime.Now.Ticks);
        for (int i = 0; i < maxGroups; i++)
        {
            
            BattleData.Role role = new BattleData.Role();
            int idIndex = 0;
            for (int j = 0; j < maxItems / 2; j++)
            {
                Card card = new Card();

                float rd;

                if ((role.cardList.Count == 0) && (j == maxItems / 2 - 1))
                    rd = 100;
                else
                    rd = Random.Range(0, 100);
                if (rd > bornStandard)
                {
                    card.isBorn = true;
                    card.maxHp = Mathf.Min(rd, 10);
                    card.atk = Mathf.Max(rd, 60, 80);
                    card.def = Mathf.Max(rd, 50);
                    card.speed = rd;
                    card.bornPos = j;
                    card.id = idIndex;
                    role.cardList.Add(card);
                    idIndex++;
                }
                else
                    card.isBorn = false;
                
            }
            role.roleID = i;
            battleData.roleList.Add(role);
        }

    }

    /// <summary>
    /// 初始化主接口
    /// </summary>
    public void Init()
    {
        msgPackage = new List<MsgPackage>();

        InitData();
        for (int i = 0; i < battleData.roleList.Count; i++)
        {
            MsgPackage.InitItemsPack initItemMsg = new MsgPackage.InitItemsPack();

            for (int j = 0; j < battleData.roleList[i].cardList.Count; j++)
            {
                initItemMsg.cardItems.Add(battleData.roleList[i].cardList[j]);
            }
            initItemMsg.itemsCount = battleData.roleList[i].cardList.Count;

            MsgPackage msg = new MsgPackage();
            msg.initItemsPack = initItemMsg;
            msgPackage.Add(msg);
            Groups[i].GetComponent<playerItems>().MsgHandle(msgPackage[i]);
        }
        msgPackage.Clear();
    }


    /// <summary>
    /// 初始化战斗
    /// </summary>
    public void Battle()
    {
        MsgPackage.BattleGroup battleGroup_0 = new MsgPackage.BattleGroup();

        #region == Creat SpeedList ==

        List<SpeedListItem> speedList = new List<SpeedListItem>();
        int atkItemNum = battleData.roleList[(int)CommonData.Camps.player].cardList.Count;
        int defItemNum = battleData.roleList[(int)CommonData.Camps.enemy].cardList.Count;

        for (int i = 0; i < maxGroups; i++)
        {
            foreach (Card cr in battleData.roleList[i].cardList)
            {
                SpeedListItem sli = new SpeedListItem();
                sli.camp = (CommonData.Camps)i;
                sli.card = cr;
                speedList.Add(sli);
            }
        }
        speedList.Sort((x, y) => { return x.card.speed.CompareTo(y.card.speed); });

        #endregion


        #region == Creat SpeedItemIndex ==

        List<List<int>> speedItemIndex = new List<List<int>>();
        for (int i = 0; i < maxGroups; i++)
        {
            List<int> sii = new List<int>();
            speedItemIndex.Add(sii);
        }

        for (int j = 0; j < speedList.Count; j++)
        {
            speedItemIndex[(int)speedList[j].camp].Add(j);
        }
            
        #endregion

   
        #region == CreateBattleMsg ==
        bool isBattleEnd = false;
        List<MsgPackage.BattleGroup> battleGroupList = new List<MsgPackage.BattleGroup>();
        MsgPackage.BattleGroup battleGroup = new MsgPackage.BattleGroup();
        battleGroup.rounds = new List<MsgPackage.BattleGroup.Round>();

        Random.InitState((int)System.DateTime.Now.Ticks);
        while (!isBattleEnd)
        {
            MsgPackage.BattleGroup.Round round = new MsgPackage.BattleGroup.Round();
            for (int i = 0; i < speedList.Count; i++)
            {
                int taregtItem = -1;
                int targetCamp = -1;

                if (speedList[i].camp == CommonData.Camps.player)
                    targetCamp = (int)CommonData.Camps.enemy;
                if (speedList[i].camp == CommonData.Camps.enemy)
                    targetCamp = (int)CommonData.Camps.player;

                taregtItem = Random.Range(0, speedItemIndex[targetCamp].Count - 1);

                float atk = speedList[i].card.atk;
                float def = speedList[speedItemIndex[targetCamp][taregtItem]].card.def;
                float maxHp = speedList[speedItemIndex[targetCamp][taregtItem]].card.maxHp;
                float curHp = Mathf.Max(0, maxHp - 5);
                speedList[speedItemIndex[targetCamp][taregtItem]].card.maxHp = curHp;


                #region == Pack BattleMsg ==
                MsgPackage.BattleGroup.StepResult stepRes = new MsgPackage.BattleGroup.StepResult();
                MsgPackage.BattleGroup.AttributeResult attrRes = new MsgPackage.BattleGroup.AttributeResult();
                MsgPackage.BattleGroup.ActiveItem atkItem = new MsgPackage.BattleGroup.ActiveItem();
                MsgPackage.BattleGroup.ActiveItem defItem = new MsgPackage.BattleGroup.ActiveItem();
                attrRes.attr = CommonData.CardAttribute.maxHp;
                attrRes.value = -5;

                stepRes.attrResList.Add(attrRes);
                stepRes.atkType = CommonData.AtkType.atk;

                atkItem.camps = speedList[i].camp;
                atkItem.card = speedList[i].card;
                defItem.camps = speedList[speedItemIndex[targetCamp][taregtItem]].camp;
                defItem.card = speedList[speedItemIndex[targetCamp][taregtItem]].card;
                MsgPackage.BattleGroup.Step step = new MsgPackage.BattleGroup.Step();

                step.atkItem = atkItem;
                step.defItem = defItem;

                step.stepResList.Add(stepRes);
                round.steps.Add(step);
                #endregion

                
                if (curHp == 0)
                {
                    speedList.RemoveAt(speedItemIndex[targetCamp][taregtItem]);

                    for (int ci = 0; ci < maxGroups; ci++)
                    {
                        speedItemIndex[ci].Clear();
                    }

                    for (int ii = 0; ii < speedList.Count; ii++)
                    {
                        speedItemIndex[(int)speedList[ii].camp].Add(ii);
                    }
                }
                if ((speedItemIndex[(int)CommonData.Camps.player].Count == 0) || (speedItemIndex[(int)CommonData.Camps.enemy].Count == 0))
                {
                    isBattleEnd = true;
                    break;
                }

            }
            battleGroup.rounds.Add(round);
        }

        battleGroup.atkPlayerID = CommonData.PlayerID.Player_ONE;

        battleGroupList.Add(battleGroup);
        MsgPackage msg = new MsgPackage();
        msg.battlePack.battleGroupList = battleGroupList;
        msgPackage.Clear();
        msgPackage.Add(msg);

        #endregion

        //开始演示
        RunByStep();
    }

    /// <summary>
    /// 重置所有Item的tag
    /// </summary>
    public void ResetItemTag()
    {
        for(int i = 0; i < Groups.Length; i++)
        {
            foreach (GameObject item in Groups[i].GetComponent<playerItems>().items)
                foreach (Transform child in item.transform)
                    child.tag = "Untagged";
        }

    }

    /// <summary>
    /// 战斗演示
    /// </summary>
    public void RunByStep()
    {
        MsgPackage.BattleGroup localGroup = msgPackage[0].battlePack.battleGroupList[0];

        if (setpIndex == localGroup.rounds[roundIndex].steps.Count)
        {
            roundIndex++;
            setpIndex = 0;
        }
        if(roundIndex == localGroup.rounds.Count)
        {
            Debug.Log("本场战斗结束");
            roundIndex = 0;
            setpIndex = 0;
            return;
        }

        MsgPackage.BattleGroup.ActiveItem atkItem = localGroup.rounds[roundIndex].steps[setpIndex].atkItem;
        MsgPackage.BattleGroup.ActiveItem defItem = localGroup.rounds[roundIndex].steps[setpIndex].defItem;

        Vector3 startPos = Groups[(int)atkItem.camps].GetComponent<playerItems>().items[atkItem.card.bornPos].transform.position;
        Vector3 endPos = Groups[(int)defItem.camps].GetComponent<playerItems>().items[defItem.card.bornPos].transform.position;

        Groups[(int)atkItem.camps].GetComponent<playerItems>().items[atkItem.card.bornPos].GetComponentsInChildren<Transform>()[1].tag = "atkItem";
        Groups[(int)defItem.camps].GetComponent<playerItems>().items[defItem.card.bornPos].GetComponentsInChildren<Transform>()[1].tag = "defItem";

        Groups[(int)atkItem.camps].GetComponent<playerItems>().items[atkItem.card.bornPos].GetComponentInChildren<CardItem>().Move(endPos - startPos);
        
        setpIndex++;
    }


    /// <summary>
    /// 重置界面
    /// </summary>
    public void ReSet()
    {
        for (int i = 0; i < maxGroups; i++)
        {
            Groups[i].GetComponent<playerItems>().ReSet();
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
