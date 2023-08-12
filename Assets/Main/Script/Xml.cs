using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using CardGame_Xml;


namespace CardGame_Xml
namespace CardGame_Xml 
{
<<<<<<< HEAD
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
=======
    public class XmlId  : IEquatable<XmlId>, IEquatable<int>, IComparable<XmlId>
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public XmlId(int _id)
        {
            this._id = _id;
        }

        public bool Equals(XmlId other)
        {
            return this._id == other._id;
        }

        public bool Equals(int other)
        {
            return this._id == other;
        }

        public int CompareTo(XmlId other)
        {
            int num = this._id.CompareTo(other._id);
            return num;
        }
    }


    public class XmlIdData
    {
        [XmlAttribute("ID")]
        public int _id;
    }
}

[XmlRoot("UnitRoots")]
public class UnitXmlRoots
{
    [XmlElement("Unit")]
    public List<UnitXmlInfo> UnitXmlList = new List<UnitXmlInfo>();
}

public class UnitXmlInfo : XmlIdData
{


    [XmlIgnore]
    public XmlId Id
    {
        get
        {
            return new XmlId(this._id);
        }
    }
>>>>>>> origin/main

    [XmlElement("Name")]
    public string Name = "이름이 없어요";

    [XmlElement]
    public UnitEffect UnitEffect = new UnitEffect();

}

<<<<<<< HEAD
[Serializable]
=======

>>>>>>> origin/main
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

<<<<<<< HEAD
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

=======
>>>>>>> origin/main

