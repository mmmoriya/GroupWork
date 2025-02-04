using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MmItem : MonoBehaviour
{
    public Sprite[] fruits;
    // Start is called before the first frame update
    void Start()
    {
        // 0からスプライトの配列 fruits[] に入っているスプライトの個数未満のランダムな整数値を取得する
        int index = Random.Range(0, fruits.Length);

        // このスクリプトがアタッチされているGameObjectの SpriteRenderer コンポーネントを取得し
        // SpriteRenderer コンポーネントの sprite にfruits[] の指定の位置に入っているスプライトをセットする
        GetComponent<SpriteRenderer>().sprite = fruits[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
