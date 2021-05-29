using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float x_val;
    private float speed;
    public float inputSpeed;
    public float jumpingPower;
    public LayerMask CollisionLayer;
    private bool jumpFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //矢印キーが押された場合
        x_val = Input.GetAxis("Horizontal");
        jumpFlg = IsCollision();
        //Spaceキーが押された場合
        Debug.Log(jumpFlg);
        if (Input.GetKeyDown("space") && jumpFlg)
        {
            jump();
        }
    }
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
            //右を向を向く
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
        //左に移動
        else if (x_val < 0)
        {
            speed = inputSpeed * -1;
            //左を向を向く
            transform.localScale = new Vector3(-2f, 2f, 2f);
        }
        // キャラクターを移動 Vextor2(x軸スピード、y軸スピード(元のまま))
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    void jump()
    {
        rb2d.AddForce(Vector2.up * jumpingPower);
        jumpFlg = false;
    }

    bool IsCollision()
    {
        Vector3 left_SP = transform.position - Vector3.right * 0.2f;
        Vector3 right_SP = transform.position + Vector3.right * 0.2f;
        Vector3 EP = transform.position - Vector3.up * 0.1f;
        //Debug.DrawLine(left_SP, EP);
        //Debug.DrawLine(right_SP, EP);
        return Physics2D.Linecast(left_SP, EP, CollisionLayer)
        || Physics2D.Linecast(right_SP, EP, CollisionLayer);
    }
}
