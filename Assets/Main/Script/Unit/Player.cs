using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController m_PlayerController;

    public GameObject Effect;

    //플레이어 이동
    private Vector3 m_MoveDir;

    //카메라 조작
    [SerializeField]
    private GameObject FpsCamera;

    public UnitBase m_UnitBase;




    void Start()
    {
        m_PlayerController = GetComponent<CharacterController>();
    }


    void Update()
    {
        PlayerMove();
        PlayerRotating();
    }


    private void PlayerMove()
    {
        m_MoveDir.x = Input.GetAxisRaw("Horizontal");
        m_MoveDir.z = Input.GetAxisRaw("Vertical");

        m_PlayerController.Move(transform.rotation * m_MoveDir * m_UnitBase.Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space))
        {
            Effect.SetActive(true);
            float timer = 0;
            timer += Time.deltaTime;
            if (timer >= 4)
            {
                timer = 0;
                Effect.SetActive(false);
            }
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


