using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private BattleManager battleManager = BattleManager.Instance;

    private NavMeshAgent m_NavMesh;


    private Animator m_Animator;

    private Player m_player;

    public UnitBase m_UnitBase;

    private bool IsAttack = false;

    private RaycastHit[] m_HitTarget;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMesh = GetComponent<NavMeshAgent>();
        m_player = GameManager.StaticPlayer;
    }

    void Update()
    {
        EnemyMove();
        EnemyRayCast();
    }

    private void EnemyMove()
    {
        if (IsAttack == false)
        {
            transform.LookAt(m_player.transform);
            m_NavMesh.speed = m_UnitBase.Speed;
            m_NavMesh.SetDestination(m_player.transform.position);

        }
    }

    private void EnemyRayCast()
    {
        m_HitTarget = Physics.BoxCastAll(transform.position, transform.lossyScale * 1.2f, Vector3.forward, transform.rotation, 2f, LayerMask.GetMask("Player"));
        if (IsAttack == true)
        {
            return;
        }
        foreach (RaycastHit hit in m_HitTarget)
        {

            Debug.Log("플레이어 발견");
            StartCoroutine(EnemyAttack(hit));
        }
    }

    private IEnumerator EnemyAttack(RaycastHit hit)
    {
        IsAttack = true;
        m_Animator.SetBool("Attack", IsAttack);
        yield return new WaitForSeconds(1.5f);
        int count = m_HitTarget.Length;
        if (count >= 1)
        {
            for (int i = count - 1; i > -1; i--)
            {
                GameObject unit = m_HitTarget[i].transform.gameObject;

            }
        }
        IsAttack = false;

    }



}
