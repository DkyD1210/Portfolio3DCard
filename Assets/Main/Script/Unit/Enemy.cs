using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BattleManager battleManager = BattleManager.Instance;

    private CharacterController m_Controller;

    private Player m_player;

    public UnitBase m_UnitBase;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
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
            m_Controller.Move(transform.rotation * Vector3.forward *  m_UnitBase.Speed *Time.deltaTime);
        }
    }


}
