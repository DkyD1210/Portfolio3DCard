using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;


    public List<AudioClip> m_BGMList;

    public List<AudioClip> m_SFXList;


    [SerializeField]
    private AudioSource m_BGMPlayer;

    [SerializeField]
    private List<AudioSource> m_SFXPlayer;


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
        InitPlayer();

        PlayBGM(0);
    }


    private void InitPlayer()
    {
        m_BGMPlayer = GetComponent<AudioSource>();
        m_SFXPlayer.AddRange(GetComponentsInChildren<AudioSource>());
        m_SFXPlayer.Remove(m_BGMPlayer);
    }

    public void PlayBGM(int _Num)
    {
        m_BGMPlayer.clip = m_BGMList[_Num];
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

}
