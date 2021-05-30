using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject enemy;
    public float stopDistance;         //止まるときの距離

    private float x_val;

    private bool isFollowing = true;   //追従するかどうか

    public PlayerController mt;

    public bool enemyMove = true;      //エネミーの動き
    private bool enemyJump = false;         //ジャンプ用
    private bool Follow = false;       //二度目の入力でのついてくるか否か

    Rigidbody2D rb2d;

    public float inputSpeed;
    public float jumpingPower;
    

    //public LayerMask CollisionLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyJump = false;
    }
    //↑床に着くまでジャンプさせないマン

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //箱を用意して、その中にY座標を入れる
        Vector2 targetPos = Player.transform.position;
        targetPos.y = transform.position.y;

        //距離
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (isFollowing)
        {
            //if(間の距離が止まるときの距離以上なら?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(Player.transform.position.x, enemy.transform.position.y),
                inputSpeed * Time.deltaTime);
            }
            //enemy→player

            // 右
            if (Player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // 左
            else if (Player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            //ジャンプ
            if (enemyJump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //エネミーの動き用
        if (enemyMove == false)
        {
            ////矢印キーが押された場合
            //x_val = Input.GetAxis("Horizontal");
            //enemyJump = IsCollision();
            ////Spaceキーが押された場合
            //if (Input.GetKeyDown("space") && enemyJump)
            //{
            //    jump();
            //}
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

            if (enemyJump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //★操作の切り替え処理
        //1回目の切り替え時の動き
        if (Input.GetKeyDown(KeyCode.F) && Follow == false)
        {
            mt.player_Move = !mt.player_Move;
            Following();
            enemyMove = !enemyMove;
            Follow = !Follow;
        }

        //2回目の切り替え時、プレイヤーだけ動いてエネミー不動堂
        //この状態だと何回Enter押してもプレイヤーしか動かんで
        else if (Input.GetKeyDown(KeyCode.F) && Follow == true)
        {
            isFollowing = false;
            enemyMove = true;
            mt.player_Move = false;
        }

        //呼ぶボタン(Delete仮置き)を押した時の動き
        //Followを切り替えることでもう一度追従や切り替えができるお
        if (Follow == true && Input.GetKeyDown(KeyCode.Delete))
        {
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
        mt.player_Move = !mt.player_Move;

        // 操作権を敵に移動させる
        Following();
        enemyMove = !enemyMove;
    }

    //void jump()
    //{
    //    rb2d.AddForce(Vector2.up * jumpingPower);
    //    enemyJump = false;
    //}

    //bool IsCollision()
    //{
    //    Vector3 left_SP = transform.position - Vector3.right * 0.2f;
    //    Vector3 right_SP = transform.position + Vector3.right * 0.2f;
    //    Vector3 EP = transform.position - Vector3.up * 0.1f;
    //    //Debug.DrawLine(left_SP, EP);
    //    //Debug.DrawLine(right_SP, EP);
    //    return Physics2D.Linecast(left_SP, EP, CollisionLayer)
    //           || Physics2D.Linecast(right_SP, EP, CollisionLayer);
    //}
}
