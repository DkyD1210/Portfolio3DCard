using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController m_Controller;

    public GameObject Effect;

    private Animator m_Ainimator;

    //플레이어 이동
    private Vector3 MoveDir;

    //카메라 조작
    [SerializeField]
    private GameObject FpsCamera;

    public UnitBase m_UnitBase;

    private float Gravity = 9.81f;



    void Start()
    {
        m_Ainimator = GetComponent<Animator>();
        m_Controller = GetComponent<CharacterController>();
        m_UnitBase.Init();
    }


    void Update()
    {
        PlayerMove();
        PlayerGravity();
        PlayerRotating();
        PlayerAnimation();
    }


    private void PlayerMove()
    {
        //전후
        MoveDir.z = Input.GetAxisRaw("Vertical");
        //좌우
        MoveDir.x = Input.GetAxisRaw("Horizontal");

        if (MoveDir.z < 0)
        {
            m_Controller.Move(transform.rotation * (MoveDir * m_UnitBase.Speed * Time.deltaTime) * 0.6f);
        }
        else
        {
            m_Controller.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime);
        }

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

    private void PlayerGravity()
    {
        if (m_Controller.isGrounded == false)
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }
    }

    private void PlayerAnimation()
    {
        m_Ainimator.SetFloat("Horizontal", MoveDir.x);
        m_Ainimator.SetFloat("Vertical", MoveDir.z);


    }
}


