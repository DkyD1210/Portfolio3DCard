using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBuffBase
{
    public float BuffTime;

    protected float _timer = 0f;

    protected UnitBase m_UnitBase;

    public virtual void Init(Player player)
    {
        m_UnitBase = player.m_UnitBase;
    }

    public virtual void ActivateBuff()
    {
        _timer += Time.deltaTime;
    }
}

public class CardBuff_SpeedBuff : CardBuffBase
{
    private float AddedSpeed;

    private float Coefficent;

    public CardBuff_SpeedBuff(float _time, float _coeff)
    {
        BuffTime = _time;
        Coefficent = _coeff * 0.01f;
    }

    public override void Init(Player player)
    {
        base.Init(player);
        Debug.Log(m_UnitBase.Speed);
        AddedSpeed += m_UnitBase.Speed * Coefficent;
        m_UnitBase.Speed += AddedSpeed;
        Debug.Log(m_UnitBase.Speed);
    }

    public override void ActivateBuff()
    {
        base.ActivateBuff();
        if (BuffTime <= _timer)
        {
            Debug.Log(m_UnitBase.Speed);
            m_UnitBase.Speed -= AddedSpeed;
            m_UnitBase.RemoveBuff(this);
            Debug.Log(m_UnitBase.Speed);
        }
    }
}
