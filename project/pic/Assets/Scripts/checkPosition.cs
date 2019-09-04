using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPosition : MonoBehaviour {
    Transform m_transform;

    // Use this for initialization
    void Start () {
        m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        print(m_transform.childCount);
    }
}
