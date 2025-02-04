using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MmGameController : MonoBehaviour
{
    //ゲームのステータスを定義
    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state; //自作した型を扱う変数

    public MmPlayerController player; //Playerのスクリプト
    public GameObject blocks; //Blocksオブジェクト

    public int score; //得点用

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText;

    public AudioSource audioSource;
    
    public AudioClip itemSound;
    public AudioClip overSound;


    // Start is called before the first frame update
    void Start()
    {
        //開始と同時にReadyステータスにする
        Ready();

        // AudioSourceコンポーネントを取得
        AudioSource soundPlayer= GetComponent<AudioSource>();

        // オーディオソースが取得できているか確認
        if(soundPlayer != null)
        {
            // オーディオを再生
            soundPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //常にゲームのステータスをチェック
        //状況に応じてやることが変わる
        switch (state)
        {
            //もしReady状態だったら
            case State.Ready:
                //ボタンが押され次第、GameStart処理
                if (Input.GetButtonDown("Fire1")) GameStart();
                break;

            //もしPlay状態だったら
            case State.Play:
                //Azarashiを操作して何かに当たったらGameOver処理
                if (player.IsDead()) GameOver();
                break;

            //もしGameOver状態だったら
            case State.GameOver:
                //ボタンが押され次第、Reload処理
                if (Input.GetButtonDown("Fire1")) Reload();
                break;
        }
    }

    void Ready()
    {
        //ステータスをReadyにする
        state = State.Ready;

        //AzarashiのActiveでない状態にする
        //※isKinematicがtrueになる
        player.SetSteerActive(false);

        //Block達は存在しない
        blocks.SetActive(false);

        //スコアテキストを更新
        scoreText.text = "Score : " + 0;

        //ステータスの文字を表示する
        stateText.gameObject.SetActive(true);
        stateText.text = "Ready \n \n Tap to start";
    }

    void GameStart()
    {
        //ステータスをPlayにする
        state = State.Play;

        //AzarashiのActiveな状態にする（動く）
        //※isKinematicがfalseになる
        player.SetSteerActive(true);

        //Block達を存在させる
        blocks.SetActive(true);

        //Playになった時、まずは一回跳ねる
        player.Flap();

        //今から始まるのでステータスは非表示
        stateText.gameObject.SetActive(false);
        stateText.text = ""; //一応空欄にしておく

    }

    void GameOver()
    {
        //ステータスをPlayにする
        state = State.GameOver;

        //Find系メソッドで全ての「ScrollObject」コンポーネントの情報を取得（配列名 scrollObjectsへ）
        MmScrolleObject[] scrollObjects = FindObjectsOfType<MmScrolleObject>();

        //配列に特化した繰り返し構文
        //foreachを使って、配列scrollObjectsの中身
        //全てに順番に処理
        foreach (MmScrolleObject so in scrollObjects) so.enabled = false; //各々のScrollObjectスクリプトの存在をなしにする

        //ゲームオーバーというステータスを表示
        stateText.gameObject.SetActive(true);
        stateText.text = "GameOver";

        // GameOver BGM再生
        AudioSource soundPlayer = GetComponent<AudioSource>();
        soundPlayer.Stop();
        soundPlayer.PlayOneShot(overSound);
    }

    void Reload()
    {
        //SceneManagerクラスのGetActiveSceneメソッドで「今のScene情報」を取得できる。さらにその名前を取得して変数currentSceneNameへ。
        string currentSceneName = SceneManager.GetActiveScene().name;

        //Sceneの切り替え
        SceneManager.LoadScene(currentSceneName);
    }

    //他のスクリプトから呼び出される
    public void IncreaseScore()
    {
        //スコアが1増える
        score++;

        //スコア表示を更新
        scoreText.text = "Score : " + score;

    }

    public void ItemScore()
    {
        //スコアが1増える
        score += 10;

        //スコア表示を更新
        scoreText.text = "Score : " + score;

        // Item BGM再生
        AudioSource soundPlayer = GetComponent<AudioSource>();
        //soundPlayer.Stop();
        soundPlayer.PlayOneShot(itemSound);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void ChangeSceneGame()
    {
        SceneManager.LoadScene("MmMain");
    }

    public void PauseButton()
    {
        GameObject pausePanel = GameObject.Find("Pause");
        pausePanel.GetComponent<Canvas>().sortingOrder = 10;

        Time.timeScale = 0;

        //BGM停止
        AudioSource soundPlayer = GetComponent<AudioSource>();
        soundPlayer.Stop();
    }

    public void ContinueButton()
    {
        GameObject pausePanel = GameObject.Find("Pause");
        pausePanel.GetComponent<Canvas>().sortingOrder = -5;

        Time.timeScale = 1;

        //BGM再生
        AudioSource soundPlayer = GetComponent<AudioSource>();
        soundPlayer.Play();
    }

    public void RetireButton()
    {
        SceneManager.LoadScene("MmMain");
    }
}
