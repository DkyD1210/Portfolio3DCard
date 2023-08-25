using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using CardGame_Xml;

public class CardXmlRoot
{
    [XmlElement("Card")]
    public List<CardXmlInfo> CardXmlList = new List<CardXmlInfo>();
}

[Serializable]
public class CardXmlInfo : XmlIdData
{
    [XmlIgnore]
    public XmlId Id
    {
        get
        {
            return new XmlId(this._id);
        }
    }

    [XmlElement("TextID")]
    public int _textId = 0;

    [XmlIgnore]
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
    public CardRangeType CardRange = CardRangeType.Melee;

    [XmlElement]
    public CardEffect CardEffect = new CardEffect();

    [XmlElement]
    public Rarity Rarity = Rarity.Basic;
}

[Serializable]
public class CardEffect
{

    [XmlElement]
    public int Damage = 0;

    [XmlElement]
    public int Barrier = 0;

    [XmlElement("Script")]
    public string script = string.Empty;

}
