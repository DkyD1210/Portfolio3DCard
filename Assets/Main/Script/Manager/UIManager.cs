using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    private BattleManager battleManager;

    [SerializeField]
    private TextMeshProUGUI m_CostText;





    void Start()
    {
        battleManager = BattleManager.Instance;
    }

    
    void Update()
    {
        ShowCost();
    }

    private void ShowCost()
    {
        string maxcost = battleManager.GetMaxCost().ToString();
        string cost = battleManager.GetCost().ToString();
        
        m_CostText.text = $"{cost}/{maxcost}";
    }
}
