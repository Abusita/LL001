using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsProtobuf;

public class DelegateManager : MonoBehaviour {

    public delegate void UpdateEventDelegate(MsgPack msg); //更新事件委托

    public static UpdateEventDelegate UpdateBattleSceneEvent;   //更新战斗场景


    /// <summary>
    /// 更新战斗场景
    /// </summary>
    /// <param name="msg">MsgPack</param>
    public static void OnUpdateBattleSceneEvent(MsgPack msg)
    {
        if (UpdateBattleSceneEvent != null)
        {
            UpdateBattleSceneEvent(msg);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
