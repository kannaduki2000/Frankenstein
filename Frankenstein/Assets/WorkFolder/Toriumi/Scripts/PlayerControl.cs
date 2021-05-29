using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Speed = new Vector2(1, 1);   //速度
    private int presskeyFrames = 0;             //長押しフレーム数
    private int PressLong = 300;                 //長押し判定の閾値
    private int PressShort = 100;                //軽く押した判定の閾値
    Rigidbody2D rb;
    Item item;
    



    void Start()
    {
        //Animetorコンポネーションを取得する
        //anim = GetComponent<Animator>();

    }

    // アップデートはフレームごとに1回呼び出される
    void Update()
    {
        //var pos = GetComponent<RectTransform>().localPosition;
        ////左矢印キーを押している
        //if (Input.GetKey(KeyCode.A))
        //{
        //    pos.x -= 1;
        //}
        ////右矢印キーを押している
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    pos.x += 1;
        //}
        //GetComponent<RectTransform>().localPosition = pos;
        Vector2 Position = transform.position;
        if(Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed.x;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Position.x += Speed.x;
        }
        transform.position=Position;

        float x= Input.GetAxisRaw("Horizontal");
        if(x !=0)
        {
            Vector2 Iscale = gameObject.transform.localScale;
            if((Iscale.x > 0 && x < 0) || (Iscale.x < 0 && x > 0))
            {
                Iscale.x *= -1;
                gameObject.transform.localScale = Iscale;
            }
        }

        //GameObject obj = GameObject.Find("Player");

        //Vector3 scale = obj.transform.localScale;

        //if(x > 0)
        //{
        //    scale.x = 1;
        //}
        //else if(x < 0)
        //{
        //    scale.x = -1;
        //}

        //obj.transform.localScale = scale;



        if (aa)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //スペースの判定
                presskeyFrames += (Input.GetKey(KeyCode.Space)) ? 1 : 0;
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                //もしスペースが長押しされたら
                if (PressLong <= presskeyFrames)

                //高めに投げる
                {
                    item.Hight();
                    //this.gameObject.transform.DetachChildren();
                    Debug.Log("長め");
                    this.gameObject.transform.DetachChildren();
                }

                //もしスペースが押されたら
                else if (PressShort <= presskeyFrames)

                //低めに投げる
                {
                    item.Low();
                    //this.gameObject.transform.DetachChildren();
                    Debug.Log("短め");
                    this.gameObject.transform.DetachChildren();
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
    private bool aa = false;
    //アイテムに当たったら
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //アニメーションが再生される
            Debug.Log("アニメーション再生");


            
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            aa = false;
            Debug.Log("exit");
        }
    }


    //アイテムに当たり続けたら
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Item")
        {
            Debug.Log("stay");

            //item = collision.gameObject.GetComponent<Item>();
            //Wを押していたら
            if (Input.GetKey(KeyCode.W))
            {
                aa = true;
                //アイテムクラスの取得
                item = collision.gameObject.GetComponent<Item>();

                //アイテムのY軸が上がる
                // ここでこのオブジェクトをプレイヤーの子供にする

                item.gameObject.transform.parent = this.transform;


                //item.Move();


                //if (Input.GetKey(KeyCode.Space))
                //{
                //    //スペースの判定
                //    presskeyFrames += (Input.GetKey(KeyCode.Space)) ? 1 : 0;
                //    Debug.Log(presskeyFrames);
                //}

                //if (Input.GetKeyUp(KeyCode.Space))
                //{
                //    //もしスペースが長押しされたら
                //    if (PressLong <= presskeyFrames)

                //    //高めに投げる
                //    {
                //        item.Hight();

                //        Debug.Log("長め");

                //    }

                //    //もしスペースが押されたら
                //    else if (PressShort <= presskeyFrames)

                //    //低めに投げる
                //    {
                //        item.Low();

                //        Debug.Log("短め");

                //    }
                //}

            }

            

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.gameObject.transform.DetachChildren();
        }
    }
    //キャラが反転した時アイテムだけ置いてけぼり     〇
    //アニメーションの再生、アニメーションの入れ方    〇
    //長押しの判定より短い判定のが強い              〇
    //子から元に戻らない


}
