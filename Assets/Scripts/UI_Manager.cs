using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Manager : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject StartPanel;
    public GameObject FailPanel;
    public GameObject Player;

    public Button StartBtn;
    public Button PauseBtn;
    public Button PlayBtn;

    public Text Score;
    public Text HighScore;

    public PlayerMovement playerMovement;
    public SectionMaker sectionMaker;


    void Start()
    {
        Player.SetActive(false);
        StartPanel.SetActive(true);
        GamePanel.SetActive(false);
        FailPanel.SetActive(false);
        HighScore.text = PlayerPrefs.GetInt("highScore").ToString();
    }

    public void StartButton()
    {
        PauseBtn.gameObject.SetActive(true);
        PlayBtn.gameObject.SetActive(false);
        playerMovement.stillAlive = true;
        Player.SetActive(true);
        StartPanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    public void PauseGame()
    {
        PauseBtn.gameObject.SetActive(false);
        PlayBtn.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PauseBtn.gameObject.SetActive(true);
        PlayBtn.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Fail()
    {
        GameObject ScoreTxt = GameObject.Find("ScoreTxt");
        int highScore = PlayerPrefs.GetInt("highScore");

        if (highScore <= int.Parse((ScoreTxt.GetComponent<Text>().text.ToString())))
        {
            PlayerPrefs.SetInt("highScore", int.Parse(ScoreTxt.GetComponent<Text>().text));
            PlayerPrefs.Save();
        }
        GamePanel.SetActive(false);
        FailPanel.SetActive(true);
    }

    public void StartGame()
    {
        HighScore.text = PlayerPrefs.GetInt("highScore").ToString();
        Player.SetActive(false);
        StartPanel.SetActive(true);
        GamePanel.SetActive(false);
        FailPanel.SetActive(false);
    }
    public void StartAgain()
    {
        sectionMaker.StartGame();
        StartGame();
    }
}
