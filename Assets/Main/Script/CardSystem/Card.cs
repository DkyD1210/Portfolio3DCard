using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame_Xml;

public class Card : MonoBehaviour
{

    public Card(CardXmlInfo data, Sprite artWork)
    {
        this.Artwork = artWork;
        this.Cost = data.CardEffect.Cost;
    }    

    private Sprite Artwork;

    private int Cost;

    private string Name;

    private string Description;

    private Rarity Rarity;

    private CardType CardType;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
