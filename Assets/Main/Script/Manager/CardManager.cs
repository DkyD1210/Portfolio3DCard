using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour
{

    Player player;

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

    [SerializeField]
    private List<CardScript> m_ScriptList = new List<CardScript>();


    public CostSystem cardSystem;

    void Start()
    {
        player = GameManager.Player;
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
            CardFrame card = m_Hand[i];


            float value = (float)i / (count);
            Vector3 pos = Vector3.Lerp(HandStart, HandEnd, value);

            switch (card.CardState)
            {
                case CardState.MouseEnter:
                    pos.y = CardLayer.position.y - 75f;
                    card.transform.localScale = new Vector3(1.2f, 1.2f, 2f);
                    card.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    card.transform.localPosition = pos;
                    card.transform.SetAsLastSibling();
                    break;
                case CardState.MouseDrag:
                    pos = Input.mousePosition;
                    card.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
                    card.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    card.transform.position = pos;
                    break;
                case CardState.MouseExit:
                    pos.y = CardLayer.position.y * (Mathf.Abs(0.5f - value) * -1);
                    card.transform.localScale = new Vector3(1f, 1f, 0);
                    card.transform.localRotation = Quaternion.Euler(0, 0, 25 * (0.5f - value));
                    card.transform.localPosition = pos;
                    card.transform.SetAsFirstSibling();
                    break;
                case CardState.CardUse:
                    m_Hand.Remove(card);
                    StartCoroutine(UseCard(card));
                    break;
            }


        }
    }

    private IEnumerator UseCard(CardFrame card)
    {
        card.m_CardBase.Script.OnUse(player);
        Destroy(card.gameObject);
        yield return null;
    }

}

[System.Serializable]
public class CostSystem
{
    public float Cost;

    public int MaxCost = 10;

    public int RecoverCost = 100;
}
