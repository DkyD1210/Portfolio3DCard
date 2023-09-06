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


    public List<CardFrame> m_DeckList;

    [Tooltip("뽑을 패")]
    [SerializeField]
    private List<CardFrame> m_BeforeDummyList = new List<CardFrame>();
    [Tooltip("손패")]
    [SerializeField]
    private List<CardFrame> m_HandList = new List<CardFrame>();
    [Tooltip("버린 패")]
    [SerializeField]
    private List<CardFrame> m_AfterDummyList = new List<CardFrame>();

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

    [SerializeField]
    private Transform RewardLayer;

    private List<CardFrame> m_RewardCardList = new List<CardFrame>();
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


    private void SetStartDeck()
    {
        foreach (int i in Startdeck)
        {
            CardBase data = xmlManager.TransXmlCard(xmlManager.GetCardData(i));
            CardFrame card = MakeCard(data, deck);
            m_DeckList.Add(card);
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


    private void CardHand()
    {

        int count = m_HandList.Count;

        if (count <= 0)
        {
            DrawCard(5);
            return;
        }

        for (int i = count - 1; i > -1; i--)
        {

            CardFrame card = m_HandList[i];


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

        m_AfterDummyList.Add(card);

        m_HandList.Remove(card);

        card.transform.SetParent(after, false);

        card.CardState = CardState.MouseExit;
        card.transform.localRotation = Quaternion.Euler(0, 0, 0);


    }

    public void HandSupply()
    {

        int count = m_DeckList.Count;
        for (int i = 0; i < count; i++)
        {
            CardBase data = m_DeckList[i].m_CardBase;
            CardFrame card = MakeCard(data);
            m_BeforeDummyList.Add(card);
            card.transform.SetParent(before, false);
        }
        m_BeforeDummyList = DeckSufle(m_BeforeDummyList);
        DrawCard(5);
    }

    public void DrawCard(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            int beforeCount = m_BeforeDummyList.Count;
            if (beforeCount < 1)
            {
                int afterCount = m_AfterDummyList.Count;
                if (afterCount < 1)
                {
                    return;
                }
                for (int j = 0; j < afterCount; j++)
                {
                    CardFrame card = m_AfterDummyList[0];
                    m_BeforeDummyList.Add(card);
                    m_AfterDummyList.Remove(card);
                    card.transform.SetParent(before, false);
                }
            }
            CardFrame drawCard = m_BeforeDummyList[0];

            drawCard.transform.SetParent(CardLayer, false);
            m_HandList.Add(drawCard);
            m_BeforeDummyList.Remove(drawCard);
        }

    }

    public CardFrame MakeCard(CardBase data)
    {
        GameObject objCard = Instantiate(m_CardObject);

        CardFrame resultCard = objCard.GetComponent<CardFrame>();
        resultCard.m_CardBase = new CardBase(data);
        return resultCard;
    }

    public CardFrame MakeCard(CardBase data, Transform layer)
    {
        GameObject objCard = Instantiate(m_CardObject, layer, false);

        CardFrame resultCard = objCard.GetComponent<CardFrame>();
        resultCard.m_CardBase = new CardBase(data);
        return resultCard;
    }

    public void ClrearDeck()
    {
        CardFrame card = new CardFrame();
        int count = m_BeforeDummyList.Count;
        for (int i1 = count - 1; i1 > -1; i1--)
        {
            card = m_BeforeDummyList[i1];
            Destroy(card.gameObject);
        }
        m_BeforeDummyList.Clear();

        int count2 = m_HandList.Count;
        for (int i2 = count2 - 1; i2 > -1; i2--)
        {
            card = m_HandList[i2];
            Destroy(card.gameObject);
        }
        m_HandList.Clear();

        int count3 = m_AfterDummyList.Count;
        for (int i3 = count3 - 1; i3 > -1; i3--)
        {
            card = m_AfterDummyList[i3];
            Destroy(card.gameObject);
        }
        m_AfterDummyList.Clear();
    }



    public void SetCardReward(int count)
    {
        for(int i = 0; i < count; i++)
        {
            //MakeCard(, RewardLayer);
        }

    }

    public void AddCardReward(CardFrame card)
    {
        card.transform.SetParent(deck, false);
        m_DeckList.Add(card);

        int count = m_RewardCardList.Count;
        for(int i = count -1; i > -1; i--)
        {
            CardFrame frame = m_RewardCardList[i];
            Destroy(frame.gameObject);
        }
        m_RewardCardList.Clear();
    }



}

