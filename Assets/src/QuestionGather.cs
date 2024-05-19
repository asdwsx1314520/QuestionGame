using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGather : MonoBehaviour
{
    /// <summary> 問題本身 </summary>
    [Header("問題本身")]
    public GameObject[] arrQuestion;
    /// <summary> 每題的答案 </summary>
    [Header("每題的答案")]
    public string[] arrAnswer;

    #region 關閉所有的題目
    /// <summary> 關閉所有的題目 </summary>
    public void closeAllQuestion()
    {
        int len = arrQuestion.Length;
        for (int i = 0; i < len; i++)
        {
            arrQuestion[i].SetActive(false);
        }
    }
    #endregion

    #region 獲取指定的題目
    /// <summary> 獲取指定題目 </summary>
    /// <param name="value"> 哪一題 </param>
    public void getQuestion(int value)
    {
        closeAllQuestion();
        arrQuestion[value].SetActive(true);
    }
    #endregion

    #region 判斷玩家回答是否正確
    /// <summary>
    /// 判斷玩家回答是否正確
    /// </summary>
    /// <param name="value"> 哪一題 </param>
    /// <param name="answer"> 答案 </param>
    public bool getJudgeAnswer(int value, string answer)
    {
        return answer == arrAnswer[value];
    }
    #endregion
}
