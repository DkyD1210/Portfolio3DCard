using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using CardGame_Xml;

public class CardXmlRoot
{
    [XmlElement("Card")]
    private List<CardXmlInfo> CardXmlList = new List<CardXmlInfo>();
}

[Serializable]
public class CardXmlInfo : XmlIdData
{
    [XmlIgnore]
    public XmlId ID
    {
        get
        {
            return new XmlId(this._id);
        }
    }

    [XmlElement("Name")]
    public string Name = "Ä«µå";

    [XmlElement("TextID")]
    public int _textId;

    public XmlId TextID
    {
        get
        {
            return new XmlId(this._textId);
        }
    }

    [XmlElement("Artwork")]
    public string artWork = string.Empty;

    [XmlElement]
    public Rarity Rarity;

    [XmlElement]
    public CardType CardType;

    [XmlElement]
    public CardEffect CardEffect = new CardEffect();
}

[Serializable]
public class CardEffect
{
    [XmlAttribute("Cost")]
    public int Cost;

    [XmlElement]
    public int Damage = 0;
    
    [XmlElement]
    public int Barrier = 0;

    [XmlElement("Script")]
    public string script = string.Empty;

}
