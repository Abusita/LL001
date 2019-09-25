using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {


    public float maxHp = 0;
    [HideInInspector]
    public float atk = 0;
    [HideInInspector]
    public float def = 0;
    [HideInInspector]
    public float speed = 0;

    [HideInInspector]
    public int id = 0;
    [HideInInspector]
    public bool isBorn = false;
    [HideInInspector]
    public int bornPos = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
