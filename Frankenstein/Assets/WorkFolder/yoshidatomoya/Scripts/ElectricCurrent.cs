using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCurrent : MonoBehaviour
{
    // HPを表示
    int HP = 100;

    public float speed;
    private Rigidbody2D rb;

    private bool touchFlag = false;

    public GameObject hpBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // プレイヤー移動
        float horizontalKey = Input.GetAxis("Horizontal");

        if (horizontalKey > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (horizontalKey < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (touchFlag)
        {
            Debug.Log("aaa");
            // 表示
            hpBar.SetActive(true);

            // 電気を流す
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // HPを減らす
                HP -= 30;
                Debug.Log(HP);
                // ここに処理を加える
            }
            // 電気を充電
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // HPを増やす
                HP += 30;
                Debug.Log(HP);
                // ここに処理を加える
            }


        }


        // 非表示

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = true;   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HomeApp")
        {
            touchFlag = false;
            hpBar.SetActive(false);
        }
    }
}

/*
＠電流関係
    電気を流す〇
　　電気を充電する〇
　　電気を流せるかどうかの判定〇
　　　流せるなら流せる対象の状態（電気を流すor充電）を取得〇
　　 　種類に合わせた処理をする
　　　　（敵ならその敵に応じた電気を消費して追従）
　　　　（ギミックならそのギミックに応じた処理）
　　電気を流せる物の近くに立ったらHPバーとボタンの表示 〇
*/