using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager1 : MonoBehaviour
{

    public delegate void GameDelegate();                        //delegate是什麼    v.發派  n.代理
                                                                //this is allow us to create certain events for other scripts to notified of
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;       //delegate是一特殊event

    public GameObject StartPage;
    public GameObject GameOverPage;
    public GameObject CountdownPage;
    public Text ScoreText;

    int score = 0;

    bool gameOver = true;                                      //private
    public bool GameOver { get { return gameOver; } }           // accessible but not modifiable for other Scirpt
    //也就是說控制gameover的bool只能在GameManager修改
    enum pageState//宣告新的可列舉的具名常數 https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/enum
    {
        None,        // = 0
        Start,       // = 1
        GameOver,    // = 2
        Countdown    // = 3
    }

    void SetPageState(pageState state)
    {
        switch (state)
        {
            case pageState.None:
                StartPage.SetActive(false);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(false);
                break;
            case pageState.Start:
                StartPage.SetActive(true);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(false);
                break;
            case pageState.GameOver:
                StartPage.SetActive(false);
                GameOverPage.SetActive(true);
                CountdownPage.SetActive(false);
                break;
            case pageState.Countdown:
                StartPage.SetActive(false);
                GameOverPage.SetActive(false);
                CountdownPage.SetActive(true);
                break;
        }
    }
    //create a static accessibility reference

    public static GameManager1 Instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
    }
    //eg: otherscript : 
    //                 GameManager.Instance
    //                 (be able to access the public members within this class)

    //因為此為單一個scene 所以不需擔心 destroying Onload...etc.                            (還不懂)

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerScored += OnPlayerScored;
        TapController.OnPlayerDied += OnPlayerDied;
    }
    
    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerScored -= OnPlayerScored;
        TapController.OnPlayerDied -= OnPlayerDied;    
    }


    void OnCountdownFinished(){
        SetPageState (pageState.None);    //遊戲中
        OnGameStarted();//event sent to TapController
        score = 0;
        //GameOver = false;        只能讀不能改
        gameOver = false;
    }
    void OnPlayerScored()
    {
        score++;
        ScoreText.text = score.ToString();
    }

    void OnPlayerDied(){
        gameOver = true;     //真正執行OnPlayerDied()的是在TapController  而gameOver的值只能在GM這邊改變
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore){
            PlayerPrefs.SetInt("HighScore",score);
        }
        SetPageState(pageState.GameOver);
    }
  



    //Replay Button
    public void ConfirmGameOver()//actived when replay button is hit
    {
        OnGameOverConfirmed();//event sent to TapController
        ScoreText.text = "0";
        SetPageState(pageState.Start);
    }

    
    //Play Button
    public void StartGame()//actived when play button is hit
    {
        SetPageState(pageState.Countdown);
    }
}
