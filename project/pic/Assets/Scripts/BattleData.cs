using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour {

    public class Role
    {
        public CommonData.Camps camp;
        public int roleID = 0;
        public List<Card> cardList = new List<Card>();
    }

    public class BattleGroup
    {
        public Role atkRole;
        public Role defRole;
    }


    public List<Role> roleList = new List<Role>();
    public List<BattleGroup> battleGroupList = new List<BattleGroup>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
