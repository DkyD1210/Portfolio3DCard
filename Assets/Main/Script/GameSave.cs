using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instace;

    public bool IsSaveData = false;

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

    public SaveData saveData;

    public void SaveGameData()
    {
        saveData = new SaveData()
        {
            playerData = GameManager.StaticPlayer.m_UnitBase,
            deck = CardManager.Instance.m_DeckList,
            waveCount = BattleManager.WaveCount
        };


        
    }

    public void LoadGameData()
    {
        IsSaveData = true;
    }


}

[System.Serializable]
public class SaveData
{

    public UnitBase playerData;

    public List<CardFrame> deck;

    public int waveCount;


}
