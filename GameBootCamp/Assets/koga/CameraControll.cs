﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Move move;
    public Transform verRot;
    public Transform horRot;
    //カメラとプレイヤーとの差分
    private Vector3 offset;

   
    // Start is called before the first frame update
    void Start()
    {
        verRot = transform.parent;
        horRot = GetComponent<Transform>();
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float viasHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        move = player.GetComponent<Move>();

        Vector3 camerapos = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
        
        transform.position = camerapos;
    }
}
