using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BattleManager : MonoBehaviour
{


    public static BattleManager Instance;

    private CardManager cardManager;

    private GameManager gameManager;

    private UIManager uiManager;

    private SoundManager soundManager;

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

    private bool CountDown = true;

    private bool UnitCreateEnd = false;

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
        uiManager = UIManager.Instance;
        soundManager = SoundManager.Instance;
        PlayerTrs = GameManager.StaticPlayer.transform;

        if (SaveManager.instance.IsSaveData == true)
        {
            _waveCount = SaveManager.instance.GetSaveData().waveCount;
        }
        else
        {
            _waveCount = 1;
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

        Debug.Log(_waveCount.ToString());

        WaveTime = 30 + ((int)(_waveCount * 0.5f) * 5);
        m_EnemySpawnCount = 7 + (_waveCount * 3);

        if (_waveCount % 5 == 0)
        {
            soundManager.PlayBGM(2);
            _wavestate = e_WaveState.BossWave;
            CreatBoss();
        }
        else
        {
            soundManager.PlayBGM(1);
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
                if (CountDown == true)
                {
                    if (WaveTime <= m_Timer + 10)
                    {
                        StartCoroutine(CountDownSFX());
                    }
                }
                if (UnitCreateEnd == true && m_Enemy.Count == 0)
                {
                    WaveEnd();
                }
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
                        StartCoroutine(uiManager.GameWin());
                    }
                }
                break;
        }

    }

    private IEnumerator CountDownSFX()
    {
        CountDown = false;
        for (int i = 0; i < 10; i++)
        {
            if (_wavestate != e_WaveState.EnemyWave)
            {
                break;
            }
            soundManager.PlaySFX(6);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    private IEnumerator CreateUnit()
    {
        float unitTime = 10f / m_EnemySpawnCount;

        int unitCount = 0;

        while (m_EnemySpawnCount > unitCount)
        {
            if (BattleState == e_WaveState.BossWave)
            {
                break;
            }
            int count = gameManager.m_EnemyOBJList.Count;
            int rand = Random.Range(0, count);

            GameObject unit = Instantiate(gameManager.m_EnemyOBJList[rand], SummonTrs.position, Quaternion.identity, EnemyLayer);
            m_Enemy.Add(unit);

            unitCount++;

            yield return new WaitForSeconds(unitTime);
        }

        UnitCreateEnd = true;
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
        CountDown = true;
        soundManager.PlaySFX(7);

        int count = m_Enemy.Count;
        for (int i = count - 1; i > -1; i--)
        {
            GameObject enemy = m_Enemy[i];
            m_Enemy.Remove(enemy);
            Destroy(enemy);
        }
        UnitCreateEnd = false;


        Player player = GameManager.StaticPlayer;
        player.m_UnitBase.ClearBuff();
        gameManager.CreatParticle(PlayerTrs, 2, player.transform.position + new Vector3(0, 1, 0));

        cardManager.ClrearDeck();

        _waveCount++;

        cardManager.SetCardReward(3);
        _wavestate = e_WaveState.Prepare;
        uiManager.ShowWaveEndUI();
    }

    public int GetCount()
    {
        int count = WaveTime - (int)m_Timer;
        return count;
    }



}
