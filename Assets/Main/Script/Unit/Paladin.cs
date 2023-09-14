using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Paladin : MonoBehaviour
{

    private enum BossState
    {
        Defualt,
        SideMove,
        JumpAttack,
    }

    private Animator m_Animator;

    private NavMeshAgent m_Agent;

    private Player m_player;

    private Vector3 MoveDir;

    public UnitBase m_UnitBase;

    [SerializeField]
    private BossState m_State;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_player = GameManager.StaticPlayer;
        //m_UnitBase.Init();
        StartCoroutine(LookPlayer());
    }


    void Update()
    {
        Moving();
        BossAnimation();
    }

    private void Moving()
    {

        if (m_State == BossState.Defualt)
        {
            MoveDir = Vector3.zero;

            int rand = 0;
            switch (rand)
            {

                case 0:
                    StartCoroutine(MoveSide(5f));
                    break;

                case 1:
                    StartCoroutine(JumpAttack());
                    break;

            }

        }


    }

    private void BossAnimation()
    {
        m_Animator.SetFloat("Horizontal", MoveDir.x);
        m_Animator.SetFloat("Vertical", MoveDir.z);
    }


    private IEnumerator LookPlayer()
    {
        while (true) //(m_UnitBase.Ondie() == false)
        {
            if (m_State == BossState.Defualt)
            {
                transform.LookAt(m_player.transform);
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator MoveSide(float _Time)
    {
        m_State = BossState.SideMove;

        float _distance = Vector3.Distance(m_player.transform.position, transform.position);
        float randX = Random.Range(0, 2);
        MoveDir.x = randX == 0 ? -1 : 1;


        MoveDir.z = _distance >= 10 ? -1 : 1;

        float timer = 0f;
        while (timer <= _Time)
        {
            _distance = Vector3.Distance(m_player.transform.position, transform.position);

            if (_distance <= 10)
            {
                StartCoroutine(JumpAttack());
                break;
            }

            timer += Time.deltaTime;
            m_Agent.Move(transform.rotation * MoveDir * Time.deltaTime);       //m_UnitBase.Speed *
            yield return null;
        }
        if (m_State == BossState.SideMove)
        {
            m_State = BossState.Defualt;
        }
    }


    private IEnumerator JumpAttack()
    {
        m_State = BossState.JumpAttack;

        m_Animator.SetTrigger("JumpAttack");
        transform.LookAt(m_player.transform);

        Vector3 position = transform.position;
        Vector3 tagetPos = m_player.transform.position;
        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / 1.48f;

            Vector3 _distance = Vector3.Lerp(position, tagetPos, timer);
            transform.position = _distance;
            yield return null;
        }

        RaycastHit[] m_HitTarget = Physics.SphereCastAll(transform.position, transform.lossyScale.magnitude * 1.8f, Vector3.forward, 0f, LayerMask.GetMask("Player"));
        int count = m_HitTarget.Length;
        if (count >= 1)
        {
            for (int i = count - 1; i > -1; i--)
            {
                GameObject unit = m_HitTarget[i].transform.gameObject;
                UnitBase target = unit.GetUnitBase();
                target.LoseHp(38);

                Debug.Log("보스 점프 공격함");
            }
        }
        if (m_State == BossState.JumpAttack)
        {
            m_State = BossState.Defualt;
        }
    }

}
