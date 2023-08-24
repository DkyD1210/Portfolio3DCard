using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController m_Controller;

    public GameObject Effect;

    //플레이어 이동
    private Vector3 MoveDir;

    //카메라 조작
    [SerializeField]
    private GameObject FpsCamera;

    public UnitBase m_UnitBase;




    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        PlayerMove();
        PlayerRotating();
    }


    private void PlayerMove()
    {
        MoveDir.x = Input.GetAxisRaw("Horizontal");
        MoveDir.z = Input.GetAxisRaw("Vertical");

        m_Controller.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.Space))
        {
            Effect.SetActive(true);
        }
    }

    private void PlayerRotating()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, -100 * Time.deltaTime, 0));
        }
    }

}


