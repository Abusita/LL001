using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;

public class BattleControl : MonoBehaviour {

    public GameObject[] Groups = null;

    private BattleGroupPack groupPack = new BattleGroupPack();
    private GameObject[] pItems;    //主战方卡牌集合
    private GameObject[] eItems;    //对战方卡牌集合

    private int roundIndex = 0;     //回合数索引
    private int setpIndex = 0;      //步数索引

    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="msg"></param>
    public void MsgHandle(MsgPack msg)
    {
        switch (msg.MsgType)
        {
            case MsgType.CsInitbattlesceneRes:  //初始化场景回执
                InitScene(msg);
                break;
            case MsgType.CsBattlestartRes:      //开始战斗回执
                BattleSatrt(msg);
                break;
            default:
                break;
        }
    }

    public void InitScene(MsgPack msg)
    {
        if(msg.InitItemPack != null)
        {
            foreach(var item in msg.InitItemPack.CampInfos)
            {
                if(item.Camp == Camps.CsCampPlayer)
                    Groups[(int)item.Camp].GetComponent<playerItems>().InitCard(item);
                if (item.Camp == Camps.CsCampEnemy)
                    Groups[(int)item.Camp].GetComponent<enemyItems>().InitCard(item);
            }
        }
    }

    /// <summary>
    /// 打印消息内容，待删
    /// </summary>
    /// <param name="msg"></param>
    public static void showMsg(BattleGroupPack battleGroupPack)
    {
        //BattleGroupPack battleGroupPack = msg.GroupPack;

        for(int i = 0; i < battleGroupPack.Rounds.Count; i++)
        {
            for (int j = 0; j < battleGroupPack.Rounds[i].Steps.Count; j++)
            {
                Step st = battleGroupPack.Rounds[i].Steps[j];
                Debug.Log(" " + st.AtkItem.Camp + "  VS  " + st.DefItem.Camp + ": " + st.StepResList[0].AttrResList[0].ResAttr + " " + st.StepResList[0].AttrResList[0].Value);
            }
        }
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    /// <param name="msg"></param>
    public void BattleSatrt(MsgPack msg)
    {
        this.groupPack = msg.GroupPack;

        RunByStep();
    }

    /// <summary>
    /// 战斗演示
    /// </summary>
    public void RunByStep()
    {
        if (setpIndex == groupPack.Rounds[roundIndex].Steps.Count)
        {
            roundIndex++;
            setpIndex = 0;
        }
        if (roundIndex == groupPack.Rounds.Count)
        {
            Debug.Log("本场战斗结束");
            roundIndex = 0;
            setpIndex = 0;
            return;
        }

        ActiveItem atkItem = groupPack.Rounds[roundIndex].Steps[setpIndex].AtkItem;
        ActiveItem defItem = groupPack.Rounds[roundIndex].Steps[setpIndex].DefItem;


        if (atkItem.Camp == Camps.CsCampPlayer)
        {
            Vector3 startPos = pItems[atkItem.Card.BornPos].transform.position;
            Vector3 endPos = eItems[defItem.Card.BornPos].transform.position;

            pItems[atkItem.Card.BornPos].GetComponentsInChildren<Transform>()[1].tag = "atkItem";
            eItems[defItem.Card.BornPos].GetComponentsInChildren<Transform>()[1].tag = "defItem";

            pItems[atkItem.Card.BornPos].GetComponentInChildren<CardItem>().SetMoveDir(endPos - startPos);

            StepResult stepRes = groupPack.Rounds[roundIndex].Steps[setpIndex].StepResList[0];
            pItems[atkItem.Card.BornPos].GetComponentInChildren<Card>().StepAttrRes(stepRes);
            eItems[defItem.Card.BornPos].GetComponentInChildren<Card>().StepAttrRes(stepRes);
        }
        if (atkItem.Camp == Camps.CsCampEnemy)
        {
            Vector3 startPos = eItems[atkItem.Card.BornPos].transform.position;
            Vector3 endPos = pItems[defItem.Card.BornPos].transform.position;

            eItems[atkItem.Card.BornPos].GetComponentsInChildren<Transform>()[1].tag = "atkItem";
            pItems[defItem.Card.BornPos].GetComponentsInChildren<Transform>()[1].tag = "defItem";

            eItems[atkItem.Card.BornPos].GetComponentInChildren<CardItem>().SetMoveDir(endPos - startPos);

            StepResult stepRes = groupPack.Rounds[roundIndex].Steps[setpIndex].StepResList[0];
            eItems[atkItem.Card.BornPos].GetComponentInChildren<Card>().StepAttrRes(stepRes);
            pItems[defItem.Card.BornPos].GetComponentInChildren<Card>().StepAttrRes(stepRes);
        }

        setpIndex++;
    }

    /// <summary>
    /// 重置所有Item的tag
    /// </summary>
    public void ResetItemTag()
    {
        foreach (GameObject item in Groups[(int)Camps.CsCampPlayer].GetComponent<playerItems>().items)
            foreach (Transform child in item.transform)
                child.tag = "Untagged";
        foreach (GameObject item in Groups[(int)Camps.CsCampEnemy].GetComponent<enemyItems>().items)
            foreach (Transform child in item.transform)
                child.tag = "Untagged";
    }


    /// <summary>
    /// 重置界面
    /// </summary>
    public void ReSet()
    {
        Groups[0].GetComponent<playerItems>().ReSet();
        Groups[1].GetComponent<enemyItems>().ReSet();
    }

	// Use this for initialization
	void Start () {

        pItems = Groups[(int)Camps.CsCampPlayer].GetComponent<playerItems>().items;
        eItems = Groups[(int)Camps.CsCampEnemy].GetComponent<enemyItems>().items;
        DelegateManager.UpdateBattleSceneEvent += MsgHandle;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
