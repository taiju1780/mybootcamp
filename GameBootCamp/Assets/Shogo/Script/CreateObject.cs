﻿//-------------------------------------
// Script  : CreateObject
// Name    : 障害物の作成
// Creater : 大山 尚悟 (おおやま しょうご)
// Day     : 11 / 04
//-------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateObject : MonoBehaviour
{
    [SerializeField, Tooltip("オブジェクト")]
    GameObject[] obj;

     // CSVファイル
    [SerializeField, Tooltip("CSVマップ")]
    TextAsset csvDatas;

    [SerializeField, Tooltip("サイズ")]
    Vector3 size;

    [SerializeField, Tooltip("間隔")]
    int interval;
    [SerializeField, Tooltip("初期位置X")]
    int intPositionX;
    [SerializeField, Tooltip("初期位置Y")]
    int intPositionY;

    //CSVの全文字列を保存する
    string str = "";
    //取り出した文字列を保存する
    string strget = "";

    //CSVデータの行数
    int gyou = 2;
    //CSVデータの列数
    int retu = 50;

    int[,] map = new int[2, 50];   //マップ番号を格納するマップ用変数
    int[] iDat = new int[15];       //文字検索用

    // 縦横
    int line; 
    int column;

    const float togeY = 6.0f;
    const float togeTogeY = 2.0f;

    enum TYPE_OBJECT
    {
        OBJECT_A,
        OBJECT_B,
        OBJECT_C,
        OBJECT_TOGE,
        OBJECT_D,
        OBJECT_E,
        OBJECT_F,
        OBJECT_TOGE_MOVE,
        GOAL,
        TAKE,
    }

    // Start is called before the first frame update
    void Start()
    {
        line = 0;
        column = 0;

        obj[(int)TYPE_OBJECT.OBJECT_A].transform.localScale = size;
        obj[(int)TYPE_OBJECT.OBJECT_B].transform.localScale = size;
        obj[(int)TYPE_OBJECT.OBJECT_C].transform.localScale = size;
        obj[(int)TYPE_OBJECT.OBJECT_D].transform.localScale = size;
        obj[(int)TYPE_OBJECT.OBJECT_E].transform.localScale = size;
        obj[(int)TYPE_OBJECT.OBJECT_F].transform.localScale = size;
        // CSVデータをstrに保存
        StringReader reader = new StringReader(csvDatas.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            str = str + "," + line;
        }

        // 最後に検索文字列の","を追記。これがないと最後の文字を取りこぼす。
        str = str + ",";

        // CSVデータをマップ配列変数mapに保存
        for (int c = 0; c < gyou; c++)
        {
            for (int i = 0; i < retu; i++)
            {
                // ","を検索
                try
                {
                    iDat[0] = str.IndexOf(",", iDat[0]);
                }
                catch { break; }

                // 次の","を検索
                try
                {
                    iDat[1] = str.IndexOf(",", iDat[0] + 1);
                }
                catch { break; }

                // 何文字取り出すか決定
                iDat[2] = iDat[1] - iDat[0] - 1;

                // iDat[2]文字ぶんだけ取り出す
                try
                {
                    strget = str.Substring(iDat[0] + 1, iDat[2]);
                }
                catch { break; }

                // 取り出した文字列を数値型に変換
                try
                {
                    iDat[3] = int.Parse(strget);
                }
                catch { break; }

                // マップ用変数に保存。１とか６とか数字が入るよ
                map[line, column] = iDat[3];
                // 一つ右のマップ用変数へ
                column++;
                // 次のインデックスへ
                iDat[0]++;
            }

            // 一つ下のマップチップへ
            line++;
            // マップチップ格納を一番左に戻す。
            column = 0;
        }

        line = 0;
        column = 0;

        for (int c = 0; c < gyou; c++)
        {
            for (int i = 0; i < retu; i++)
            {
                if (map[line, column] == 1)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_A], new Vector3(intPositionX, Random.Range(-3, 3), 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 2) 
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_B], new Vector3(intPositionX, Random.Range(-3, 3), 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 3)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_C], new Vector3(intPositionX, Random.Range(-3, 3), 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 4)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_TOGE], new Vector3(intPositionX, togeY, 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 5)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_D], new Vector3(intPositionX, 0.0f, 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 6)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_E], new Vector3(intPositionX, 0.0f, 0.0f), Quaternion.identity);
                }
                if (map[line, column] == 7)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_F], new Vector3(intPositionX, 0.0f, 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 8)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.OBJECT_TOGE_MOVE], new Vector3(intPositionX, togeTogeY, 0.0f), Quaternion.identity);
                }

                if (map[line, column] == 9)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.GOAL], new Vector3(intPositionX, 0, 0.0f), Quaternion.identity);
                }

                // 各壁の配置
                if (map[line, column] == 10)
                {
                    Instantiate(obj[(int)TYPE_OBJECT.TAKE], new Vector3(intPositionX, Random.Range(-5, 5), 0.0f), new Quaternion(0, 180, 0, 0));
                }

                // 次の右のマップ番号を読み込む
                column++;
                // 配置位置を右に移動
                intPositionX = intPositionX + interval;
            }

            // 一行終了。下の段のマップ番号を読み込んでいく
            line++;
            // 行の始めに戻るからb=0
            column = 0;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
