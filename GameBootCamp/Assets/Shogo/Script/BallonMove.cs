//-------------------------------------
// Script  : BallonMove
// Name    : 風船移動
// Creater : 大山 尚悟 (おおやま しょうご)
// Day     : 11 / 05
//-------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonMove : MonoBehaviour
{
    Rigidbody rid;
    Vector3 direction;

    const int SPEED = -2;
    bool isPlusMinus;

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
        // 出現時の位置によってサインの向きを変更
        if(transform.position.y < 0)
        {
            isPlusMinus = false;
        }
        else
        {
            isPlusMinus = true;
        }

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
        float sin = Mathf.Sin(2 * Mathf.PI * 0.5f * Time.time);

        // 移動
        if (isPlusMinus)
        {
            direction = new Vector3(1 * SPEED, sin * 2, 0);
        }
        else
        {
            direction = new Vector3(1 * SPEED, -sin * 2, 0);
        }

        // プレイヤーの後削除
        if (transform.position.x < player.transform.position.x - PLAYER_BACK_DELETE)
        {
            Destroy(this.gameObject);
        }
    }
}
