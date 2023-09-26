using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public bool IsSaveData = false;

    private SaveData saveData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        LoadSaveData();
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

    }



    public void SaveData()
    {
        List<int> list = new List<int>();
        foreach (CardFrame i in CardManager.Instance.m_DeckList)
        {
            list.Add(i.m_CardBase.Id);
        }

        saveData = new SaveData()
        {
            PlayerHp = GameManager.StaticPlayer.m_UnitBase.hp,
            deck = list,
            waveCount = BattleManager.WaveCount
        };

        string data = JsonConvert.SerializeObject(saveData);
        PlayerPrefs.SetString("SaveData", data);


    }

    public void LoadSaveData()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string data = PlayerPrefs.GetString("SaveData");
            saveData = JsonConvert.DeserializeObject<SaveData>(data);
        }
        else
        {
            Debug.Log("세이브 없음");
        }

    }

    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteKey("SaveData");
    }

    public void UseSaveData(bool _value)
    {
        IsSaveData = _value;
    }

    public SaveData GetSaveData()
    {
        return saveData;
    }


}

[System.Serializable]
public class SaveData
{
    public int PlayerHp;

    public List<int> deck;

    public int waveCount;


}
