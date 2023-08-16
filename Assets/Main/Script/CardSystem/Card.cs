using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardGame_Xml;

public class Card : MonoBehaviour
{


    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private TMP_Text m_CardCost;

    [SerializeField]
    private TMP_Text m_CardName;

    [SerializeField]
    private TMP_Text m_CardDesc;

    [SerializeField]
    private CardBase CardData;

    void Start()
    {
        m_CardImage = GetComponent<Image>();
        
    }

    void Update()
    {
        
    }
}

