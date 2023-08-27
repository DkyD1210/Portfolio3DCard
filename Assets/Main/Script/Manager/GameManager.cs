using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instace;

    private XmlManager xmlManager;

    public static Player StaticPlayer;

    [Header("전역설정")]
    [SerializeField]
    private GameObject m_PlayerOBJ;

    [SerializeField]
    private Transform UnitLayer;

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
        SetPlayer();
    }

    void Start()
    {
    }


    void Update()
    {
        SetUI();
    }

    private void SetPlayer()
    {
        StaticPlayer = FindObjectOfType<Player>();
        if (StaticPlayer == null)
        {
            GameObject player = Instantiate(m_PlayerOBJ, UnitLayer);
            if (player.TryGetComponent(out Player playerCompenet) == false)
            {
                player.AddComponent<Player>();
            }
            StaticPlayer = player.GetComponent<Player>();
        }
        StaticPlayer.m_UnitBase = XmlManager.Instance.TransXmlUnit(XmlManager.Instance.GetUnitData(1));


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
