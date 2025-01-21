using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MmBlock : MonoBehaviour
{
    public float minHeight; //高さの下限
    public float maxHeight; //高さの上限
    public GameObject root; //高さを動かしたい対象

    // Start is called before the first frame update
    void Start()
    {
        //スタートと同時にまず高さを決める
        ChangeHeight();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Rootオブジェクトの高さをランダムにする
    void ChangeHeight()
    {
        //minHeight以上、maxHeight未満を取得
        float height = Random.Range(minHeight, maxHeight);
        root.transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }

    //メッセージを受けとったら発動
    void OnScrollEnd()
    {
        //高さをランダムに取得
        ChangeHeight();
    }
}
