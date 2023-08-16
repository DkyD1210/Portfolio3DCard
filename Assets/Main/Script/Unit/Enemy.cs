using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BattleManager battleManager = BattleManager.Instance;

    private Player m_player;

    [SerializeField]
    private UnitBase m_UnitBase;

    void Start()
    {
        m_player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_player.transform);
    }
}
