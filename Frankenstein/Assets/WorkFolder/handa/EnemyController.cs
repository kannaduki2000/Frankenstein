using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject enemy;
    public float stopDistance;         //�~�܂�Ƃ��̋���

    private float x_val;

    private bool isFollowing = true;   //�Ǐ]���邩�ǂ���

    public PlayerController mt;

    public bool enemyMove = true;      //�G�l�~�[�̓���
    private bool enemyJump = false;         //�W�����v�p
    private bool Follow = false;       //��x�ڂ̓��͂ł̂��Ă��邩�ۂ�

    Rigidbody2D rb2d;

    public float inputSpeed;
    public float jumpingPower;
    

    //public LayerMask CollisionLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyJump = false;
    }
    //�����ɒ����܂ŃW�����v�����Ȃ��}��

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //����p�ӂ��āA���̒���Y���W������
        Vector2 targetPos = Player.transform.position;
        targetPos.y = transform.position.y;

        //����
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (isFollowing)
        {
            //if(�Ԃ̋������~�܂�Ƃ��̋����ȏ�Ȃ�?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(Player.transform.position.x, enemy.transform.position.y),
                inputSpeed * Time.deltaTime);
            }
            //enemy��player

            // �E
            if (Player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // ��
            else if (Player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            //�W�����v
            if (enemyJump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rb2d.AddForce(transform.up * this.jumpingPower);
                enemyJump = !enemyJump;
            }
        }

        //�G�l�~�[�̓����p
        if (enemyMove == false)
        {
            ////���L�[�������ꂽ�ꍇ
            //x_val = Input.GetAxis("Horizontal");
            //enemyJump = IsCollision();
            ////Space�L�[�������ꂽ�ꍇ
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

        //������̐؂�ւ�����
        //1��ڂ̐؂�ւ����̓���
        if (Input.GetKeyDown(KeyCode.F) && Follow == false)
        {
            mt.player_Move = !mt.player_Move;
            Following();
            enemyMove = !enemyMove;
            Follow = !Follow;
        }

        //2��ڂ̐؂�ւ����A�v���C���[���������ăG�l�~�[�s����
        //���̏�Ԃ��Ɖ���Enter�����Ă��v���C���[�����������
        else if (Input.GetKeyDown(KeyCode.F) && Follow == true)
        {
            isFollowing = false;
            enemyMove = true;
            mt.player_Move = false;
        }

        //�Ăԃ{�^��(Delete���u��)�����������̓���
        //Follow��؂�ւ��邱�Ƃł�����x�Ǐ]��؂�ւ����ł��邨
        if (Follow == true && Input.GetKeyDown(KeyCode.Delete))
        {
            isFollowing = true;
            Follow = !Follow;
        }
    }

    // ���ĒǏ]�̐؂�ւ�����������
    public void Following()
    {
        isFollowing = !isFollowing;
    }

    // ���đ���̐؂�ւ�����������
    public void PlayerChange()
    {
        // �v���C���[�̑�����ł��Ȃ�����
        mt.player_Move = !mt.player_Move;

        // ���쌠��G�Ɉړ�������
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
