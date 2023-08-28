using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BattleManager battleManager = BattleManager.Instance;

    private CharacterController m_Controller;

    private Animator m_Animator;

    private Player m_player;

    public UnitBase m_UnitBase;

    private bool IsAttack = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Controller = GetComponent<CharacterController>();
        m_player = FindObjectOfType<Player>();
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
            m_Controller.Move(transform.rotation * Vector3.forward * m_UnitBase.Speed * Time.deltaTime);
        }
    }

    private void EnemyRayCast()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + m_Controller.center, transform.lossyScale * 1.5f, Vector3.forward, transform.rotation, 5f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("플레이어 발견");
                EnemyAttack();
            }
        }
    }

    private void EnemyAttack()
    {
        IsAttack = true;
        m_Animator.SetBool("Attack", IsAttack);

    }

    public void OnAttackEnd()
    {
        IsAttack = false;
    }


}
