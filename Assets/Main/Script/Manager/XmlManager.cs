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

    [SerializeField]
    private UnitXmlRoots Roots;

    //public Dictionary<XmlId, UnitXmlInfo> DataDic = new Dictionary<XmlId, UnitXmlInfo>();
    public Dictionary<int, UnitXmlInfo> DataDic = new Dictionary<int, UnitXmlInfo>();

    private const string Path = "Assets/Main/Xml/Data/";

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
        LoadXmlData("UnitInfo");
    }



    private void SaveXmlDataTest()
    {
        UnitXmlRoots unit = new UnitXmlRoots
        {
            UnitXmlList = new List<UnitXmlInfo>
            {
                new UnitXmlInfo
                {
                    _id = 1,
                    Name = "Å×½ºÆ®¿ëÀ¯´Ö1",
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
                    Name = "Å×½ºÆ®¿ëÀ¯´Ö2",
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

    public void LoadXmlData(string _path)
    {
        string path = Path + _path + ".xml";

        using (var stream = XmlReader.Create(path))
        {
            try
            {
                XmlSerializer unit = new XmlSerializer(typeof(UnitXmlRoots));
                Roots = unit.Deserialize(stream) as UnitXmlRoots;

                foreach (UnitXmlInfo data in Roots.UnitXmlList)
                {
                    UnitXmlInfo addData = new UnitXmlInfo();
                    addData._id = data._id;
                    addData.Name = data.Name;
                    addData.UnitEffect = data.UnitEffect;


                    DataDic.Add(addData.Id.id, addData);
                }
            }
            catch
            {
                Debug.LogError(path + " is Null");
            }


        }
    }


    public UnitBase TransXml(UnitXmlInfo xmlBase)
    {
        UnitBase unitBase = new UnitBase(xmlBase._id);
        UnitData data = new UnitData(xmlBase.UnitEffect);
        unitBase.UnitData = data;

        return unitBase;
    }

    public UnitXmlInfo GetData(int id)
    {
        return GetData(new XmlId(id));
    }

    public UnitXmlInfo GetData(XmlId id)
    {
        UnitXmlInfo result;

        if (DataDic.TryGetValue(id.id, out result))
        {
            Debug.Log("À¯´Ö ¼³Á¤µÊ");
            return result;
        }
        result = new UnitXmlInfo
        {
            _id = id.id,
            Name = "¿¡·¯ À¯´Ö",
            UnitEffect = new UnitEffect
            {
                Hp = -1
            }
        };
        Debug.LogError("À¯´Ö ¿¡·¯³²");
        return result;
    }


}
