using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private GameManager gameManager;

    //��ü�ð���
    [SerializeField]
    private float m_GameTime = 1.0f;

    private bool m_GameStop;


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
        Time.timeScale = m_GameTime;
    }

}
