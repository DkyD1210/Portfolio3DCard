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

    [SerializeField]
    public List<GameObject> m_EnemyOBJList = new List<GameObject>();

    public static Dictionary<int, GameObject> m_EnemyDic = new Dictionary<int, GameObject>();

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
        xmlManager = XmlManager.Instance;
        SetPlayer();
        SetEnemy();
    }


    void Update()
    {
        SetUI();
    }

    private void SetEnemy()
    {
        int count = m_EnemyOBJList.Count;
        for (int i = 0; i > count; i++)
        {
            GameObject unit = m_EnemyOBJList[i];
            UnitBase unitdata = unit.GetComponent<Enemy>().m_UnitBase;
            if (unitdata == null)
            {
                unitdata = unit.AddComponent<Enemy>().m_UnitBase;
            }
            int unitXmlID = 100000 + i;
            unitdata = xmlManager.TransXmlUnit(xmlManager.GetUnitData(unitXmlID));
            m_EnemyDic.Add(unitdata.Id, unit);
            Debug.Log("성공");

        }
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
        StaticPlayer.m_UnitBase = xmlManager.TransXmlUnit(xmlManager.GetUnitData(1));

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

    private void GetUnit()
    {

    }

}
