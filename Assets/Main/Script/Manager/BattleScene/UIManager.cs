using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

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
    private TMP_Text WaveCountPanel;

    [SerializeField]
    private GameObject BossImage;




    [Header("플레이어 관련")]
    [SerializeField]
    private GameObject PlayerLayer;

    [SerializeField]
    private Slider PlayerHpBar;

    [SerializeField]
    private TMP_Text PlayerHPText;

    private Player player;

    private int PlayerMaxHP;

    private int PlayerHP;


    [Header("카드 관련")]

    [SerializeField]
    private GameObject CardLayer;

    [SerializeField]
    private GameObject DeckUI;

    [SerializeField]
    private GameObject BeforeUI;

    [SerializeField]
    private GameObject AfterUI;

    [SerializeField]
    private GameObject CardRewardUI;

    [Header("그 외")]

    [SerializeField]
    private GameObject WaveEndUI;

    [SerializeField]
    private GameObject GameEndUI;

    [SerializeField]
    private GameObject GameWinUI;

    [SerializeField]
    private GameObject ESCUI;

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

    void Start()
    {
        UIBackGround = _UIBackGround;
        battleManager = BattleManager.Instance;
        player = GameManager.StaticPlayer;

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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowESCUI();
        }
    }

    private void UpdateWaveUI()
    {

        WaveCountPanel.text = $"웨이브 : {BattleManager.WaveCount}";

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

        float _hp = (float)PlayerHP / PlayerMaxHP;
        PlayerHpBar.value = _hp;
    }

    public void GameEndBefore()
    {
        PlayerLayer.SetActive(false);
        CardLayer.SetActive(false);
    }

    public void GameEndAfter()
    {
        GameEndUI.SetActive(true);
        Debug.Log("게임종료");
    }

    public IEnumerator GameWin()
    {
        GameWinUI.SetActive(true);
        ShowWaveEndUI();

        Image image = GameWinUI.GetComponent<Image>();
        Color alpha = image.color;

        while (image.color.a < 1)
        {
            alpha.a += Time.deltaTime;
            image.color = alpha;

            yield return null;
        }

        ShowWaveEndUI();
        TimeManager.Instance.SetTimeSet(e_GameTime.Stop);
        yield return null;
    }

    public void ShowESCUI()
    {
        bool activeSelf = !ESCUI.activeSelf;
        ESCUI.SetActive(activeSelf);
        _UIBackGround.SetActive(activeSelf);
    }
    public void ShowConfigUI()
    {
        SoundManager.Instance.ShowConfig();
    }

    #region 카드 UI 세팅

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
