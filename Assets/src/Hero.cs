using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;

    private Animator m_animator;
    private Rigidbody2D m_body2d;

    /// <summary> 玩家血量 </summary>
    private int nmHp = 100;
    /// <summary> 遊戲UI畫面 </summary>
    [Header("遊戲UI畫面")]
    public GameObject canvas;
    public Transform tf;
    private int m_totalStation = 12;
    private float m_moveDistance = 7.5f;//7.5f
    private List<float> arrDistance = new List<float>();
    [SerializeField]
    private bool isRun = false;
    /// <summary> 當前的目的 </summary>
    private float m_nowStation = 0;

    /// <summary> 武士傷害 </summary>
    private int warrior_hit = 50;
    /// <summary> 齒輪傷害 </summary>
    private int sawtooth_hit = 100;
    /// <summary> 雷射傷害 </summary>
    private int ray_hit = 35;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        float m_now = 0;
        for (int i = 0; i < m_totalStation; i++)
        {
            m_now += m_moveDistance;
            arrDistance.Add(m_now);
        }
    }

    void FixedUpdate()
    {
        //m_animator.SetInteger("AnimState", 0);
        if (isRun)
        {
            m_body2d.velocity = new Vector2(1 * m_speed, m_body2d.velocity.y);
            m_animator.SetTrigger("SelfRun");

            if (tf.position.x >= m_nowStation)
            {
                isRun = false;
                canvas.SetActive(true);
            }
        }
        else
        {
            m_animator.SetTrigger("SelfIdle");
        }
    }

    #region 開始往前
    /// <summary>
    /// 開始往前
    /// </summary>
    /// <param name="nmStation">當前積分</param>
    public void onRun(int nmStation)
    {
        isRun = true;
        m_nowStation = arrDistance[nmStation];
    }
    #endregion

    #region 受傷
    void OnTriggerEnter2D(Collider2D col)
    {
        m_animator.SetTrigger("Hurt");

        int hit = 0;

        switch (col.name)
        {
            case "warrior_hit":
                hit = warrior_hit;
                break;
            case "sawtooth_hit":
                hit = sawtooth_hit;
                break;
            case "ray_hit":
                hit = ray_hit;
                break;
        }

        nmHp -= hit;

        if (nmHp <= 0)
        {
            m_animator.SetTrigger("Death");
        }
    }
    #endregion

    #region 復活
    public void onComBack()
    {
        nmHp = 100;
        canvas.SetActive(true);
    }
    #endregion


}
