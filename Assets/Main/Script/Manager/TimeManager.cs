using System.Collections;
using System.Collections.Generic;
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

    //��ü�ð���
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
        gameManager = GameManager.Instace;
    }

    // Update is called once per frame
    void Update()
    {
        SetGameTime();
    }

    private void SetGameTime()
    {
        m_GameTime = (float)TimeSet / 10;
        Time.timeScale = m_GameTime;
    }

}