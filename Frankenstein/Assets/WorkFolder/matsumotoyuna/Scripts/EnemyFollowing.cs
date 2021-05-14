using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public GameObject player;
    //public GameObject enemy; //����񂭂ˁH
    public Transform target;
    public float speed = 3.0f; //���x
    public float stopDistance; //�~�܂�Ƃ��̋���

    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g��T���o���Ď擾�������
        player = GameObject.Find("player");
        //enemy = GameObject.Find("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //����p�ӂ��āA���̒���Y���W������
        Vector2 targetPos = target.position;
        targetPos.y = transform.position.y;

        //target�̕����Ɍ���
        transform.LookAt(target);
        //(targetPos);

        //����
        float distance = Vector2.Distance(transform.position, target.position);

        //if(�Ԃ̋������~�܂�Ƃ��̋����ȏ�Ȃ�?)
        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
        }

        //if(�v���C���[���W�����v����)
        //{
        //     �G�l�~�[���W�����v;
        //}

        //A���������炱�̃X�N���v�g�𖳌�������(�Ǐ]���Ȃ��Ȃ�)
        //L�{�^���������ꂽ��`�ɕύX�\��
        if (Input.GetKey(KeyCode.A))
        {
             GetComponent<EnemyFollowing>().enabled = false;
        }
    }
}

/*
�G�̒Ǐ]
�v���C���[�̍��W�Əd�Ȃ��Ă͂����Ȃ��I
�v���C���[�ƃG�l�~�[�́A�����炩�̊Ԃ��J����C���[�W
�i�s�����Ɣ��Α��ɂ��H(�v���C���[���E�ɐi��ł��鎞�͍����ɂ�)
enemy����������イ�����Ă��܂��̂ł����A����́c
*/

/*
L�{�^���ő���؂�ւ�
���̂��Ƃ͕t���Ă��Ȃ����@�\�I�t
*/

/*
L�{�^�������������A�{�^���������ƌĂׂ遨�@�\�I��
(����͂���Ȃ������H)
*/