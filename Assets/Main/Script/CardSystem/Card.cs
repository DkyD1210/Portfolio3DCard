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
    private TMP_Text m_CardCost;

    [SerializeField]
    private TMP_Text m_CardName;

    [SerializeField]
    private TMP_Text m_CardDesc;


    public CardBase m_CardBase;



    public CardState CardState;

    private bool CardUse = false;


    void Start()
    {
        m_CardImage = GetComponent<Image>();
        m_CardCost = transform.Find("Cost").GetComponent<TMP_Text>();
        m_CardName = transform.Find("CardName").GetComponent<TMP_Text>();
        m_CardDesc = transform.Find("CardDesc").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (CardUse == true)
        {
            CardState = CardState.CardUse;
            return;
        }
        CostText();
        NameText();
    }

    private void CostText()
    {
        m_CardCost.text = m_CardBase.UseCost().ToString();

        if (m_CardBase.Cost < m_CardBase.UseCost())
        {
            m_CardCost.color = Color.green;
        }
        else if (m_CardBase.Cost > m_CardBase.UseCost())
        {
            m_CardCost.color = Color.red;
        }
        else
        {
            m_CardCost.color = Color.black;
        }
    }

    private void NameText()
    {
        m_CardName.text = m_CardBase.Name;
    }

    private void DescText()
    {

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

