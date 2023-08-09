using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using CardGame_Xml;

public class XmlManager : MonoBehaviour
{

    public static XmlManager Instance;

    private UnitXmlRoots Roots;

    public Dictionary<XmlId, UnitXmlInfo> DataDic = new Dictionary<XmlId, UnitXmlInfo>();



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        LoadXmlData(Path);
    }

    private const string Path = "Assets/Main/Xml/Data/UnitInfo.xml";

    private void SaveXmlDataTest()
    {
        UnitXmlRoots unit = new UnitXmlRoots
        {
            UnitXmlList = new List<UnitXmlInfo>
            {
                new UnitXmlInfo
                {
                    _id = 1,
                    Name = "테스트용유닛1",
                    UnitEffect = new UnitEffect
                    {
                        Hp = 10,
                        Speed = 2,
                        Damage = 1,
                        GetDamageReduce = 0.8f,
                        UnitFaction = Faction.Neutral,
                        UnitType = UnitType.Named
                    }
                },

                new UnitXmlInfo
                {
                    _id = 2,
                    Name = "테스트용유닛2",
                    UnitEffect = new UnitEffect
                    {
                        Hp = 100,
                        Speed = 20,
                        Damage = 10,
                        GetDamageReduce = 1.5f,
                        UnitFaction = Faction.Player,
                        UnitType = UnitType.Boss
                    }
                },
            }
        };


        XmlSerializer serializer = new XmlSerializer(typeof(UnitXmlRoots));


        FileStream fs = new FileStream(Path, FileMode.Create);
        serializer.Serialize(fs, unit);
        fs.Close();
    }

    private void LoadXmlData(string _path)
    {
        /*
        File.Exists(_path);
        Directory.Exists(_path);
        Directory.CreateDirectory(_path);
        */

        using (var stream = XmlReader.Create(_path))
        {
            XmlSerializer unit = new XmlSerializer(typeof(UnitXmlRoots));
            Roots = unit.Deserialize(stream) as UnitXmlRoots;

            foreach (UnitXmlInfo data in Roots.UnitXmlList)
            {
                UnitXmlInfo addData = new UnitXmlInfo();
                addData._id = data._id;
                addData.Name = data.Name;
                addData.UnitEffect = data.UnitEffect;

                DataDic.Add(addData.Id, addData);
            }

            Debug.Log(DataDic);
        }

    }


    public UnitBase TransXml(UnitXmlInfo xmlBase)
    {
        UnitBase unitBase = new UnitBase(xmlBase._id);
        UnitData data = new UnitData(xmlBase.UnitEffect);
        unitBase.UnitData = data;

        return unitBase;
    }


}
