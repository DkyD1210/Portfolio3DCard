using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleManager : MonoBehaviour
{


    public static BattleManager Instance;


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

    private float Timer;

    private bool IsBossDead = false;


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

    private void Update()
    {
        UpdateWave();
        if (Input.GetKeyDown(KeyCode.P))
        {
            WaveStart();
        }
    }

    private void WaveStart()
    {
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
        WaveTime = 30 + ((_waveCount / 5) * 5);
        Timer = 0;
    }

    private void UpdateWave()
    {
        switch (_wavestate)
        {
            case e_WaveState.EnemyWave:
                Timer += Time.deltaTime;
                if (Timer >= WaveTime)
                {
                    WaveEnd();
                }
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

    private void WaveEnd()
    {
        Timer = 0;
        foreach (GameObject enemy in m_Enemy)
        {
            m_Enemy.Remove(enemy);
            Destroy(enemy);
        }
        _wavestate = e_WaveState.Prepare;
    }

    public int GetCount()
    {
        int count = WaveTime - (int)Timer;
        return count;
    }
}
