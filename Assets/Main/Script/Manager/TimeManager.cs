using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameTime
{
    Stop = 0,
    Slow = 5,
    Defualt = 10,
    Fast = 15,
    SuperFast = 20
}

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    private GameManager gameManager;

    //전체시간용
    [SerializeField]
    private float m_GameTime = 1.0f;

    public static GameTime TimeSet = GameTime.Defualt;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        SetGameTime();
    }

    private void SetGameTime()
    {
        if(UIManager.UIBackGround.activeSelf == true)
        {
            Time.timeScale = 0f;
            return;
        }
        m_GameTime = (float)TimeSet / 10;
        Time.timeScale = m_GameTime;
    }

}
