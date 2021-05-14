using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Vector2 velocity;
    // 初期化に使用する
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // アップデートはフレームごとに1回呼び出される
    void Update()
    {
        var pos = GetComponent<RectTransform>().localPosition;
        //左矢印キーを押している
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 1;
        }
        //右矢印キーを押している
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += 1;
        }
        GetComponent<RectTransform>().localPosition = pos;
        velocity.x = Input.GetAxis("Vertical") * 1.0f;
        velocity.y = Input.GetAxis("Horizontal") * 1.0f;
    }

    private void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + velocity * Time.fixedDeltaTime);
    }

    //当たったとき
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("neko");
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("nene");
            }
        }
    }
   
}
