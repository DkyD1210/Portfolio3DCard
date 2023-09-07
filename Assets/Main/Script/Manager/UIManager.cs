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

    [Header("��� ����")]
    [SerializeField]
    private GameObject _UIBackGround;

    public static GameObject UIBackGround;

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
    private UnityEngine.UI.Slider PlayerHpBar;

    [SerializeField]
    private TMP_Text PlayerHPText;


    [Header("ī�� ����")]
    [SerializeField]
    private GameObject DeckUI;

    [SerializeField]
    private GameObject BeforeUI;

    [SerializeField]
    private GameObject AfterUI;

    [SerializeField]
    private GameObject CardRewardUI;

    [SerializeField]
    private GameObject WaveEndUI;

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

        float _hp = (float)PlayerHP / PlayerMaxHP;
        PlayerHpBar.value = _hp;
    }


    #region ī�� UI ����

    public void ShowDeckUI()
    {
        bool activeSelf = !DeckUI.activeSelf;
        DeckUI.SetActive(activeSelf);
        _UIBackGround.SetActive(activeSelf);
    }

    public void ShowBforekUI()
    {
        bool activeSelf = !BeforeUI.activeSelf;
        BeforeUI.SetActive(activeSelf);
        _UIBackGround.SetActive(activeSelf);
    }

    public void ShowAfterkUI()
    {
        bool activeSelf = !AfterUI.activeSelf;
        AfterUI.SetActive(activeSelf);
        _UIBackGround.SetActive(activeSelf);
    }


    public void ShowCardRewardUI()
    {
        bool activeSelf = !CardRewardUI.activeSelf;
        CardRewardUI.SetActive(activeSelf);
        _UIBackGround.SetActive(activeSelf);
    }

    public void ShowWaveEndUI()
    {
        bool activeSelf = !WaveEndUI.activeSelf;
        WaveEndUI.SetActive(activeSelf);
    }


    #endregion


}
