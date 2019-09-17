using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : MonoBehaviour {

    [HideInInspector]
    public float moveSpeed = 10.0f;

    Rigidbody2D rd;
    SpriteRenderer sr;

    //移动方向向量
    Vector3 moveDir;
    //起点位置
    Vector3 startPos;

    bool isMove;
    bool isReturn;

    public void DestoryItem()
    {
        GameObject.Destroy(this.gameObject);
    }


	// Use this for initialization
	void Start () {
        this.rd = this.GetComponent<Rigidbody2D>();
        this.sr = this.GetComponent<SpriteRenderer>();

        this.moveDir = new Vector3(0.0f, 0.0f, 0.0f);
        this.startPos = transform.position;
        this.isMove = false;
        this.isReturn = false;
	}

    public void Move(Vector3 moveDir)
    {
        this.moveDir = moveDir.normalized;
        this.isMove = true;
        this.sr.sortingOrder = 1;
    }


	// Update is called once per frame
	void FixedUpdate () {

        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        if(isMove)
        {
            if (isReturn)
            {
                moveDir = startPos - transform.position;
                this.moveDir = moveDir.normalized;
                if ((Mathf.Abs(transform.position.x - startPos.x) < 0.1) && (Mathf.Abs(transform.position.y - startPos.y) < 0.1))
                {
                    transform.position = startPos;
                    moveDir = new Vector3(0.0f, 0.0f, 0.0f);
                    isMove = false;
                    isReturn = false;
                    sr.sortingOrder = 0;

                    GameObject.FindGameObjectWithTag("BattleControl").GetComponent<BattleControl>().ResetItemTag();
                    GameObject.FindGameObjectWithTag("BattleControl").GetComponent<BattleControl>().RunByStep();
                }
            }
            transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    #region == OnCollisionEvent ==

    void OnTriggerEnter2D(Collider2D col)
    {
        
        //作为非攻击者
        if (col.gameObject.tag == "atkItem")
        {
            if (gameObject.tag == "defItem")
            {        
                gameObject.GetComponent<Card>().maxHp = Mathf.Max(0, gameObject.GetComponent<Card>().maxHp - 5);
                if (gameObject.GetComponent<Card>().maxHp == 0)
                {
                    GameObject.Destroy(this.gameObject);
                }
            }

        }

        //作为攻击者
        if (col.gameObject.tag == "defItem")
        {
            isReturn = true;  
            moveDir = startPos - transform.position;
        }
    }
    #endregion

}
