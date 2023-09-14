using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController m_Controller;

    public GameObject Effect;

    public Animator m_Animator;

    //플레이어 이동
    public Vector3 MoveDir;

    public UnitBase m_UnitBase;

    private float Gravity = 9.81f;

    private bool IsRoll = false;

    private bool GameEnd = false;



    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Controller = GetComponent<CharacterController>();
        m_UnitBase.Init();
        if (SaveManager.instance.IsSaveData == true)
        {
            int _hp = SaveManager.instance.GetSaveData().PlayerHp;
            m_UnitBase.SetHp(_hp);
        }
    }


    void Update()
    {
        //if (m_UnitBase.Ondie())
        {
            //if (GameEnd == false)
            {
                //StartCoroutine(PlayerDie());
            }
            //return;
        }
        PlayerApplyBuff();
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
        m_Animator.SetFloat("Horizontal", MoveDir.x);
        m_Animator.SetFloat("Vertical", MoveDir.z);


        if (Input.GetKey(KeyCode.Space))
        {
            Effect.SetActive(true);
        }


    }

    private void PlayerApplyBuff()
    {
        int count = m_UnitBase.BuffList.Count;
        //if(count <= 0)
        //{
        //    return;
        //}
        for (int i = count - 1; i > -1; i--)
        {
            m_UnitBase.BuffList[i].ActivateBuff();
        }
    }

    public IEnumerator PlayerRollAnima(Vector3 target, float _time)
    {
        m_Animator.SetTrigger("Roll");
        m_Animator.SetFloat("RollSpeed", (1.1f / _time));
        IsRoll = true;
        Vector3 position = transform.position;

        float timer = 0;
        while (timer <= 1)
        {
            timer += Time.deltaTime / _time;
            //m_Controller.Move(Vector3.Lerp(a, target, timer));
            transform.position = Vector3.Lerp(position, target, timer);
            yield return null;
        }
        IsRoll = false;
        m_Animator.SetTrigger("Roll");
    }

    private IEnumerator PlayerDie()
    {
        GameEnd = true;
        m_Animator.SetBool("Die", GameEnd);
        TimeManager.Instance.SetTimeSet(e_GameTime.Slow);
        yield return new WaitForSeconds(3f);
        TimeManager.Instance.SetTimeSet(e_GameTime.Stop);
        UIManager.Instance.GameEnd();

    }
}


