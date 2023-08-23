using System;
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
}


public class CardFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private TMP_Text m_CardName;

    [SerializeField]
    private TMP_Text m_CardDesc;


    public CardBase m_CardBase;

    public Sprite m_ArtWork;

    public CardState CardState;

    private bool CardUse = false;


    void Start()
    {
        InitCardImage();
        m_CardName = transform.Find("CardName").GetComponent<TMP_Text>();
        m_CardDesc = transform.Find("CardDesc").GetComponent<TMP_Text>();
        m_CardBase.Script = new Script_BaseAttack();
    }

    void Update()
    {
        if (CardUse == true)
        {
            CardState = CardState.CardUse;
            return;
        }
        NameText();
    }



    private void NameText()
    {
        m_CardName.text = m_CardBase.Name;
    }

    private void DescText()
    {
        
    }

    private void InitCardImage()
    {
        m_CardImage = transform.Find("CardImage").GetComponent<Image>();
        m_ArtWork = Resources.Load<Sprite>("CardImage/" + m_CardBase.Artwork);
        m_CardImage.sprite = m_ArtWork;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"���� : {gameObject.name}");
        CardState = CardState.MouseEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"���� : {gameObject.name}");
        CardState = CardState.MouseExit;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"Ŭ���� : {gameObject.name}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log($"�巡���� : {gameObject.name}");
        CardState = CardState.MouseDrag;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log($"�巡�� ���� : {gameObject.name}");
        CardState = CardState.MouseExit;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Input.mousePosition.y >= Screen.height / 3)
        {
            Debug.Log($"����� : {gameObject.name}");
            CardUse = true;
            CardState = CardState.CardUse;
        }

    }
}

