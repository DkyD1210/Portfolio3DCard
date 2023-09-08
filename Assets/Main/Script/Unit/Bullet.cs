using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;

    public int Speed;

    private BoxCollider m_Collider;

    void Start()
    {
        m_Collider = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject unit = other.transform.gameObject;
        UnitBase target = unit.GetUnitBase();
        if (target.UnitData.UnitFaction == Faction.Enemy)
        {
            target.LoseHp(Damage);
            Destroy(this.gameObject);
        }
    }
}
