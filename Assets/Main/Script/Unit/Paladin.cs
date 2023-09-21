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
        Run,
        JumpAttack,
        SlashAttack,
        TurnAttack,
        Death,
    }

    private Animator m_Animator;

    private NavMeshAgent m_NavMesh;

    private Player m_player;

    private Vector3 MoveDir;

    public UnitBase m_UnitBase;

    [SerializeField]
    private BossState m_State;

    [SerializeField]
    private float m_Distance;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMesh = GetComponent<NavMeshAgent>();
        m_player = GameManager.StaticPlayer;
        m_UnitBase.Init();
        m_NavMesh.speed = m_UnitBase.Speed;
    }


    void Update()
    {
        if (m_State == BossState.Death)
        {
            return;
        }

        m_Distance = Vector3.Distance(m_player.transform.position, transform.position);

        Moving();
        BossAnimate();
    }

    private void Moving()
    {

        if (m_State == BossState.Run || m_State == BossState.SideMove)
        {
            transform.LookAt(m_player.transform);
        }

        if (m_State == BossState.Defualt)
        {
            MoveDir = Vector3.zero;

            int randAct = Random.Range(0, 3);
            switch (randAct)
            {

                case 0:
                    StartCoroutine(RunFoward());
                    break;

                default:
                    float time = Random.Range(0.75f, 2.35f);
                    StartCoroutine(MoveSide(time));
                    break;

            }

        }


    }

    private void BossAnimate()
    {
        m_Animator.SetFloat("Horizontal", MoveDir.x);
        m_Animator.SetFloat("Vertical", MoveDir.z);

        if (m_UnitBase.Ondie() == true)
        {
            StartCoroutine(BossDie());
            return;
        }

    }

    private IEnumerator MoveSide(float _Time)
    {
        Debug.Log("<color=red>옆으로 이동</color>");

        m_State = BossState.SideMove;

        float randX = Random.Range(0, 2);
        MoveDir.x = randX == 0 ? -1 : 1;


        MoveDir.z = m_Distance >= 16 ? 1 : -1;

        float timer = 0f;
        while (timer <= _Time)
        {
            timer += Time.deltaTime;

            if (m_Distance <= 5)
            {
                MoveDir.z = -1;
                m_NavMesh.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime * 0.45f);

                yield return null;
            }
            else
            {
                m_NavMesh.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime * 0.2f);
                yield return null;
            }

        }

        if (m_State == BossState.SideMove)
        {
            m_State = BossState.Defualt;
        }
    }

    private IEnumerator RunFoward()
    {
        Debug.Log("<color=red>달리기</color>");

        m_State = BossState.Run;

        MoveDir.x = 0f;
        MoveDir.z = 2f;

        int rand = Random.Range(0, 5);


        switch (rand)
        {
            case 0:
                while (m_Distance >= 10)
                {
                    m_Distance = Vector3.Distance(m_player.transform.position, transform.position);
                    m_NavMesh.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime * 0.8f);
                    yield return null;
                }
                StartCoroutine(JumpAttack());
                break;
            case 1:
                while (m_Distance >= 6)
                {
                    m_Distance = Vector3.Distance(m_player.transform.position, transform.position);
                    m_NavMesh.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime * 0.8f);
                    yield return null;
                }
                StartCoroutine(TurnAttack());
                break;

            default:
                while (m_Distance >= 3.5)
                {
                    m_Distance = Vector3.Distance(m_player.transform.position, transform.position);
                    m_NavMesh.Move(transform.rotation * MoveDir * m_UnitBase.Speed * Time.deltaTime * 0.8f);
                    yield return null;
                }
                StartCoroutine(SlashAttack());
                break;
        }

        if (m_State == BossState.Run)
        {
            m_State = BossState.Defualt;
        }
    }

    private IEnumerator JumpAttack()
    {
        Debug.Log("<color=red>점프공격</color>");

        m_State = BossState.JumpAttack;

        m_Animator.SetTrigger("JumpAttack");
        transform.LookAt(m_player.transform);

        Vector3 position = transform.position;
        Vector3 tagetPos = m_player.transform.position;
        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / 1.48f;

            Vector3 JumpPos = Vector3.Lerp(position, tagetPos, timer);
            transform.position = JumpPos;
            yield return null;
        }

        RaycastHit[] m_HitTarget = Physics.SphereCastAll(transform.position, transform.lossyScale.magnitude * 1.6f, Vector3.forward, 0f, LayerMask.GetMask("Player"));
        int count = m_HitTarget.Length;
        if (count >= 1)
        {
            for (int i = count - 1; i > -1; i--)
            {
                GameObject unit = m_HitTarget[i].transform.gameObject;
                UnitBase target = unit.GetUnitBase();
                target.LoseHp(m_UnitBase.Damage * 2);

                Debug.Log("보스 점프 공격함");
            }
        }

        yield return new WaitForSeconds(0.3f);

        if (m_State == BossState.JumpAttack)
        {
            m_State = BossState.Defualt;
        }
    }


    private IEnumerator SlashAttack()
    {
        Debug.Log("<color=red>기본공격</color>");
        m_State = BossState.SlashAttack;
        m_Animator.SetTrigger("SlashAttack");

        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / 0.45f;

            m_NavMesh.Move(transform.rotation * Vector3.forward * m_UnitBase.Speed * Time.deltaTime * 0.6f);

            yield return null;
        }

        RaycastHit[] m_HitTarget = Physics.SphereCastAll(transform.position, transform.lossyScale.magnitude * 1.15f, Vector3.forward, 0f, LayerMask.GetMask("Player"));
        int count = m_HitTarget.Length;
        if (count >= 1)
        {
            for (int i = count - 1; i > -1; i--)
            {
                GameObject unit = m_HitTarget[i].transform.gameObject;
                UnitBase target = unit.GetUnitBase();
                target.LoseHp(m_UnitBase.Damage);

                Debug.Log("보스 일반 공격함");
            }
        }

        yield return new WaitForSeconds(0.45f);

        if (m_State == BossState.SlashAttack)
        {
            m_State = BossState.Defualt;
        }
    }

    private IEnumerator TurnAttack()
    {
        Debug.Log("<color=red>회전공격 </color>");
        m_State = BossState.TurnAttack;
        m_Animator.SetTrigger("TurnAttack");

        MoveDir = Vector3.zero;

        float timer = 0f;
        bool CanAttack = true;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / 1.05f;


            m_NavMesh.Move(transform.rotation * Vector3.forward * m_UnitBase.Speed * Time.deltaTime * 1.5f);

            if (CanAttack == true)
            {
                RaycastHit[] m_HitTarget = Physics.SphereCastAll(transform.position, transform.lossyScale.magnitude * 1.1f, Vector3.forward, 0f, LayerMask.GetMask("Player"));
                int count = m_HitTarget.Length;
                if (count >= 1)
                {
                    for (int i = count - 1; i > -1; i--)
                    {
                        GameObject unit = m_HitTarget[i].transform.gameObject;
                        UnitBase target = unit.GetUnitBase();
                        target.LoseHp(m_UnitBase.Damage);

                        Debug.Log("보스 회전 공격함");
                    }
                    CanAttack = false;
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        if (m_State == BossState.TurnAttack)
        {
            m_State = BossState.Defualt;
        }
    }

    private IEnumerator BossDie()
    {
        m_State = BossState.Death;
        m_Animator.SetBool("Die", true);
        yield return new WaitForSeconds(3.3f);

    }

}
