//-------------------------------------
// Script  : SpaceAsteroidMove
// Name    : 小惑星の移動
// Creater : 大山 尚悟 (おおやま しょうご)
// Day     : 11 / 06
//-------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceAsteroidMove : MonoBehaviour
{

    Vector3 targetPos;

    Rigidbody rid;
    Vector3 direction;

    const int SPEED = -8;
    const int CIRCLESPEED = 4;

    float sin;
    float cos;

    GameObject player;
    const int PLAYER_BACK_DELETE = 40;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sin = 0;
        cos = 0;
        // 各初期化
        rid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        // 移動
        rid.velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        // サインカーブ
        sin = Mathf.Sin(Time.time * 2) * CIRCLESPEED;
        cos = Mathf.Cos(Time.time * 2) * CIRCLESPEED;

        // 移動
        direction = new Vector3(SPEED + sin, cos, 0);

        // 時間後削除
        if (transform.position.x < player.transform.position.x - PLAYER_BACK_DELETE)
        {
            Destroy(this.gameObject);
        }
    }
}
