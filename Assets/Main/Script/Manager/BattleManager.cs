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
        if(Input.GetKey(KeyCode.P))
        {
            WaveStart();
        }
    }

    private void WaveStart()
    {
        _waveCount++;
        _wavestate = e_WaveState.EnemyWave;
    }

    private void UpdateWave()
    {
        switch (_wavestate)
        {
            case e_WaveState.EnemyWave:
                Timer += Time.deltaTime;
                if (Timer >= WaveTime)
                {
                    Timer = 0;
                    WaveEnd();
                }
                break;
            case e_WaveState.BossWave:
                if(IsBossDead == true)
                {
                    IsBossDead = false;
                    WaveEnd();
                }
                break;
        }

    }

    private void WaveEnd()
    {
        foreach(GameObject enemy in m_Enemy)
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
