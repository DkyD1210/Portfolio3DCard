using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardGame_Xml;
using UnityEngine.EventSystems;
public enum CardState
{
    MouseExit,
    MouseEnter,
    MouseDrag,
    CardUse,
    CardSelect,
}


public class CardFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private Image m_CardFrame;

    [SerializeField]
    private TMP_Text m_CardName;

    [SerializeField]
    private TMP_Text m_CardDesc;


    public CardBase m_CardBase;

    public Sprite m_ArtWork;

    public CardState CardState;


    [SerializeField]
    private List<Color> m_RairityColorList = new List<Color>();

    void Start()
    {
        m_CardFrame = transform.Find("CardFrame").GetComponent<Image>();
        m_CardFrame.color = m_RairityColorList[(int)m_CardBase.Rarity];

        InitCardImage();
        InitNameText();
        DescText();

    }

    void Update()
    {

    }

    private void InitCardImage()
    {
        m_CardImage = transform.Find("CardImage").GetComponent<Image>();
        m_ArtWork = Resources.Load<Sprite>("CardImage/" + m_CardBase.Artwork);
        m_CardImage.sprite = m_ArtWork;
    }
    private void InitNameText()
    {
        m_CardName = transform.Find("CardName").GetComponent<TMP_Text>();
        m_CardName.text = m_CardBase.Script.CardName;
    }

    private void DescText()
    {
        m_CardDesc = transform.Find("CardDesc").GetComponent<TMP_Text>();
        m_CardDesc.text = m_CardBase.Script.CardDesc;

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardState != CardState.CardUse && CardState != CardState.CardSelect)
        {
            CardState = CardState.MouseEnter;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardState != CardState.CardUse && CardState != CardState.CardSelect)
        {
            CardState = CardState.MouseExit;
        }

    }


    public void OnDrag(PointerEventData eventData)
    {
        if (CardState != CardState.CardUse && CardState != CardState.CardSelect)
        {
            CardState = CardState.MouseDrag;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CardState != CardState.CardUse && CardState != CardState.CardSelect)
        {
            CardState = CardState.MouseExit;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CardState != CardState.CardUse && CardState != CardState.CardSelect)
        {
            if (Input.mousePosition.y >= Screen.height / 3)
            {
                Debug.Log($"»ç¿ëÇÔ : {gameObject.name}");
                CardState = CardState.CardUse;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardState == CardState.CardSelect)
        {
            CardManager.Instance.AddCardReward(this);
            CardManager.Instance.ClearCardReward();
            UIManager.Instance.ShowWaveEndUI();
            UIManager.Instance.ShowCardRewardUI();
        }
    }





}

