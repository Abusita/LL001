using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zishenzhanji : MonoBehaviour {
    //public static float[,] weizhi = new float[30,2];
    public static List<float> mPosx = new List<float>(30);
    public static List<float> mPosy = new List<float>(30);
    Transform m_transform;
    private int t = 0;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            m_transform.position = Input.mousePosition;
        }

        if (t > 30)
        {
            mPosx.RemoveAt(0);
            mPosy.RemoveAt(0);
            mPosx.Add(m_transform.position.x);
            mPosy.Add(m_transform.position.y);
        }
        else
        {
            mPosx.Add(m_transform.position.x);
            mPosy.Add(m_transform.position.y);
        }
        Debug.Log(mPosx[0]);
        t++;
    }

}