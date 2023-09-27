using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [SerializeField]
    private List<AudioClip> m_BGMList;

    [SerializeField]
    private List<AudioClip> m_SFXList;


    [SerializeField]
    private AudioSource m_BGMPlayer;

    [SerializeField]
    private List<AudioSource> m_SFXPlayer;

    [Range(0f, 1f)]
    private float m_BGMVolume = 0.6f;

    [Range(0f, 1f)]
    private float m_SFXVolume = 0.6f;

    [SerializeField]
    private GameObject ConfigUI;

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
        InitAudioPlayer();
    }

    void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        m_BGMPlayer.volume = m_BGMVolume;

        int count = m_SFXPlayer.Count;
        for (int i = 0; i < count; i++)
        {
            m_SFXPlayer[i].volume = m_SFXVolume;
        }
    }

    private void InitAudioPlayer()
    {
        m_BGMPlayer = GetComponent<AudioSource>();
        m_SFXPlayer.AddRange(GetComponentsInChildren<AudioSource>());
        m_SFXPlayer.Remove(m_BGMPlayer);
    }

    public void PlayBGM(int _Num)
    {
        if (_Num >= m_BGMList.Count)
        {
            m_BGMPlayer.Stop();
            return;
        }

        AudioClip BGM = m_BGMList[_Num];

        if (m_BGMPlayer.clip == BGM)
        {
            return;
        }
        m_BGMPlayer.clip = BGM;
        m_BGMPlayer.Play();
    }


    public void PlaySFX(int _Num)
    {
        AudioClip sfx = m_SFXList[_Num];

        int count = m_SFXPlayer.Count;
        for (int i = 0; i < count; i++)
        {
            AudioSource audioSource = m_SFXPlayer[i];
            if (audioSource.isPlaying == false)
            {
                audioSource.clip = sfx;
                audioSource.Play();
                return;
            }
        }

        AudioSource source = gameObject.AddComponent<AudioSource>();
        m_SFXPlayer.Add(source);
        source.clip = sfx;
        source.Play();

    }

    public void SetBGMVolume(float _volume)
    {
        m_BGMVolume = _volume;
    }

    public void SetSFXVolume(float _volume)
    {
        m_SFXVolume = _volume;
    }


    public void ShowConfig()
    {
        ConfigUI.SetActive(!ConfigUI.activeSelf);
    }
}
