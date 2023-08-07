using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController m_PlayerController;



    //플레이어 이동
    private Vector3 m_MoveDir;

    [SerializeField]
    private float m_Speed;

    //카메라 조작
    [SerializeField]
    private GameObject FpsCamera;




    void Start()
    {
        m_PlayerController = GetComponent<CharacterController>();
    }


    void Update()
    {
        UnitMove();
        CameraRoation();
    }


    private void UnitMove()
    {
        m_MoveDir.x = Input.GetAxisRaw("Vertical");
        m_MoveDir.z = Input.GetAxisRaw("Horizontal");

        m_PlayerController.Move(m_MoveDir * m_Speed * Time.deltaTime);

    }

    private void CameraRoation()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

    }

}


