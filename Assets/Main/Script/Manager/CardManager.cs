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


    public List<CardFrame> m_Deck;

    [Tooltip("뽑을 패")]
    private List<CardFrame> m_BeforeDummy = new List<CardFrame>();
    [Tooltip("손패")]
    private List<CardFrame> m_Hand = new List<CardFrame>();
    [Tooltip("버린 패")]
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



    void Start()
    {

        player = GameManager.StaticPlayer;
        HandStart = new Vector3((CardLayer.rect.width * 0.5f) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width * 0.5f, 0, 0);
        HandSupply();


    }



    void Update()
    {
        CardHand();

    }

    private void HandSupply()
    {
        if (m_IsFirst == true)
        {
            foreach (int i in Startdeck)
            {
                CardBase data = XmlManager.Instance.TransXmlCard(XmlManager.Instance.GetCardData(i));
                CardFrame card = MakeCard(data);
                m_Deck.Add(card);
            }
        }
        m_BeforeDummy.AddRange(m_Deck);
        m_BeforeDummy = DeckSufle(m_BeforeDummy);
        DrawCard(10);
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
            if (m_BeforeDummy.Count < 1)
            {
                if (m_AfterDummy.Count < 1)
                {
                    return;
                }
                m_BeforeDummy.AddRange(m_AfterDummy);
                m_AfterDummy.Clear();
            }
            CardFrame drawCard = m_BeforeDummy[0];



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
                    pos.y = Input.mousePosition.y;
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
                    card.transform.localRotation = Quaternion.Euler(0, 0, 25 * (0.5f - value));
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
        m_AfterDummy.Add(card);

        m_Hand.Remove(card);
        Destroy(card.gameObject);
    }



}

