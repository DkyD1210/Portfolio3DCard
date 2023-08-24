using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BattleManager battleManager = BattleManager.Instance;

    private Rigidbody m_Rigid;

    private Player m_player;

    public UnitBase m_UnitBase;

    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        m_player = FindObjectOfType<Player>();
    }

    void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        transform.LookAt(m_player.transform);
        if (m_UnitBase.UnitData.UnitType != UnitType.Boss)
        {
            m_Rigid.velocity = transform.rotation * Vector3.forward * m_UnitBase.Speed * Time.deltaTime;
        }
    }


}
