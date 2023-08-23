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
    private UnitXmlRoots _unitXmlRoots;

    [SerializeField]
    private CardXmlRoot _cardXmlRoot;

    //public Dictionary<XmlId, UnitXmlInfo> DataDic = new Dictionary<XmlId, UnitXmlInfo>();
    public Dictionary<int, UnitXmlInfo> UnitDataDic = new Dictionary<int, UnitXmlInfo>();

    public Dictionary<int, CardXmlInfo> CardDataDic = new Dictionary<int,CardXmlInfo>();

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
        CreateXmlDataTest(XmlPath.CardInfo);
        LoadXmlData(XmlPath.UnitInfo);
        //InitSprite();
    }



    private void CreateXmlDataTest(XmlPath _path)
    {
        /*
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
        */
        CardXmlRoot cardXmlRoot = new CardXmlRoot
        {
            CardXmlList = new List<CardXmlInfo>
            {
                new CardXmlInfo
                {
                    _id = 1,
                    Name = "테스트용_기본공격",
                    _textId = 1,
                    artWork = string.Empty,
                    CardRange = CardRangeType.Melee,
                    CardEffect = new CardEffect
                    {
                        Damage = 7,
                        Barrier = 0,
                        script = string.Empty,
                    },
                    Rarity = Rarity.Basic
                }
            }
        };
        XmlSerializer serializer = new XmlSerializer(typeof(CardXmlRoot));


        string path = Path + _path + ".xml";

        FileStream fs = new FileStream(path, FileMode.Create);
        serializer.Serialize(fs, cardXmlRoot);
        fs.Close();
    }

    public void LoadXmlData(XmlPath _path)
    {
        string path = Path + _path + ".xml";

        using (var stream = XmlReader.Create(path))
        {
            try
            {
                XmlSerializer unit = new XmlSerializer(typeof(UnitXmlRoots));
                _unitXmlRoots = unit.Deserialize(stream) as UnitXmlRoots;

                foreach (UnitXmlInfo data in _unitXmlRoots.UnitXmlList)
                {
                    UnitXmlInfo addData = new UnitXmlInfo();
                    addData._id = data._id;
                    addData.Name = data.Name;
                    addData.UnitEffect = data.UnitEffect;


                    UnitDataDic.Add(addData.Id.id, addData);
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

        if (UnitDataDic.TryGetValue(id.id, out result))
        {
            Debug.Log("유닛 설정됨");
            return result;
        }
        result = new UnitXmlInfo
        {
            _id = id.id,
            Name = "에러 유닛",
            UnitEffect = new UnitEffect
            {
                Hp = -1
            }
        };
        Debug.LogError("유닛 에러남");
        return result;
    }

    /*
    public List<Texture2D> spriteList;

    private void InitSprite()
    {
        string path = "Assets/Resources";
        Byte[] spriteSpr = File.ReadAllBytes(path);
        if(spriteSpr.Length > 0)
        {
            Texture2D _sprite = new Texture2D(0, 0);
            _sprite.LoadImage(spriteSpr);
            spriteList.Add(_sprite);
        }
    }
    */

}
