using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.Jobs;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;


    private XmlManager xmlManager;


    Player player;

    [SerializeField]
    private GameObject m_CardObject;

    [Header("카드 UI 공간")]
    [SerializeField]
    private RectTransform CardLayer;
    private Vector3 HandStart;
    private Vector3 HandEnd;

    [SerializeField]
    private Transform deck;

    [SerializeField]
    private Transform before;

    [SerializeField]
    private Transform after;




    public List<CardFrame> m_Deck;

    [Tooltip("뽑을 패")]
    [SerializeField]
    private List<CardFrame> m_BeforeDummy = new List<CardFrame>();
    [Tooltip("손패")]
    [SerializeField]
    private List<CardFrame> m_Hand = new List<CardFrame>();
    [Tooltip("버린 패")]
    [SerializeField]
    private List<CardFrame> m_AfterDummy = new List<CardFrame>();

    private bool m_IsFirst = true;

    private List<int> Startdeck = new List<int>
        {
            //기본공격
            100001,
            100001,
            100001,
            100001,

            //원거리 공격
            100002,
            100002,

            //구르기
            100003,
            100003,
            100003,

            //도주
            100004,
        };

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
        xmlManager = XmlManager.Instance;
        player = GameManager.StaticPlayer;
        HandStart = new Vector3((CardLayer.rect.width * 0.5f) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width * 0.5f, 0, 0);
        if (m_IsFirst == true)
        {
            SetStartDeck();
            m_IsFirst = false;
        }

    }



    void Update()
    {
        CardHand();

    }


    public void HandSupply()
    {

        int count = m_Deck.Count;
        for (int i = 0; i < count; i++)
        {
            CardBase data = m_Deck[i].m_CardBase;
            CardFrame card = MakeCard(data);
            m_BeforeDummy.Add(card);
            card.transform.SetParent(before, false);
        }
        m_BeforeDummy = DeckSufle(m_BeforeDummy);
        DrawCard(5);
    }

    private void SetStartDeck()
    {
        foreach (int i in Startdeck)
        {
            CardBase data = xmlManager.TransXmlCard(xmlManager.GetCardData(i));
            CardFrame card = MakeCard(data);
            m_Deck.Add(card);
            card.transform.SetParent(deck, false);
        }
    }

    private List<CardFrame> DeckSufle(List<CardFrame> _list)
    {
        List<CardFrame> resultList = new List<CardFrame>();

        int count = _list.Count;
        for (int i = count - 1; i > -1; i--)
        {
            int randomcard = Random.Range(0, i);
            CardFrame card = _list[randomcard];
            resultList.Add(card);
            _list.Remove(card);
        }

        return resultList;
    }

    private void DrawCard(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            int beforeCount = m_BeforeDummy.Count;
            if (beforeCount < 1)
            {
                int afterCount = m_AfterDummy.Count;
                if (afterCount < 1)
                {
                    return;
                }
                for (int j = 0; j < afterCount; j++)
                {
                    CardFrame card = m_AfterDummy[0];
                    m_BeforeDummy.Add(card);
                    m_AfterDummy.Remove(card);
                    card.transform.SetParent(before, false);
                }
            }
            CardFrame drawCard = m_BeforeDummy[0];

            drawCard.transform.SetParent(CardLayer, false);
            m_Hand.Add(drawCard);
            m_BeforeDummy.Remove(drawCard);
        }

    }

    private CardFrame MakeCard(CardBase data)
    {
        GameObject objCard = Instantiate(m_CardObject, CardLayer);

        CardFrame resultCard = objCard.GetComponent<CardFrame>();
        resultCard.m_CardBase = new CardBase(data);
        return resultCard;
    }


    private void CardHand()
    {

        int count = m_Hand.Count;

        if (count <= 0)
        {
            DrawCard(5);
            return;
        }

        for (int i = count - 1; i > -1; i--)
        {

            CardFrame card = m_Hand[i];


            float value = (float)(i + 1) / (count + 1);
            Vector3 pos = Vector3.Lerp(HandStart, HandEnd, value);

            switch (card.CardState)
            {
                case CardState.MouseEnter:
                    pos.y = CardLayer.transform.position.y + 25;
                    card.transform.localPosition = pos;

                    card.transform.localScale = new Vector3(1.2f, 1.2f, 2f);
                    card.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
                    card.transform.localPosition = pos;

                    card.transform.localScale = new Vector3(1f, 1f, 0);
                    card.transform.localRotation = Quaternion.Euler(0, 0, 30 * (0.5f - value));
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
        card.m_CardBase.Script.OnUse(player, card.m_CardBase);

        m_AfterDummy.Add(card);

        card.transform.SetParent(after, false);

        card.CardState = CardState.MouseExit;
        card.CardUse = false;
        card.transform.localRotation = Quaternion.Euler(0, 0, 0);

        m_Hand.Remove(card);

    }

    public void ClrearDeck()
    {
        CardFrame card = new CardFrame();
        int count = m_BeforeDummy.Count;
        for (int i1 = count - 1; i1 >= 0; i1--)
        {
            card = m_BeforeDummy[i1];
            m_BeforeDummy.Remove(card);
            Destroy(card.gameObject);
        }

        int count2 = m_Hand.Count;
        for (int i2 = count - 1; i2 >= 0; i2--)
        {
            card = m_Hand[i2];
            m_Hand.Remove(card);
            Destroy(card.gameObject);
        }

        int count3 = m_AfterDummy.Count;
        for (int i3 = count - 1; i3 >= 0; i3--)
        {
            card = m_Hand[i3];
            m_Hand.Remove(card);
            Destroy(card.gameObject);
        }
    }


}

