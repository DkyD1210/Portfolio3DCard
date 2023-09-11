using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BattleManager : MonoBehaviour
{


    public static BattleManager Instance;

    private CardManager cardManager;

    private GameManager gameManager;



    [SerializeField]
    private List<GameObject> m_AllUnits = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_Enemy = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_Neutral = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_Player = new List<GameObject>();


    private static int _waveCount;

    public static int WaveCount
    {
        get { return _waveCount; }
    }

    private static e_WaveState _wavestate;

    public static e_WaveState BattleState
    {
        get { return _wavestate; }
    }

    [SerializeField]
    private int WaveTime;

    private float m_Timer;

    private int m_UnitTimer;

    private bool IsBossDead = false;


    private Transform PlayerTrs;

    [SerializeField]
    private Transform SummonTrs;

    [SerializeField]
    private Transform EnemyLayer;

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
    }


    private void Start()
    {
        cardManager = CardManager.Instance;
        gameManager = GameManager.Instance;
        PlayerTrs = GameManager.StaticPlayer.transform;

        if (SaveManager.instance.IsSaveData == true)
        {
            _waveCount = SaveManager.instance.GetSaveData().waveCount;
        }
        else
        {
            _waveCount = 0;
        }
    }

    private void Update()
    {
        UpdateWave();
        if (Input.GetKeyDown(KeyCode.F) && _wavestate == e_WaveState.Prepare)
        {
            WaveStart();
        }
    }

    public void WaveStart()
    {
        cardManager.ClearCardReward();

        _waveCount++;
        Debug.Log(_waveCount.ToString());
        if (_waveCount % 5 == 0)
        {
            _wavestate = e_WaveState.BossWave;
        }
        else
        {
            _wavestate = e_WaveState.EnemyWave;
        }
        //WaveTime = 30 + ((_waveCount / 5) * 5);
        m_Timer = 0;
        m_UnitTimer = 0;
        cardManager.HandSupply();
    }

    private void UpdateWave()
    {
        switch (_wavestate)
        {
            case e_WaveState.Prepare:
                break;

            case e_WaveState.EnemyWave:
                m_Timer += Time.deltaTime;
                if (m_Timer >= WaveTime)
                {
                    WaveEnd();
                }
                CreateUnit();
                break;
            case e_WaveState.BossWave:
                if (IsBossDead == true)
                {
                    IsBossDead = false;
                    WaveEnd();
                }
                break;

        }

    }


    private void CreateUnit()
    {

        if (m_UnitTimer != (int)m_Timer)
        {
            int count = gameManager.m_EnemyOBJList.Count;
            int rand = Random.Range(0, count);

            GameObject unit = Instantiate(gameManager.m_EnemyOBJList[rand], SummonTrs.position, Quaternion.identity, transform);
            m_Enemy.Add(unit);
        }
        m_UnitTimer = (int)m_Timer;
    }



    private void WaveEnd()
    {
        m_Timer = 0;
        int count = m_Enemy.Count;
        for (int i = count - 1; i > -1; i--)
        {
            GameObject enemy = m_Enemy[i];
            m_Enemy.Remove(enemy);
            Destroy(enemy);
        }
        Player player = GameManager.StaticPlayer;
        player.m_UnitBase.ClearBuff();
        cardManager.ClrearDeck();
        cardManager.SetCardReward(3);
        _wavestate = e_WaveState.Prepare;
        UIManager.Instance.ShowWaveEndUI();
    }

    public int GetCount()
    {
        int count = WaveTime - (int)m_Timer;
        return count;
    }

}
