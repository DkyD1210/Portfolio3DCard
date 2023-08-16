using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instace;

    private XmlManager xmlManager;

    [SerializeField]
    private Player Player;

    [SerializeField]
    private GameObject UIBackGround;

    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SetPlayer();
    }


    void Update()
    {
        SetUI();
    }

    private void SetPlayer()
    {
        Player = FindObjectOfType<Player>();
        Player.m_UnitBase = XmlManager.Instance.TransXml(XmlManager.Instance.GetData(1));
    }

    private void SetUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIBackGround.activeSelf == false)
            {
                UIBackGround.SetActive(true);
                CameraManager.m_NoCursor = false;
                TimeManager.TimeSet = GameTime.Stop;
            }
            else
            {
                UIBackGround.SetActive(false);
                CameraManager.m_NoCursor = true;
                TimeManager.TimeSet = GameTime.Defualt;
            }
        }
    }
}
