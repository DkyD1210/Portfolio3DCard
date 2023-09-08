using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instace;

    public bool IsSaveData = false;

    private SaveData saveData;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }



    public void SaveGameData()
    {
        List<int> list = new List<int>();
        foreach(CardFrame i in CardManager.Instance.m_DeckList)
        {
            list.Add(i.m_CardBase.Id);
        }

        saveData = new SaveData()
        {
            playerData = GameManager.StaticPlayer.m_UnitBase,
            deck = list,
            waveCount = BattleManager.WaveCount
        };

        string data = JsonConvert.SerializeObject(saveData);
        PlayerPrefs.SetString("SaveData", data);


    }

    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string data = PlayerPrefs.GetString("SaveData");
            SaveData _savedata = JsonConvert.DeserializeObject<SaveData>(data);
            saveData = _savedata;
            IsSaveData = true;
            SceneManager.LoadScene((int)SceneType.MainScene);
        }
        else
        {
            Debug.LogError("SaveData is Null");
        }

    }

    public SaveData GetSaveData()
    {
        return saveData;
    }

}

[System.Serializable]
public class SaveData
{

    public UnitBase playerData;

    public List<int> deck;

    public int waveCount;


}
