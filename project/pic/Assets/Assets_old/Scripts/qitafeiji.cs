using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qitafeiji : MonoBehaviour {
    int i;
    Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(Shuaxinweizhi());
    }

    IEnumerator Shuaxinweizhi()
    {
        yield return new WaitForSeconds(1);
        m_transform.position = new Vector3(zishenzhanji.mPosx[0], zishenzhanji.mPosy[0], 0);
    }
}
