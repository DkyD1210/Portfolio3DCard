using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private Slider LoadSlider;

    [SerializeField]
    private float LoadTimer;

    private float timer;

    void Start()
    {
        StartCoroutine(LoadScene());
        timer = 0f;
    }


    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)SceneType.MainScene);
        asyncOperation.allowSceneActivation = false;
        bool LoadDone = false;

        while (LoadDone == false)
        {
            timer += Time.deltaTime;
            float timeValue = (timer / LoadTimer) * 0.5f;
            float loadValue =  asyncOperation.progress * 0.5f;
            LoadSlider.value = timeValue + loadValue;

            if (timeValue + loadValue >= 1f)
            {
                LoadDone = true;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
