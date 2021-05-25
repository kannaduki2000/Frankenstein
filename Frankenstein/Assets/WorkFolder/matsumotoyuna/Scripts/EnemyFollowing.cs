using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edit→Project Settings→Physics2D下のチェック
//その前にレイヤー分けすると、
//プレイヤーとエネミーですれ違い通信ができるようになるよ！

//とてもとても非効率な書き方してる

public class EnemyFollowing : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    //public Transform target;
    public float speed = 3.0f;         //速度
    public float stopDistance;         //止まるときの距離

    private bool isFollowing = true;   //追従するかどうか

    public MoveTest mt;

    public bool enemyMove = true;      //エネミーの動き
    private bool Jump = false;         //ジャンプ用
    private bool Follow = false;       //二度目の入力でのついてくるか否か

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;          //ジャンプ力

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
    }
    //↑床に着くまでジャンプさせないマン

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

            //ジャンプ
            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }

        //エネミーの動き用
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

        // 追従の切り替え処理
        //if (Input.GetKey(KeyCode.A))
        //{
        //     //GetComponent<EnemyFollowing>().enabled = false;
        //     Following();
        //}

        //★操作の切り替え処理
        //1回目の切り替え時の動き
        if (Input.GetKeyDown(KeyCode.Return) && Follow == false)
        {
            //GetComponent<EnemyFollowing>().enabled = false;
            mt.playerMove = !mt.playerMove;
            Following();
            enemyMove = !enemyMove;
            //PlayerChange(); //私にこれを扱うことなんてできませんでした。
            Follow = !Follow;
        }

        //2回目の切り替え時、プレイヤーだけ動いてエネミー不動堂
        //この状態だと何回Enter押してもプレイヤーしか動かんで
        else if(Input.GetKeyDown(KeyCode.Return) && Follow == true)
        {
            //Following();  //こんなの知りません。
            isFollowing = false;
            enemyMove = true;
            mt.playerMove = false;
        }

        //呼ぶボタン(Delete仮置き)を押した時の動き
        //Followを切り替えることでもう一度追従や切り替えができるお
        if (Follow == true && Input.GetKeyDown(KeyCode.Delete))
        {
            //Following();  //あぁ、わからない。
            isFollowing = true;
            Follow = !Follow;
        }
    }

    // かつて追従の切り替えだったもの
    public void Following()
    {
        isFollowing = !isFollowing;
    }

    // かつて操作の切り替えだったもの
    public void PlayerChange()
    {
        // プレイヤーの操作をできなくする
        mt.playerMove = !mt.playerMove;

        // 操作権を敵に移動させる
        Following();
        enemyMove = !enemyMove;

        //この状態で元のボタンを押すと、操作切り替え・追従なし

        // カメラの追従を敵に移す→誰か頑張ってクレメンス他力本願寺
    }
}

/*解決
敵の追従
プレイヤーの座標と重なってはいけない！
プレイヤーとエネミーは、いくらかの間を開けるイメージ
進行方向と反対側につく？(プレイヤーが右に進んでいる時は左側につく)
*/

/*
Lボタンで操作切り替え
そのあとは付いてこない→機能オフ?
*/

/*
Lボタンを押した後にAボタンを押すと呼べる→機能オン
(これはいらなさそう？)→(いるらしいですよあなた)
*/

/*
足の上げ下げアニメーション→また今度
*/