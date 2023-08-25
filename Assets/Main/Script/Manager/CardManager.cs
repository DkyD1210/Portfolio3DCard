using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour
{

    Player player;

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



    void Start()
    {
        List<int> startdeck = new List<int>
        {
            100001,
            100001,
            100001,
            100001,
            100002,
            100002,
            100003,
            100003,
            100003,
            100004,
        };
        foreach(int i in startdeck)
        {
            CardBase card = XmlManager.Instance.TransXmlCard(XmlManager.Instance.GetCardData(i));
            m_Deck.Add(card);
        }
        player = GameManager.Player;
        HandStart = new Vector3((CardLayer.rect.width * 0.5f) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width * 0.5f, 0, 0);
        m_BeforeDummy.AddRange(m_Deck);
        DrawCard(10);


    }



    void Update()
    {
        CardHand();


    }


    private void DrawCard(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (m_BeforeDummy.Count < 1)
            {
                if (m_AfterDummy.Count < 1)
                {
                    return;
                }
                m_BeforeDummy.AddRange(m_AfterDummy);
                m_AfterDummy.Clear();
            }
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

        for (int i = count - 1; i > -1; i--)
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
                    CardUse(card);
                    break;
            }

        }
    }


    private void CardUse(CardFrame card)
    {
        //card.m_CardBase.Script.OnUse(player);
        m_AfterDummy.Add(card.m_CardBase);

        m_Hand.Remove(card);
        Destroy(card.gameObject);
    }



}

