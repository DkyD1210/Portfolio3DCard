using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController m_Controller;

    public GameObject Effect;

    public Animator m_Ainimator;

    //플레이어 이동
    public Vector3 MoveDir;

    //카메라 조작
    [SerializeField]
    private GameObject FpsCamera;

    public UnitBase m_UnitBase;

    private float Gravity = 9.81f;

    private bool IsRoll = false;



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
        if (IsRoll == true)
        {
            return;
        }
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

    public IEnumerator PlayerRollAnima(Vector3 target, float _time)
    {
        m_Ainimator.SetTrigger("Roll");
        m_Ainimator.SetFloat("RollSpeed", (1.1f / _time));
        IsRoll = true;
        Vector3 a = transform.position;
        float timer = 0;
        while (timer <= 1)
        {
            timer += Time.deltaTime / _time;
            //m_Controller.Move(Vector3.Lerp(a, target, timer));
            transform.position = Vector3.Lerp(a, target, timer);
            yield return null;
        }
        IsRoll = false;
        m_Ainimator.SetTrigger("Roll");
    }
}


