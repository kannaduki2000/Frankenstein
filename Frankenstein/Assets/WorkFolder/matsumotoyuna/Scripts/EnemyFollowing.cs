using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy; //����񂭂ˁH
    //public Transform target;
    public float speed = 3.0f; //���x
    public float stopDistance; //�~�܂�Ƃ��̋���

    // �Ǐ]���邩�ǂ���
    private bool isFollowing = true;

    public MoveTest mt;
    public bool enemyMove = true;
    private bool Jump = false;

    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g��T���o���Ď擾�������
        //player = GameObject.Find("player");
        //enemy = GameObject.Find("enemy");
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //����p�ӂ��āA���̒���Y���W������
        Vector2 targetPos = player.transform.position;
        targetPos.y = transform.position.y;

        //target�̕����Ɍ���
        //�A�j���[�V�����������ɍ��E���]������������ق�
        //transform.LookAt(target);
        //(targetPos);

        //����
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (isFollowing)
        {
            //if(�Ԃ̋������~�܂�Ƃ��̋����ȏ�Ȃ�?)
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector2(player.transform.position.x, enemy.transform.position.y),
                speed * Time.deltaTime);
            }
            //enemy��player

            // �E
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // ��
            else if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }

        if (enemyMove == false)
        {
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

            if (Jump == false && Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                Jump = !Jump;
            }
        }

        //if(�v���C���[���W�����v����)
        //{
        //     �G�l�~�[���W�����v;
        //}

        // �Ǐ]�̐؂�ւ�����
        //if (Input.GetKey(KeyCode.A))
        //{
        //     //GetComponent<EnemyFollowing>().enabled = false;
        //     Following();
        //}

        // ����̐؂�ւ�����
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //GetComponent<EnemyFollowing>().enabled = false;
            PlayerChange();
        }
    }

    // �Ǐ]�̐؂�ւ�
    public void Following()
    {
        isFollowing = !isFollowing;
    }

    // ����̐؂�ւ�
    public void PlayerChange()
    {
        // �v���C���[�̑�����ł��Ȃ�����
        mt.playerMove = !mt.playerMove;

        // ���쌠��G�Ɉړ�������
        Following();
        enemyMove = !enemyMove;


        //���̏�Ԃŕʂ̃{�^���������ƁA����؂�ւ��E�Ǐ]�Ȃ�
        /*if(Input.GetKeyDown(KeyCode.DownArrow))
        {
           mt.playerMove = !mt.playerMove;
        }*/

        // �J�����̒Ǐ]��G�Ɉڂ�
    }
}

/*����
�G�̒Ǐ]
�v���C���[�̍��W�Əd�Ȃ��Ă͂����Ȃ��I
�v���C���[�ƃG�l�~�[�́A�����炩�̊Ԃ��J����C���[�W
�i�s�����Ɣ��Α��ɂ��H(�v���C���[���E�ɐi��ł��鎞�͍����ɂ�)
enemy�������Ă��܂��̂ł����A����́c
*/

/*
L�{�^���ő���؂�ւ�
���̂��Ƃ͕t���Ă��Ȃ����@�\�I�t?
*/

/*
L�{�^�������������A�{�^���������ƌĂׂ遨�@�\�I��
(����͂���Ȃ������H)
*/

/*
���̏グ�����A�j���[�V����
*/