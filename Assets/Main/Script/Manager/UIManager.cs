using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instace;

    private BattleManager battleManager;


    [Header("웨이브 관련")]
    [SerializeField]
    private TMP_Text WaveState;

    [SerializeField]
    private TMP_Text WaveCountDown;

    [SerializeField]
    private GameObject BossImage;


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
        battleManager = BattleManager.Instance;
        WaveState = GetComponent<TMP_Text>();
        WaveCountDown = GetComponent<TMP_Text>();
        InitWaveUI();

    }


    void Update()
    {
        UpdateWaveUI();
    }

    private void InitWaveUI()
    {
    }

    private void UpdateWaveUI()
    {
        Debug.Log(BattleManager.BattleState);
        Debug.Log(battleManager.GetCount().ToString());

        switch (BattleManager.BattleState)
        {
            case e_WaveState.Prepare:
                BossImage.SetActive(false);
                WaveCountDown.text = "?";
                WaveState.text = "준비 단계";
                break;

            case e_WaveState.EnemyWave:
                BossImage.SetActive(false);
                WaveCountDown.text = battleManager.GetCount().ToString();
                WaveState.text = "일반 전투";
                break;

            case e_WaveState.BossWave:
                BossImage.SetActive(true);
                WaveCountDown.text = "";
                WaveState.text = "강한 전투";
                break;
        }
    }
}
