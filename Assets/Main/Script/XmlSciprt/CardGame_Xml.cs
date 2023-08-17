using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

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

        [XmlElement("Name")]
        public string Name = "이름이 없어요";
    }

}