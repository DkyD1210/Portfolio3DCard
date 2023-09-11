using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    [SerializeField]
    private GameObject CollectionUI;

    [SerializeField]
    private GameObject ConfigUI;

    void Start()
    {
        
    }


    public void LoadMainScene()
    {
        SceneManager.LoadScene((int)SceneType.MainScene);
    }
    public void LoadMainSceneWithSave()
    {
        SaveManager.instance.LoadGameData();
        SceneManager.LoadScene((int)SceneType.MainScene);
    }

    public void ShowCollection()
    {
        CollectionUI.SetActive(!CollectionUI.activeSelf);
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
