using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MmScrolleObject : MonoBehaviour
{
    public float speed = 1.0f; //オブジェクトの移動スピード
    public float startPosition; //ローテーションのスタート位置
    public float endPosition; //スタートに戻されるまでの最終地点

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //常にX軸のマイナス方向に動く
        //-1 * speed
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        //もしendPositionに設定した座標に到達したらScrollEndメソッドの力でスタート地点に戻る
        if (transform.position.x <= endPosition) ScrollEnd();
    }

    //オブジェクトをスタート地点に戻すメソッド
    void ScrollEnd()
    {
        //微妙に行き過ぎた座標がないか記録
        //実際の座標 - endPosition の差をとっておく
        float diff = transform.position.x - endPosition;

        //移動前に一度、現在のx,y,z座標の記録を取っておく(後にyとzは同じ値を指定するため）
        Vector3 restartPosition = transform.position;

        //x座標はスタート地点 + 最後に行き過ぎてしまった距離（微調整のため)に書き換え
        restartPosition.x = startPosition + diff;

        //準備した数値を実際の座標にする（実際にスタート地点に行く）
        transform.position = restartPosition;

        //後に使う
        //スタート地点に移動したということを特定のスクリプトに向けて情報発信する
        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}
