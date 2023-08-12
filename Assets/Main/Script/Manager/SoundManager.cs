using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;


    public List<AudioClip> m_BGM;

    public List<AudioClip> m_SFX;



    private AudioSource m_BGMPlayer;


    private AudioSource[] m_SFXPlayer;


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

        m_BGMPlayer = GetComponent<AudioSource>();



        m_BGMPlayer.clip = m_BGM[0];
        m_BGMPlayer.Play();

    }


    void Update()
    {
        //InitSFXPlayer();
    }

    private void InitSFXPlayer()
    {
        int count = m_SFXPlayer.Length;
        for(int i = 0; i < count; i++)
        {
            if(m_SFXPlayer[i].isPlaying == false)
            {
                Destroy(m_SFXPlayer[i]);
            }
        }
    }

    public void MusicPlaye(AudioClip _bgm)
    {
        AudioSource sfx = m_BGMPlayer;
        sfx.clip = _bgm;
        sfx.Play();

    }
    

    public void SFXPlaye(AudioClip _sfx)
    {

        AudioSource sfx = m_SFXPlayer[0];
        if(sfx.clip != _sfx)
        { 
            sfx = gameObject.AddComponent<AudioSource>();
            sfx.clip = _sfx;
        }
        
    }

}
