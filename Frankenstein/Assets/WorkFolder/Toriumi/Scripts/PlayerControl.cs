using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Vector2 velocity;
    // �������Ɏg�p����
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // �A�b�v�f�[�g�̓t���[�����Ƃ�1��Ăяo�����
    void Update()
    {
        var pos = GetComponent<RectTransform>().localPosition;
        //�����L�[�������Ă���
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 1;
        }
        //�E���L�[�������Ă���
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

    //���������Ƃ�
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
