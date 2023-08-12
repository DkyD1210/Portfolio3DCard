using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instace;

    private XmlManager xmlManager;

    [SerializeField]
    private GameObject Player;

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
        UnitBaseModel model = Player.GetComponent<UnitBaseModel>();
        UnitBase _base = XmlManager.Instance.TransXml(XmlManager.Instance.GetData(1));
        model._unitBase = _base;
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
                TimeManager.TimeSet = GameTime.Slow;
            }
        }
    }
}