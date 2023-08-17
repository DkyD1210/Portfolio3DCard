using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour //handler,IDragHandler 
{


    [SerializeField]
    private TMP_Text m_CostText;


    [SerializeField]
    private GameObject m_CardObject;

    [Header("카드 UI 공간")]
    [SerializeField]
    private RectTransform CardLayer;
    private Vector3 HandStart;
    private Vector3 HandEnd;


    public List<CardBase> m_Deck;

    [Tooltip("뽑을 패")]
    public List<CardBase> m_BeforeDummy;
    [Tooltip("손패")]
    public List<CardBase> m_Hand;
    public List<GameObject> m_CardObj;
    [Tooltip("버린 패")]
    public List<CardBase> m_AfterDummy;




    public CostSystem cardSystem;

    void Start()
    {
        HandStart = new Vector3((CardLayer.rect.width * 0.5f) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width * 0.5f, 0, 0);
        m_BeforeDummy.AddRange(m_Deck);
        DrawCard();
    }


    void Update()
    {
        ShowCost();
        CardHand();


    }

    private void ShowCost()
    {
        float recoverCost = cardSystem.RecoverCost * 0.01f;
        cardSystem.Cost += recoverCost * Time.deltaTime;
        cardSystem.Cost = Mathf.Clamp(cardSystem.Cost, 0, cardSystem.MaxCost);

        m_CostText.text = $"{(int)cardSystem.Cost}/{cardSystem.MaxCost}";
    }

    private void DrawCard()
    {
        CardBase drawCard = m_BeforeDummy[0];
        m_Hand.Add(drawCard);
        m_BeforeDummy.Remove(drawCard);

        GameObject objCard = Instantiate(m_CardObject, CardLayer);
        m_CardObj.Add(objCard);

        CardFrame resultCard = objCard.GetComponent<CardFrame>();
        resultCard.m_Card = new CardBase(drawCard);
    }

    private void CardHand()
    {

        int count = m_CardObj.Count;


        for (int i = 0; i < count; i++)
        {
            //float distance = Vector3.Distance(HandStart, HandEnd);
            float value = (float)i / (count);
            Vector3 pos = Vector3.Lerp(HandStart, HandEnd, value);
            pos.y = CardLayer.position.y * (Mathf.Abs(0.5f - value) * -1);

            GameObject card = m_CardObj[i];

            card.transform.localPosition = pos;
            card.transform.localRotation = Quaternion.Euler(0, 0, 20 * (0.5f - value));



        }
    }

}

[System.Serializable]
public class CostSystem
{
    public float Cost;

    public int MaxCost = 10;

    public int RecoverCost = 100;
}
