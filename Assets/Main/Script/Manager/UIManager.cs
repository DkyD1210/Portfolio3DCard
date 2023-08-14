using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    private BattleManager battleManager;

    [SerializeField]
    private TextMeshProUGUI m_CostText;

    [SerializeField]
    private RectTransform CardLayer;

    public List<Card> m_CardList;

    [SerializeField]
    private Vector3 HandStart;

    [SerializeField]
    private Vector3 HandEnd;


    void Start()
    {
        battleManager = BattleManager.Instance;
        m_CardList.AddRange(CardLayer.gameObject.GetComponentsInChildren<Card>());
        HandStart = new Vector3((CardLayer.rect.width / 2) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width / 2, 0, 0);
    }


    void Update()
    {
        ShowCost();
        CardHand();
    }

    private void ShowCost()
    {
        string maxcost = battleManager.GetMaxCost().ToString();
        string cost = battleManager.GetCost().ToString();

        m_CostText.text = $"{cost}/{maxcost}";
    }

    private void CardHand()
    {

        int count = m_CardList.Count;




        for (int i = 0; i < count; i++)
        {
            //float distance = Vector3.Distance(HandStart, HandEnd);
            float value = (float)i / (count - 1);
            Vector3 pos = Vector3.Lerp(HandStart, HandEnd, value);
            pos.y = CardLayer.position.y * (Mathf.Abs(0.5f - value) * -1);

            Card card = m_CardList[i];

            card.transform.localPosition = pos;
        }
    }



}
