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
    private TextMeshProUGUI m_CardCost;

    [SerializeField]
    private TextMeshProUGUI m_CardName;

    [SerializeField]
    private TextMeshProUGUI m_CardDesc;


    [SerializeField]
    private CardData CardData;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

[Serializable]
public class CardData
{
    //Xml È®ÀÎ¿ë ID
    private int _id;

    public int Id
    {
        get { return this._id; }
    }

    public Sprite Artwork;

    public int Cost;

    public string Name;

    public string Description;

    public Rarity Rarity;

    public CardType Type;
}
