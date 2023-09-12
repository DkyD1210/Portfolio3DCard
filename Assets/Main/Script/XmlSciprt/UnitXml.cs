using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using CardGame_Xml;


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
    public int Damage = 1;

    [XmlElement("DamageReduce")]
    public float GetDamageReduce = 1.0f;

    [XmlElement("Faction")]
    public Faction UnitFaction;

    [XmlElement("Type")]
    public UnitType UnitType;
}






