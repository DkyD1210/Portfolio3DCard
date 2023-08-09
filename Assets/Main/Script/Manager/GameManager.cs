using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame_Xml;

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
        if(Instace == null)
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
        UnitBase _base = XmlManager.Instance.TransXml(XmlManager.Instance.DataDic[new XmlId(1)]);
        model._unitBase = _base;
    }

    private void SetUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIBackGround.SetActive(!UIBackGround.activeSelf);
            CameraManager.m_NoCursor = !UIBackGround.activeSelf;
        }
    }
}
