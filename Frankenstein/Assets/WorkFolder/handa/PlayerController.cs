using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        /*�v���C���[�̈ړ����͏���*/
        //���L�[�������ꂽ�ꍇ
        x_val = Input.GetAxis("Horizontal");
        jumpFlg = IsCollision();
        //Space�L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown("space") && jumpFlg)
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        /*�v���C���[�̕�������*/
        //�ҋ@
        if (x_val == 0)
        {
            speed = 0;
        }
        //�E�Ɉړ�
        else if (x_val > 0)
        {
            speed = inputSpeed;
            //�E����������
            transform.localScale = new Vector3(1, 1, 1);
        }
        //���Ɉړ�
        else if (x_val < 0)
        {
            speed = inputSpeed * -1;
            //������������
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // �L�����N�^�[���ړ� Vextor2(x���X�s�[�h�Ay���X�s�[�h(���̂܂�))
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
        return Physics2D.Linecast(left_SP, EP, CollisionLayer)
               || Physics2D.Linecast(right_SP, EP, CollisionLayer);
    }

}
