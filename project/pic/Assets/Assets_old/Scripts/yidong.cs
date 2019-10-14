using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yidong : MonoBehaviour {
    Transform m_transform;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(100, 100, 100) * Time.deltaTime);
    }
}
