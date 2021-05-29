using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private float x_val;
    private float speed;

    //�v���C���[�̓���̐��l����́i�����A�W�����v�j
    public float inputSpeed;
    public float jumpingPower;

    public LayerMask CollisionLayer;
    private bool jumpFlg = false;

    public Vector2 Speed = new Vector2(1, 1);   //���x
    private int presskeyFrames = 0;             //�������t���[����
    private int PressLong = 300;                 //�����������臒l
    private int PressShort = 100;                //�y�������������臒l
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
        /*�v���C���[�̈ړ����͏���--------------------------------------------*/
        //���L�[�������ꂽ�ꍇ
        x_val = Input.GetAxis("Horizontal");
        jumpFlg = IsCollision();
        //Space�L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown("space") && jumpFlg)
        {
            jump();
        }
        /*-----------------------------------------------------------------*/

        /*�E���A�����鏈��----------------------------------------------------*/
        if (aa)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //�X�y�[�X�̔���
                presskeyFrames += (Input.GetKey(KeyCode.LeftShift)) ? 1 : 0;
                Debug.Log(presskeyFrames);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //�����X�y�[�X�����������ꂽ�獂�߂ɓ�����
                if (PressLong <= presskeyFrames)
                {
                    item.Hight();
                    Debug.Log("����");
                    this.gameObject.transform.DetachChildren();
                }
                //�����X�y�[�X�������ꂽ���߂ɓ�����
                else if (PressShort <= presskeyFrames)
                {
                    item.Low();
                    Debug.Log("�Z��");
                    this.gameObject.transform.DetachChildren();
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                this.gameObject.transform.DetachChildren();
            }
        }
        /*-----------------------------------------------------------------*/

        /*�̗͂̌�������-----------------------------------------------------------------*/
        if (touchFlag)
        {
            //Debug.Log("aaa");
            // �\��
            hpBar.SetActive(true);

            // �d�C�𗬂�
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // HP�����炷
                HP -= 30;
                Debug.Log(HP);
                // �����ɏ�����������
            }
            // �d�C���[�d
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // HP�𑝂₷
                HP += 30;
                Debug.Log(HP);
                // �����ɏ�����������
            }

        }
        /*-----------------------------------------------------------------*/
    }

    /*�v���C���[�̕�������--------------------------------------------------*/
    void FixedUpdate()
    {
        //�ҋ@
        if (x_val == 0)
        {
            speed = 0;
        }
        //�E�Ɉړ�
        else if (x_val > 0)
        {
            speed = inputSpeed;
            transform.localScale = new Vector3(1, 1, 1);//�E����������
        }
        //���Ɉړ�
        else if (x_val < 0)
        {
            speed = inputSpeed * -1;
            transform.localScale = new Vector3(-1, 1, 1);//������������
        }
        // �L�����N�^�[���ړ� Vextor2(x���X�s�[�h�Ay���X�s�[�h(���̂܂�))
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

    /*�����W�����v��h������------------------------------------------------*/
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
    //�A�C�e���ɓ����葱������
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("stay");

            //W�������Ă�����
            if (Input.GetKey(KeyCode.W))
            {
                aa = true;
                //�A�C�e���N���X�̎擾
                item = collision.gameObject.GetComponent<Item>();

                //�A�C�e����Y�����オ��
                // �����ł��̃I�u�W�F�N�g���v���C���[�̎q���ɂ���
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
