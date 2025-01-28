using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MmTitleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void ChangeSceneSub()
    {
        SceneManager.LoadScene("MmSubTitle");
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
    }

    public void ContinueButton()
    {
        GameObject pausePanel = GameObject.Find("Pause");
        pausePanel.GetComponent<Canvas>().sortingOrder = -5;

        Time.timeScale = 1;
    }

    public void RetireButton()
    {
        SceneManager.LoadScene("MmMain");
    }
}
