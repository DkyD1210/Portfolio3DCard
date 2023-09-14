using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Paladin : MonoBehaviour
{

    private enum BossState
    {

    }

    private Animator m_Animator;

    private NavMeshAgent m_Agent;

    private Player m_player;

    private Vector3 MoveDir;

    public UnitBase m_UnitBase;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_player = GameManager.StaticPlayer;
    }


    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        float _distance = Vector3.Distance(m_player.transform.position, transform.position);
        if (_distance >= 8)
        {
            transform.LookAt(m_player.transform);
            StartCoroutine(MoveSide(20));
        }
        Debug.Log(_distance);
    }

    private IEnumerator MoveSide(float _Time)
    {
        float rand = Random.Range(0, 2);
        MoveDir.x = rand == 0 ? -1 : 1;

        float timer = 0f;
        while (timer <= _Time)
        {
            transform.position = new Vector3(transform.position.x + rand * Time.deltaTime, transform.position.y, transform.position.z);
            yield return null;
        }

    }

    private IEnumerator JumpAttack()
    {

        yield return null;
    }
}
