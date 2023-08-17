using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardGame_Xml;

public class CardFrame : MonoBehaviour
{


    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private TMP_Text m_CardCost;

    [SerializeField]
    private TMP_Text m_CardName;

    [SerializeField]
    private TMP_Text m_CardDesc;

    
    public CardBase m_Card;

    void Start()
    {
        m_CardImage = GetComponent<Image>();
        m_CardCost = transform.Find("Cost").GetComponent<TMP_Text>();
        m_CardName = transform.Find("CardName").GetComponent<TMP_Text>();
        m_CardDesc = transform.Find("CardDesc").GetComponent<TMP_Text>();
    }

    void Update()
    {
        CostText();
        NameText();
    }

    private void CostText()
    {
        m_CardCost.text = m_Card.UseCost().ToString();

        if(m_Card.Cost < m_Card.UseCost())
        {
            m_CardCost.color = Color.green;
        }
        else if (m_Card.Cost > m_Card.UseCost())
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
        m_CardName.text = m_Card.Name;
    }

    private void DescText()
    {

    }
}

