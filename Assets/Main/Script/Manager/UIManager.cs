using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instace;

    private BattleManager battleManager;


    [Header("���̺� ����")]
    [SerializeField]
    private TMP_Text WaveState;

    [SerializeField]
    private TMP_Text WaveCountDown;

    [SerializeField]
    private GameObject BossImage;

    [Header("�÷��̾� ����")]
    private Player player;

    private int PlayerMaxHP;

    private int PlayerHP;

    [SerializeField]
    private GameObject PlayerHPBar;

    [SerializeField]
    private TMP_Text PlayerHPText;

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
        player = GameManager.StaticPlayer;
        InitWaveUI();

    }


    void Update()
    {
        UpdateWaveUI();
        UpdateHPUI();
    }

    private void InitWaveUI()
    {
    }

    private void UpdateWaveUI()
    {

        switch (BattleManager.BattleState)
        {
            case e_WaveState.Prepare:
                BossImage.SetActive(false);
                WaveCountDown.text = "?";
                WaveState.text = "�غ� �ܰ�";
                break;

            case e_WaveState.EnemyWave:
                BossImage.SetActive(false);
                WaveCountDown.text = battleManager.GetCount().ToString();
                WaveState.text = "�Ϲ� ����";
                break;

            case e_WaveState.BossWave:
                BossImage.SetActive(true);
                WaveCountDown.text = "";
                WaveState.text = "���� ����";
                break;
        }
    }

    private void UpdateHPUI()
    {
        PlayerHP = (int)player.m_UnitBase.hp;
        PlayerMaxHP = player.m_UnitBase.MaxHp;
        string hpText = $"{PlayerHP}/{PlayerMaxHP}";
        PlayerHPText.text = hpText;
    }
}
