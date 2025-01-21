using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        //GameControllerを探す
        gameController = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //GameControllerに自分自身に向けたメッセージを出すようコントロール
        gameController.SendMessage("IncreaseScore");
    }
}
