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
    private List<GameObject> m_Boss = new List<GameObject>();

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

    private int m_EnemySpawnCount;

    public bool IsBossDead = false;

    public bool GameEnd = false;

    public bool Endless = false;

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

        if (Input.GetKeyUp(KeyCode.N))
        {
            WaveStart();
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            WaveEnd();
        }
    }

    public void WaveStart()
    {
        cardManager.ClearCardReward();

        _waveCount++;
        Debug.Log(_waveCount.ToString());

        WaveTime = 25 + ((int)(_waveCount * 0.5f) * 5);
        m_EnemySpawnCount = 10 + (_waveCount * 3);

        if (_waveCount % 5 == 0)
        {
            _wavestate = e_WaveState.BossWave;
            CreatBoss();
        }
        else
        {
            _wavestate = e_WaveState.EnemyWave;
            StartCoroutine(CreateUnit());
        }

        m_Timer = 0;
        cardManager.HandSupply();
    }

    private void UpdateWave()
    {
        if (GameEnd == true)
        {
            return;
        }
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
                break;
            case e_WaveState.BossWave:
                if (IsBossDead == true)
                {
                    IsBossDead = false;
                    WaveEnd();

                    if (Endless == false)
                    {
                        StartCoroutine(UIManager.Instance.GameWin());
                    }
                }
                break;

        }

    }


    private IEnumerator CreateUnit()
    {
        float unitTime = 10f / m_EnemySpawnCount;

        int unitCount = 0;

        while (m_EnemySpawnCount > unitCount)
        {
            int count = gameManager.m_EnemyOBJList.Count;
            int rand = Random.Range(0, count);

            GameObject unit = Instantiate(gameManager.m_EnemyOBJList[rand], SummonTrs.position, Quaternion.identity, EnemyLayer);
            m_Enemy.Add(unit);

            unitCount++;

            yield return new WaitForSeconds(unitTime);
        }

        yield return null;
    }

    private void CreatBoss()
    {
        GameObject unit = Instantiate(gameManager.m_BossOBJList[0], EnemyLayer.position + new Vector3(0, 2, 10), Quaternion.identity, EnemyLayer);
        m_Boss.Add(unit);

        IsBossDead = false;

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
