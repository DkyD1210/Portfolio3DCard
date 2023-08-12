using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController m_PlayerController;



    //�÷��̾� �̵�
    private Vector3 m_MoveDir;

    private UnitBaseModel m_UnitBaseModel;

    [SerializeField]
    private float m_Speed;

    //ī�޶� ����
    [SerializeField]
    private GameObject FpsCamera;




    void Start()
    {
        m_PlayerController = GetComponent<CharacterController>();
        m_UnitBaseModel = GetComponent<UnitBaseModel>();
        m_Speed = m_UnitBaseModel._unitBase.UnitData.Speed;
    }


    void Update()
    {
        PlayerMove();
        CameraRoating();
    }


    private void PlayerMove()
    {
        m_MoveDir.x = Input.GetAxisRaw("Horizontal");
        m_MoveDir.z = Input.GetAxisRaw("Vertical");

        m_PlayerController.Move(transform.rotation * m_MoveDir * m_Speed * Time.deltaTime);

    }

    private void CameraRoating()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

    }

}


