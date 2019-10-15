using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : MonoBehaviour {

    [HideInInspector]
    public float moveSpeed = 10.0f;     //移动速度
    private SpriteRenderer sr;          //Renderer组件，改变Sprite层级
    private Vector3 moveDir;            //移动方向向量
    private Vector3 startPos;           //起点位置
    private bool isMove;                //是否移动
    private bool isReturn;              //是否归位

    /// <summary>
    /// 销毁
    /// </summary>
    public void DestoryItem()
    {
        GameObject.Destroy(this.gameObject);
    }

    /// <summary>
    /// 设置初始移动方向
    /// </summary>
    /// <param name="moveDir">方向向量</param>
    public void SetMoveDir(Vector3 moveDir)
    {
        this.moveDir = moveDir.normalized;
        this.isMove = true;
        this.sr.sortingOrder = 1;
    }

    /// <summary>
    /// 控制卡牌移动
    /// </summary>
    void Move()
    {
        if (isMove)
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


    // Use this for initialization
    void Start()
    {
        this.sr = this.GetComponent<SpriteRenderer>();

        this.moveDir = new Vector3(0.0f, 0.0f, 0.0f);
        this.startPos = transform.position;
        this.isMove = false;
        this.isReturn = false;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move();
    }

    #region == OnCollisionEvent ==

    void OnTriggerEnter2D(Collider2D col)
    {
        //作为非攻击者
        if (col.gameObject.tag == "atkItem")
        {
            if (gameObject.tag == "defItem")
            {
                gameObject.GetComponent<Card>().OnCollisionHandle(); ;
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
