using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private Camera m_PlayerCamera;

    public static bool m_NoCursor = true;

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
        m_PlayerCamera = Camera.main;
    }


    void Update()
    {
        DeleteCursor();

    }

    private void DeleteCursor()
    {

        Cursor.lockState = CursorLockMode.None;

    }

}
