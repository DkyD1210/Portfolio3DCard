using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private BattleManager battleManager = BattleManager.Instance;

    private NavMeshAgent m_NavMesh;

    private CharacterController m_Controller;

    private Animator m_Animator;

    private Player m_player;

    public UnitBase m_UnitBase;

    private bool IsAttack = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMesh = GetComponent<NavMeshAgent>();
        m_Controller = GetComponent<CharacterController>();
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
            //m_NavMesh.speed = m_UnitBase.Speed;
            //m_NavMesh.SetDestination(m_player.transform.position);
            m_Controller.Move(transform.rotation * Vector3.forward * m_UnitBase.Speed * Time.deltaTime);
        }
    }

    private void EnemyRayCast()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + m_Controller.center, transform.lossyScale * 1.2f, Vector3.forward, transform.rotation, 2f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Player")
            {
                if (IsAttack == true)
                {
                    return;
                }
                Debug.Log("플레이어 발견");
                StartCoroutine(EnemyAttack());
            }
        }
    }

    private IEnumerator EnemyAttack()
    {
        IsAttack = true;
        m_Animator.SetBool("Attack", IsAttack);
        yield return null;

    }

    public void OnAttackEnd()
    {
        IsAttack = false;
        m_Animator.SetBool("Attack", IsAttack);
    }


}
