using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCurrent : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // プレイヤー移動
        float horizontalKey = Input.GetAxis("Horizontal");

        if(horizontalKey > 0)
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

    }

    private void OnTriggerStay(Collider other) // 近づいたときの処理
    {
        if (other.CompareTag("Player"))
        {

            
        }
    }
    private void OnTriggerExit(Collider other)　// 離れた時の処理（表示を消す）
    {
        if (other.CompareTag("Player"))
        {


        }
    }
}
