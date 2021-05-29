using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private float x_val;
    private float speed;

    //プレイヤーの動作の数値を入力（歩く、ジャンプ）
    public float inputSpeed;
    public float jumpingPower;

    public LayerMask CollisionLayer;
    private bool jumpFlg = false;

    public Vector2 Speed = new Vector2(1, 1);   //速度
    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                 //長押し判定の閾値
    private int PressShort = 100;                //軽く押した判定の閾値
    private bool aa = false;
    Item item;

    int HP = 100;

    private bool touchFlag = false;

    public GameObject hpBar;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*プレイヤーの移動入力処理--------------------------------------------*/
        //矢印キーが押された場合
        x_val = Input.GetAxis("Horizontal");
        jumpFlg = IsCollision();
        //Spaceキーが押された場合
        if (Input.GetKeyDown("space") && jumpFlg)
        {
            jump();
        }
        /*-----------------------------------------------------------------*/

        /*拾う、投げる処理----------------------------------------------------*/
        if (aa)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //スペースの判定
                presskeyFrames += (Input.GetKey(KeyCode.LeftShift)) ? 1 : 0;
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //もしスペースが長押しされたら高めに投げる
                if (PressLong <= presskeyFrames)
                {
                    item.Hight();
                    Debug.Log("長め");
                    this.gameObject.transform.DetachChildren();
                }
                //もしスペースが押されたら低めに投げる
                else if (PressShort <= presskeyFrames)
                {
                    item.Low();
                    Debug.Log("短め");
                    this.gameObject.transform.DetachChildren();
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
        /*-----------------------------------------------------------------*/

        /*体力の減増処理-----------------------------------------------------------------*/
        if (touchFlag)
        {
            //Debug.Log("aaa");
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
        /*-----------------------------------------------------------------*/
    }

    /*プレイヤーの方向処理--------------------------------------------------*/
    void FixedUpdate()
    {
        //待機
        if (x_val == 0)
        {
            speed = 0;
        }
        //右に移動
        else if (x_val > 0)
        {
            speed = inputSpeed;
            transform.localScale = new Vector3(1, 1, 1);//右を向を向く
        }
        //左に移動
        else if (x_val < 0)
        {
            speed = inputSpeed * -1;
            transform.localScale = new Vector3(-1, 1, 1);//左を向を向く
        }
        // キャラクターを移動 Vextor2(x軸スピード、y軸スピード(元のまま))
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    /*-----------------------------------------------------------------*/


    /*-----------------------------------------------------*/
    void jump()
    {
        rb2d.AddForce(Vector2.up * jumpingPower);
        jumpFlg = false;
    }
    /*------------------------------------------------------------------*/

    /*無限ジャンプを防ぐ処理------------------------------------------------*/
    bool IsCollision()
    {
        Vector3 left_SP = transform.position - Vector3.right * 0.2f;
        Vector3 right_SP = transform.position + Vector3.right * 0.2f;
        Vector3 EP = transform.position - Vector3.up * 0.1f;
        return Physics2D.Linecast(left_SP, EP, CollisionLayer)
               || Physics2D.Linecast(right_SP, EP, CollisionLayer);
    }
    /*-------------------------------------------------------------------*/

    /*---------------------------*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            aa = false;
            Debug.Log("exit");
        }
    }
    /*-------------------------------------------------------------------*/

    /*-------------------------------------------------------------------*/
    //アイテムに当たり続けたら
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //Wを押していたら
            if (Input.GetKey(KeyCode.W))
            {
                aa = true;
                //アイテムクラスの取得
                item = collision.gameObject.GetComponent<Item>();

                //アイテムのY軸が上がる
                // ここでこのオブジェクトをプレイヤーの子供にする
                item.gameObject.transform.parent = this.transform;
            }

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }
    /*-------------------------------------------------------------------*/
}
