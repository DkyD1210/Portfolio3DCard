using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{


    [SerializeField]
    private Button LoadSaveButton;


    void Start()
    {
        if (SaveManager.instance.GetSaveData() == null)
        {
            LoadSaveButton.interactable = false;
        }
    }

    public void LoadMainScene(bool _use)
    {
        SaveManager.instance.UseSaveData(_use);
        SceneManager.LoadScene((int)SceneType.LoadScene);
    }




    public void ExitApplication()
    {
        Application.Quit();
    }
}
