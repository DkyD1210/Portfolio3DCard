using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private SoundManager soundManager = SoundManager.Instance;

    private NavMeshAgent m_NavMesh;

    private Animator m_Animator;

    private AudioSource m_AudioSource;

    private Player m_player;

    public UnitBase m_UnitBase;

    private bool IsRunning = true;

    private bool UnitDie = false;

    private RaycastHit[] m_HitTarget;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMesh = GetComponent<NavMeshAgent>();
        m_AudioSource = GetComponent<AudioSource>();
        m_player = GameManager.StaticPlayer;


        m_UnitBase.Init();
        m_NavMesh.speed = m_UnitBase.Speed;

    }

    void Update()
    {
        if (UnitDie == true)
        {
            return;
        }
        EnemyHit();
        EnemyMove();
        EnemyRayCast();
    }
    private void EnemyHit()
    {
        if (m_UnitBase.IsHit == true)
        {
            StartCoroutine(EnemyHitAnima());
            m_UnitBase.IsHit = false;
        }
    }

    private void EnemyMove()
    {
        if (IsRunning == true)
        {
            transform.LookAt(m_player.transform);
            m_NavMesh.SetDestination(m_player.transform.position);
        }
    }

    private void EnemyRayCast()
    {
        m_HitTarget = Physics.BoxCastAll(transform.position, transform.lossyScale * 1.2f, Vector3.forward, transform.rotation, 0f, LayerMask.GetMask("Player"));
        if (IsRunning == false)
        {
            return;
        }
        foreach (RaycastHit hit in m_HitTarget)
        {

            Debug.Log("�÷��̾� �߰�");
            StartCoroutine(EnemyAttack(hit));
        }
    }


    private IEnumerator EnemyAttack(RaycastHit hit)
    {
        IsRunning = false;
        m_Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.45f);
        if (UnitDie == false)
        {
            int count = m_HitTarget.Length;
            if (count >= 1)
            {
                for (int i = count - 1; i > -1; i--)
                {
                    GameObject unit = m_HitTarget[i].transform.gameObject;
                    UnitBase target = unit.GetUnitBase();
                    target.LoseHp(m_UnitBase.UnitData.Damage);

                    Debug.Log("������");
                }
            }
            yield return new WaitForSeconds(2f);
            IsRunning = true;
            m_Animator.SetTrigger("Running");
        }
    }

    private IEnumerator EnemyHitAnima()
    {
        GameObject particle = GameManager.Instance.CreatParticle(transform, 1, transform.position + new Vector3(0f, 1f, 0f));
        soundManager.PlaySFX(5);

        if (m_UnitBase.Ondie() == true)
        {
            UnitDie = true;
            m_Animator.applyRootMotion = true;
            m_Animator.SetTrigger("Die");

            m_NavMesh.enabled = false;
            this.enabled = false;

            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
        else
        {
            IsRunning = false;
            m_Animator.SetTrigger("Hit");
            yield return new WaitForSeconds(1f);
            IsRunning = true;
            m_Animator.SetTrigger("Running");

        }
    }

}
