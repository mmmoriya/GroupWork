using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MmPlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;　//そのフレームそのフレームの子オブジェクトの角度
    bool isDead; //死亡判定 ※初期化しないとfalse

    public float maxHeight; //どこまで上にいけるかの上限
    public float flapVelocity; //上向きの力

    public float relativeVelocityX;
    public GameObject sprite; //子オブジェクトを指定

    public MmGameController score;
    public TextMeshProUGUI scoreText;

    GameObject gameController;

    //死亡判定がどうなっているかのチェック
    public bool IsDead()
    {
        return isDead;
    }

    void Awake()
    {
        //Startより前の段階でRigidbody2D
        rb2d = GetComponent<Rigidbody2D>();
        //子オブジェクトのAnimator
        animator = sprite.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameControllerを探す
        gameController = GameObject.FindWithTag("GameController");

    }

    // Update is called once per frame
    void Update()
    {
        //最高高度に達していない場合に限りタップの入力を受け付ける
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }

        //常に角度を反映(変数angleの数字が決まっている)
        ApplyAngle();

        //angleが水平以上 かつ　死亡判定がfalseだったらflapアニメにする
        //animator.SetBool("flap", angle >= 0.0f && !isDead);
    }

    //上空にはばたく動きのメソッド
    public void Flap()
    {
        //もしも死亡判定がtrueなら何も出来ない
        if (isDead) return;

        //重力が効いていない時も何も出来ない
        if (rb2d.isKinematic) return;

        //Velocityに力を与えて上方向に動かす
        rb2d.velocity = new Vector2(0.0f, flapVelocity);
    }

    //AzarashiSpriteが向くべき角度にする
    void ApplyAngle()
    {
        float targetAngle;

        //もしも死亡判定がtrueなら
        if (isDead)
        {
            //ひっくり返る
            targetAngle = 180.0f;
        }
        else
        {
            //現在の速度（高さ）、相対速度(底辺：3固定)から向くべき角度を求める
            targetAngle = Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
        }


        //目的地に向けた角度の調整（スムージング）
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //実際に向きに反映させる
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    //何かに衝突したら
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
         {
            //すでに死亡判定をtrueにした後は何もしない
            if (isDead) return;

            //死亡判定をtrue
            isDead = true;
        } 

        else if(collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);

            gameController.SendMessage("ItemScore");
        }
    }


    //AzarashiのActiveの状態を切り替える
    public void SetSteerActive(bool active)
    {
        rb2d.isKinematic = !active;
    }
}
