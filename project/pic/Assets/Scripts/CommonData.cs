using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour {

    /// <summary>
    /// 玩家唯一标识符(待补充)
    /// </summary>
    public enum PlayerID
    {
        Player_ONE = 0,
        Player_TWO
    }

    /// <summary>
    /// 卡牌基本属性
    /// </summary>
    public enum CardAttribute
    {
       maxHp = 0,
       atk,
       def,
       speed,
    }

    /// <summary>
    /// 攻击类型
    /// </summary>
    public enum AtkType
    {
        atk = 0,
        skill
    }


    /// <summary>
    /// 卡牌状态
    /// </summary>
    public enum State
    {

    }


    /// <summary>
    /// 对战双方标识符
    /// </summary>
    public enum Camps
    {
        player = 0,
        enemy
    };





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
