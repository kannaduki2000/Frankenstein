using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public GameObject player;
    //public GameObject enemy; //いらんくね？
    public Transform target;
    public float speed = 3.0f; //速度
    public float stopDistance; //止まるときの距離

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトを探し出して取得するもの
        player = GameObject.Find("player");
        //enemy = GameObject.Find("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //箱を用意して、その中にY座標を入れる
        Vector2 targetPos = target.position;
        targetPos.y = transform.position.y;

        //targetの方向に向く
        transform.LookAt(target);
        //(targetPos);

        //距離
        float distance = Vector2.Distance(transform.position, target.position);

        //if(間の距離が止まるときの距離以上なら?)
        if (distance > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
        }

        //if(プレイヤーがジャンプした)
        //{
        //     エネミーもジャンプ;
        //}

        //Aを押したらこのスクリプトを無効化する(追従しなくなる)
        //Lボタンが押されたら～に変更予定
        if (Input.GetKey(KeyCode.A))
        {
             GetComponent<EnemyFollowing>().enabled = false;
        }
    }
}

/*
敵の追従
プレイヤーの座標と重なってはいけない！
プレイヤーとエネミーは、いくらかの間を開けるイメージ
進行方向と反対側につく？(プレイヤーが右に進んでいる時は左側につく)
enemyがしょっちゅう消えてしまうのですが、これは…
*/

/*
Lボタンで操作切り替え
そのあとは付いてこない→機能オフ
*/

/*
Lボタンを押した後にAボタンを押すと呼べる→機能オン
(これはいらなさそう？)
*/