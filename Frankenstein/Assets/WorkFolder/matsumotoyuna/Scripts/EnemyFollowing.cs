using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy; //いらんくね？
    //public Transform target;
    public float speed = 3.0f; //速度
    public float stopDistance; //止まるときの距離

    // 追従するかどうか
    private bool isFollowing = true;

    public MoveTest mt;
    public bool enemyMove = true;
    private bool Jump = false;

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトを探し出して取得するもの
        //player = GameObject.Find("player");
        //enemy = GameObject.Find("enemy");
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //箱を用意して、その中にY座標を入れる
        Vector2 targetPos = player.transform.position;
        targetPos.y = transform.position.y;

        //targetの方向に向く
        //アニメーションも同時に左右反転させたい→後ほど
        //transform.LookAt(target);
        //(targetPos);

        //距離
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (isFollowing)
        {
            //if(間の距離が止まるときの距離以上なら?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(player.transform.position.x, enemy.transform.position.y),
                speed * Time.deltaTime);
            }
            //enemy→player

            // 右
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // 左
            else if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }

        if (enemyMove == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.01f, 0.0f, 0.0f);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }

        //if(プレイヤーがジャンプした)
        //{
        //     エネミーもジャンプ;
        //}

        // 追従の切り替え処理
        //if (Input.GetKey(KeyCode.A))
        //{
        //     //GetComponent<EnemyFollowing>().enabled = false;
        //     Following();
        //}

        // 操作の切り替え処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //GetComponent<EnemyFollowing>().enabled = false;
            PlayerChange();
        }
    }

    // 追従の切り替え
    public void Following()
    {
        isFollowing = !isFollowing;
    }

    // 操作の切り替え
    public void PlayerChange()
    {
        // プレイヤーの操作をできなくする
        mt.playerMove = !mt.playerMove;

        // 操作権を敵に移動させる
        Following();
        enemyMove = !enemyMove;


        //この状態で別のボタンを押すと、操作切り替え・追従なし
        /*if(Input.GetKeyDown(KeyCode.DownArrow))
        {
           mt.playerMove = !mt.playerMove;
        }*/

        // カメラの追従を敵に移す
    }
}

/*解決
敵の追従
プレイヤーの座標と重なってはいけない！
プレイヤーとエネミーは、いくらかの間を開けるイメージ
進行方向と反対側につく？(プレイヤーが右に進んでいる時は左側につく)
enemyが消えてしまうのですが、これは…
*/

/*
Lボタンで操作切り替え
そのあとは付いてこない→機能オフ?
*/

/*
Lボタンを押した後にAボタンを押すと呼べる→機能オン
(これはいらなさそう？)
*/

/*
足の上げ下げアニメーション
*/