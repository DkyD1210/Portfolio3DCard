using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

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

    [SerializeField]
    public List<GameObject> m_BulletList = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        xmlManager = XmlManager.Instance;
        SetPlayer();
    }

    void Start()
    {
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
            GameObject player = Instantiate(m_PlayerOBJ, UnitLayer.transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity, UnitLayer);
            StaticPlayer = player.GetComponent<Player>();
            if (StaticPlayer == null)
            {
                StaticPlayer = player.AddComponent<Player>();
                Debug.LogError("플레이어 컴포넌트 없음");
            }
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
                TimeManager.Instance.SetTimeSet(e_GameTime.Stop);
            }
            else
            {
                UIBackGround.SetActive(false);
                CameraManager.m_NoCursor = true;
                TimeManager.Instance.SetTimeSet(e_GameTime.Defualt);
            }
        }
    }

    public GameObject CreatBullet(Transform _trs, int listNum)
    {
        GameObject prefab = m_BulletList[listNum];
        GameObject bullet = Instantiate(prefab, _trs.position + new Vector3(0, 1f, 0), _trs.rotation * prefab.transform.rotation, UnitLayer);
        return bullet;
    }

    public void GameSaveAndExit()
    {
        SaveManager.instance.SaveGameData();
        SceneManager.LoadScene((int)SceneType.TitleScene);
    }

    public void GameExit()
    {
        SceneManager.LoadScene((int)SceneType.TitleScene);
    }



}
