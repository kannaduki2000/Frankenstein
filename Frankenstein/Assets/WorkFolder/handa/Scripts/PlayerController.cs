using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public GameObject Player;

    private float x_val;
    private float speed;

    //プレイヤーの動作の数値を入力（歩く、ジャンプ）
    public float inputSpeed;
    public float jumpingPower;

    public LayerMask CollisionLayer;
    [SerializeField] private LayerMask enemyLayer; // モック版熊倉:敵のLayer取得用
    private bool jumpFlg = false;

    //public Vector2 Speed = new Vector2(1, 1);   //速度
    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                 //長押し判定の閾値
    private int PressShort = 100;                //軽く押した判定の閾値
    private bool aa = false;
    Item item;

    [SerializeField] int maxHP = 100;
    [SerializeField] float HP = 100;
    [SerializeField] private bool touchFlag = false;
    [SerializeField] private bool enemyTouchFlag = false; // モック版熊倉:フラグ追加
    public GameObject hpCanvas;
    private float hpCanvasScale_x;

    public bool player_Move = false;

    private bool enemyFollowFlg = false;

    public GameObject enemy;

    //public EnemyController enemyCon;

    // モック版熊倉:GetCompornent重いんで直で取得、ここ敵の数増えるはずなので書き換えること
    [SerializeField] private EnemyController enemyCon;
    [SerializeField] private Image hp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hpCanvasScale_x = hpCanvas.transform.localScale.x;
        // 熊倉:ここいらないと思うで
        //enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // モック版熊倉:LayerでやってたっぽいのでLinecastで取得
        if (GetEnemyLayer())
        {
            if (enemyCon.isCharging)
            {
                enemyTouchFlag = true;
            }
        }
        else
        {
            enemyTouchFlag = false;
        }


        /*プレイヤーの移動入力処理--------------------------------------------*/
        if(player_Move == false)
        {
            //矢印キーが押された場合
            x_val = Input.GetAxis("Horizontal");
            jumpFlg = IsCollision();
            //Spaceキーが押された場合
            if (Input.GetKeyDown("space") && jumpFlg)
            {
                jump();
            }
        }
        
        /*-----------------------------------------------------------------*/

        /*拾う、投げるの入力処理----------------------------------------------------*/
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
        if (touchFlag || enemyTouchFlag)
        {
            // 表示
            hpCanvas.SetActive(true);

            // 電気を流す
            if (Input.GetKeyDown(KeyCode.Return))
            {
                HP -= 30;// HPを減らす
                // モック版熊倉:HPバー
                hp.fillAmount = HP / maxHP;
                Debug.Log(HP);
                
                // モック版熊倉:追加しますた
                // 触れている物がEnemyの場合
                if (enemyTouchFlag)
                {
                    // 追従開始
                    enemyCon.isFollowing = true;
                    // 充電したのでこれ以上充電出来ないように
                    enemyCon.isCharging = false;
                    hpCanvas.SetActive(false);
                }
                
            }
            // 電気を充電
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                HP += 30;// HPを増やす
                Debug.Log(HP);
                // ここに処理を加える

                if(enemyFollowFlg)
                {

                }
            }
        }
        // モック版熊倉:HP表示するObjectから離れたら強制的にHPバーを非表示にします
        else
        {
            hpCanvas.SetActive(false);
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
            // モック版熊倉:HPバーの向きの調整
            Vector3 hpTransform = new Vector3(hpCanvasScale_x, hpCanvas.transform.localScale.y, hpCanvas.transform.localScale.z);
            hpCanvas.transform.localScale = hpTransform;
        }
        //左に移動
        else if (x_val < 0)
        {
            speed = inputSpeed * -1;
            transform.localScale = new Vector3(-1, 1, 1);//左を向を向く
            // モック版熊倉:HPバーの向きの調整
            Vector3 hpTransform = new Vector3(-hpCanvasScale_x, hpCanvas.transform.localScale.y, hpCanvas.transform.localScale.z);
            hpCanvas.transform.localScale = hpTransform;
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
        Vector3 EP = transform.position - Vector3.up * 1.3f;
        return Physics2D.Linecast(left_SP, EP, CollisionLayer)
               || Physics2D.Linecast(right_SP, EP, CollisionLayer);
    }
    /*-------------------------------------------------------------------*/

    /// <summary>
    /// 敵のレイヤーがあるかどうかを取得する関数
    /// </summary>
    /// <returns></returns>
    private bool GetEnemyLayer()
    {
        Vector3 left = transform.position + Vector3.up * 1f - Vector3.right * 3.5f;
        Vector3 right = transform.position + Vector3.up * 1f + Vector3.right * 3.5f;
        // ここのコメント消せばデバッグ用の線が見えます
        //Debug.DrawLine(left, right);
        return Physics2D.Linecast(left, right, enemyLayer);
    }


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

    /*HPバーを表示するタグの判定-----------------------------------------*/
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
            hpCanvas.SetActive(false);
        }

        

    }
    /*-------------------------------------------------------------------*/

}
