using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using CardGame_Xml;


[XmlRoot("CardDescRoot")]
public class CardLocalizeRoot
{
    [XmlElement("CardDesc")]
    public List<CardDescInfo> CardDescInfoList = new List<CardDescInfo>();
}

[Serializable]
public class CardDescInfo : XmlIdData
{
    [XmlIgnore]
    public XmlId ID
    {
        get
        {
            return new XmlId(this._id);
        }
    }

    public string Desc = "설명 없음";
}
