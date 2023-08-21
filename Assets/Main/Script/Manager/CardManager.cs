using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour
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
    public List<CardFrame> m_Hand;
    [Tooltip("버린 패")]
    public List<CardBase> m_AfterDummy;




    public CostSystem cardSystem;

    void Start()
    {
        HandStart = new Vector3((CardLayer.rect.width * 0.5f) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width * 0.5f, 0, 0);
        m_BeforeDummy.AddRange(m_Deck);
        DrawCard(10);
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

    private void DrawCard(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            CardBase drawCard = m_BeforeDummy[0];

            GameObject objCard = Instantiate(m_CardObject, CardLayer);

            CardFrame resultCard = objCard.GetComponent<CardFrame>();
            resultCard.m_CardBase = new CardBase(drawCard);
            m_Hand.Add(resultCard);

            m_BeforeDummy.Remove(drawCard);
        }

    }

    private void CardHand()
    {

        int count = m_Hand.Count;


        for (int i = 0; i < count; i++)
        {
            CardFrame frame = m_Hand[i];
            

            float value = (float)i / (count);
            Vector3 pos = Vector3.Lerp(HandStart, HandEnd, value);

            switch (frame.CardState)
            {
                case CardState.MouseEnter:
                    pos.y = CardLayer.position.y - 75f;
                    frame.transform.localScale = new Vector3(1.2f, 1.2f, 2f);
                    frame.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    frame.transform.localPosition = pos;
                    frame.transform.SetAsLastSibling();
                    break;
                case CardState.MouseDrag:
                    pos = Input.mousePosition;
                    frame.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
                    frame.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    frame.transform.position = pos;
                    break;
                case CardState.MouseExit:
                    pos.y = CardLayer.position.y * (Mathf.Abs(0.5f - value) * -1);
                    frame.transform.localScale = new Vector3(1f, 1f, 0);
                    frame.transform.localRotation = Quaternion.Euler(0, 0, 25 * (0.5f - value));
                    frame.transform.localPosition = pos;
                    frame.transform.SetAsFirstSibling();
                    break;
                case CardState.CardUse:
                    m_Hand.Remove(frame);
                    UseCard(frame);
                    Destroy(frame);
                    Destroy(frame.gameObject);
                    break;
            }


        }
    }

    private void UseCard(CardFrame card)
    {
        
    }

}

[System.Serializable]
public class CostSystem
{
    public float Cost;

    public int MaxCost = 10;

    public int RecoverCost = 100;
}
