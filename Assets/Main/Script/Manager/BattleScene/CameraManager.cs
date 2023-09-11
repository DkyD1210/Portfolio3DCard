using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField]
    private CinemachineVirtualCamera m_PlayerCamera;

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
        InitCameraToPlayer();
    }


    void Update()
    {
        DeleteCursor();
    }

    private void InitCameraToPlayer()
    {
        m_PlayerCamera.LookAt = GameManager.StaticPlayer.transform;
        m_PlayerCamera.Follow = GameManager.StaticPlayer.transform;
    }

    private void DeleteCursor()
    {

        Cursor.lockState = CursorLockMode.None;

    }


}
