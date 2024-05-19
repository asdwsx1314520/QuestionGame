using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 宣告
    /// <summary> 遊戲UI畫面 </summary>
    [Header("遊戲UI畫面")]
    public GameObject canvas;
    /// <summary> 主角 </summary>
    [Header("主角")]
    public Hero hero;
    /// <summary> 當前關卡 </summary>
    public int iNowLevel = 0;
    /// <summary> 當前積分 </summary>
    public int iNowPoint = 0;
    /// <summary> 過關條件 </summary>
    public int iPassLevel = 3;
    /// <summary> 關卡積分 </summary>
    public int iLevelPoint = 0;
    /// <summary> 通關分數 </summary>
    private int iPassGame = 12;
    /// <summary> 開始遊戲按鈕 </summary>
    [Header("開始遊戲按鈕")]
    public GameObject grpStartGame;
    /// <summary> 遊戲故事 </summary>
    [Header("遊戲故事")]
    public GameObject[] arrLevelStory;
    /// <summary> 問題集合與答案 </summary>
    [Header("問題集合與答案")]
    public QuestionGather[] arrQuestionGather;
    /// <summary> 是否看完按鈕 </summary>
    [Header("是否看完按鈕")]
    public GameObject grpRead;
    /// <summary> 答題按鈕 </summary>
    [Header("答題按鈕")]
    public GameObject grpBtnAnswer;

    /// <summary> 通關畫面 </summary>
    [Header("通關畫面")]
    public GameObject grpPassGame;

    /// <summary> 攻擊動畫 </summary>
    [Header("攻擊動畫")]
    public Animator[] m_animator;
    [Header("傷害動畫集合")]
    public GameObject damage;
    #endregion

    void Awake()
    {
        DefaultSetting();
    }

    private void Start()
    {
        grpStartGame.SetActive(true);
    }

    #region 初始設定
    /// <summary> 初始設定 </summary>
    private void DefaultSetting()
    {
        closeAllStory();
        closeAllQuestion();

        grpBtnAnswer.SetActive(false);
        grpRead.SetActive(false);
        grpPassGame.SetActive(false);

        iNowLevel = 0;
        iNowPoint = 0;
        iLevelPoint = 0;
    }
    #endregion

    #region 開始遊戲
    /// <summary> 開始遊戲 </summary>
    private void StartGame()
    {
        getStory(iNowLevel);
        grpRead.SetActive(true);
    }
    #endregion

    #region 開始遊戲按鈕
    /// <summary> 開始遊戲按鈕 </summary>
    public void btnStartGame()
    {
        StartGame();
        grpStartGame.SetActive(false);
    }
    #endregion

    #region 關閉所有的故事
    /// <summary> 關閉所有的故事 </summary>
    private void closeAllStory()
    {
        int len = arrLevelStory.Length;
        for (int i = 0; i < len; i++)
        {
            arrLevelStory[i].SetActive(false);
        }
    }
    #endregion

    #region 關閉所有的題目
    /// <summary> 關閉所有的題目 </summary>
    private void closeAllQuestion()
    {
        int len = arrQuestionGather.Length;
        for (int i = 0; i < len; i++)
        {
            arrQuestionGather[i].closeAllQuestion();
        }
    }
    #endregion

    #region 獲取指定故事
    /// <summary> 獲取指定故事 </summary>
    /// <param name="value"> 哪一個 </param>
    private void getStory(int value)
    {
        closeAllStory();
        arrLevelStory[value].SetActive(true);
    }
    #endregion

    #region 獲取指定的題目
    /// <summary>
    /// 獲取指定的題目
    /// </summary>
    /// <param name="iStory"> 當前是哪一個故事 </param>
    /// <param name="iQuestion"> 哪一個題目 </param>
    private void getQuestion(int iStory, int iQuestion)
    {
        closeAllQuestion();
        arrQuestionGather[iStory].arrQuestion[iQuestion].SetActive(true);
    }
    #endregion

    #region 閱讀確認
    /// <summary> 閱讀確認 </summary>
    public void btnRead()
    {
        closeAllStory();
        getQuestion(iNowLevel, iLevelPoint);
        grpRead.SetActive(false);
        grpBtnAnswer.SetActive(true);
    }
    #endregion

    #region 答題
    /// <summary> 答題 </summary>
    public void btnAnswer(string strAnswer)
    {
        bool isAnswer = arrQuestionGather[iNowLevel].getJudgeAnswer(iLevelPoint, strAnswer);

        if (isAnswer)
        {
            Right();
        }
        else
        {
            Wrong();
        }
    }
    #endregion

    #region 答對
    /// <summary> 答對 </summary>
    private void Right()
    {
        iNowPoint++;
        iLevelPoint++;

        if (iNowPoint >= iPassGame)
        {
            grpRead.SetActive(false);
            grpBtnAnswer.SetActive(false);
            grpPassGame.SetActive(true);
            closeAllQuestion();
            closeAllStory();
            return;
        }

        canvas.SetActive(false);
        hero.onRun(iNowPoint);

        if (iLevelPoint >= iPassLevel)
        {
            iLevelPoint = 0;
            iNowLevel++;

            closeAllQuestion();
            getStory(iNowLevel);
            grpRead.SetActive(true);
            grpBtnAnswer.SetActive(false);
        }
        else
        {
            getQuestion(iNowLevel, iLevelPoint);
        }
    }
    #endregion

    #region 答錯
    /// <summary> 答錯 </summary>
    private void Wrong()
    {
        int iRand = UnityEngine.Random.Range(0, 3);

        float nmPointX = hero.tf.position.x;
        damage.transform.position = new Vector3(nmPointX, damage.transform.position.y, damage.transform.position.z);

        m_animator[iRand].SetTrigger("attack");
        canvas.SetActive(false);
    }
    #endregion

    #region 再來一局
    public void btnRestart()
    {
        SceneManager.LoadScene(0);
        
    }
    #endregion


}
