using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qitafeiji2 : MonoBehaviour {
    Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(shuaxinweizhi());
    }

    IEnumerator shuaxinweizhi()
    {
        yield return new WaitForSeconds(3);
        m_transform.position = new Vector3(zishenzhanji.mPosx[10], zishenzhanji.mPosy[10], 0);
    }
}
