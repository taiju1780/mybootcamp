//-------------------------------------
// Script  : CrowMove
// Name    : カラス移動
// Creater : 大山 尚悟 (おおやま しょうご)
// Day     : 11 / 05
//-------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMove : MonoBehaviour
{
    Rigidbody rid;
    Vector3 direction;

    const int SPEED = -5;

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
        // 各初期化
        rid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        // 左向き
        gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        
        // 移動
        rid.velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        direction = new Vector3(1 * SPEED, 0, 0);

        // 時間後削除
        if (transform.position.x < player.transform.position.x - PLAYER_BACK_DELETE)
        {
            Destroy(this.gameObject);
        }
    }
}
