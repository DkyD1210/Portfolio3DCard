using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public static UIManager Instace;

    private BattleManager battleManager;

    [Header("배경 필터")]
    [SerializeField]
    private GameObject _UIBackGround;

    public static GameObject UIBackGround;

    [Header("웨이브 관련")]
    [SerializeField]
    private TMP_Text WaveState;

    [SerializeField]
    private TMP_Text WaveCountDown;

    [SerializeField]
    private GameObject BossImage;


    [Header("플레이어 관련")]
    private Player player;

    private int PlayerMaxHP;

    private int PlayerHP;

    [SerializeField]
    private GameObject PlayerHPBar;

    [SerializeField]
    private TMP_Text PlayerHPText;


    [Header("카드 관련")]
    [SerializeField]
    private GameObject DeckUI;

    [SerializeField]
    private GameObject BeforeUI;

    [SerializeField]
    private GameObject AfterUI;

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
        UIBackGround = _UIBackGround;
        battleManager = BattleManager.Instance;
        player = GameManager.StaticPlayer;
        InitWaveUI();

    }


    void Update()
    {
        UpdateWaveUI();
        UpdateHPUI();
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ShowDeckUI();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ShowBforekUI();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ShowAfterkUI();
        }
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

    private void UpdateHPUI()
    {
        PlayerHP = (int)player.m_UnitBase.hp;
        PlayerMaxHP = player.m_UnitBase.MaxHp;
        string hpText = $"{PlayerHP}/{PlayerMaxHP}";
        PlayerHPText.text = hpText;
    }


    #region 카드 UI 세팅

    public void ShowDeckUI()
    {
        bool activeSelf = !DeckUI.activeSelf;
        _UIBackGround.SetActive(activeSelf);
        DeckUI.SetActive(activeSelf);
    }

    public void ShowBforekUI()
    {
        bool activeSelf = !BeforeUI.activeSelf;
        _UIBackGround.SetActive(activeSelf);
        BeforeUI.SetActive(activeSelf);
    }

    public void ShowAfterkUI()
    {
        bool activeSelf = !AfterUI.activeSelf;
        _UIBackGround.SetActive(activeSelf);
        AfterUI.SetActive(activeSelf);
    }


    #endregion
}
