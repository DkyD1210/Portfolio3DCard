using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using CardGame_Xml;


namespace CardGame_Xml
{
    public class XmlId
    {

        public readonly int id;

        public XmlId(int _id)
        {
            this.id = _id;
        }
    }
    public class XmlIdData
    {
        [XmlAttribute("ID")]
        public int _id;
    }



}

[Serializable]
[XmlRoot("UnitRoots")]
public class UnitXmlRoots
{
    [XmlElement("Unit")]
    public List<UnitXmlInfo> UnitXmlList = new List<UnitXmlInfo>();
}



[Serializable]
public class UnitXmlInfo : XmlIdData
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
    public string Name = "이름이 없어요";

    [XmlElement]
    public UnitEffect UnitEffect = new UnitEffect();

}

[Serializable]
public class UnitEffect
{
    [XmlElement("Hp")]
    public int Hp;

    [XmlElement]
    public int Speed;

    [XmlElement]
    public int Damage;

    [XmlElement("DamageReduce")]
    public float GetDamageReduce = 1.0f;

    [XmlElement("Faction")]
    public Faction UnitFaction = Faction.Neutral;

    [XmlElement("Type")]
    public UnitType UnitType = UnitType.Defualt;
}

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
    public string Name = "카드";

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

    [XmlElement("Script")]
    public string script = string.Empty;

}


