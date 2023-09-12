using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ConfigUI;

    void Start()
    {

    }


    public void LoadMainSceneNewGame()
    {
        SaveManager.instance.DeleteSaveData();
        SceneManager.LoadScene((int)SceneType.LoadScene);
    }
    public void LoadMainSceneWithSave()
    {
        SaveManager.instance.LoadGameData();
        if (SaveManager.instance.GetSaveData() != null)
        {
            SceneManager.LoadScene((int)SceneType.LoadScene);
        }
    }

    public void ShowConfig()
    {
        ConfigUI.SetActive(!ConfigUI.activeSelf);
    }


    public void ExitApplication()
    {
        Application.Quit();
    }
}
