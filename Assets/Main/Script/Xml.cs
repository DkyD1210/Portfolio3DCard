using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


namespace CardGame_Xml
{
    [XmlRoot("UnitRoots")]
    public class UnitXmlRoots
    {
        [XmlElement("Unit")]
        public List<UnitXmlInfo> UnitXmlList;
    }

    public class UnitXmlInfo
    {

        [XmlAttribute("ID")]
        public int _id;


        [XmlIgnore]
        public XmlId Id
        {
            get
            {
                return new XmlId(this._id);
            }
        }

        [XmlAttribute("Name")]
        public string Name = "이름이 없어요";

        [XmlElement]
        public UnitEffect UnitEffect = new UnitEffect();

    }

    public class UnitEffect
    {
        [XmlElement]
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

    public class XmlId : IEquatable<XmlId>, IEquatable<int>, IComparable<XmlId>
    {

        public readonly int id;

        public XmlId(int _id)
        {
            this.id = _id;
        }


        public bool Equals(XmlId other)
        {
            return this.id == other.id;
        }

        public bool Equals(int other)
        {
            return this.id == other;
        }

        public int CompareTo(XmlId other)
        {
            int num = this.id.CompareTo(other.id);
            return num;
        }
    }
}