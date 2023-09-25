using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame_Xml;

[Serializable]
public class UnitBase
{

    public UnitBase()
    {

    }
    public UnitBase(UnitBase model)
    {
        this._id = model.Id;
        this._unitData = model.UnitData;
        this.BuffList = model.BuffList;
        this.hp = model.hp;
        this.Speed = model.Speed;
        this.Damage = model.Damage;
    }

    public UnitBase(int id)
    {
        this._id = id;
    }

    //Xml È®ÀÎ¿ë ID
    private int _id;

    public int Id
    {
        get { return this._id; }
    }

    [SerializeField]
    private UnitData _unitData;

    public UnitData UnitData
    {
        get { return this._unitData; }
        set { this._unitData = value; }
    }

    public int MaxHp
    {
        get
        {
            return UnitData.Hp;
        }
    }

    public int hp { get; private set; }


    private float _speed;

    public float Speed
    {
        get { return this._speed; }
        set { _speed = value; }
    }

    public int Damage;

    public bool IsHit = false;

    public void Init()
    {

        SetHp(MaxHp);
        this.Speed = _unitData.Speed;
        this.Damage = _unitData.Damage;
    }

    public float SetHp(int newHp)
    {
        return this.hp = newHp;
    }

    public int LoseHp(int dmg)
    {
        int beforeHp = (int)this.hp;
        dmg = (int)((float)dmg * UnitData.GetDamageReduce);
        if (dmg <= 0)
        {
            dmg = 1;
        }
        if (hp <= 0)
        {
            hp = 0;
        }
        else
        {
            this.hp -= dmg;
        }

        int resultHp = (int)this.hp;
        IsHit = true;
        return resultHp;
    }

    public int RecoverHP(int heal)
    {
        if ((hp + heal) >= MaxHp)
        {
            hp = MaxHp;
        }
        else
        {
            hp += heal;
        }

        return hp;
    }

    public bool Ondie()
    {
        return hp <= 0;
    }



    public List<CardBuffBase> BuffList = new List<CardBuffBase>();

    public void AddBuff(CardBuffBase _buff)
    {
        BuffList.Add(_buff);
    }

    public void RemoveBuff(CardBuffBase _buff)
    {
        BuffList.Remove(_buff);
    }

    public void ClearBuff()
    {
        BuffList.Clear();
    }

}

[Serializable]
public class UnitData
{
    public UnitData()
    {
    }

    public UnitData(UnitData data)
    {
        this.Hp = data.Hp;
        this.Speed = data.Speed;
        this.Damage = data.Damage;
        this.GetDamageReduce = data.GetDamageReduce;
        this.UnitFaction = data.UnitFaction;
        this.UnitType = data.UnitType;
    }

    public UnitData(UnitEffect data)
    {
        this.Hp = data.Hp;
        this.Speed = data.Speed;
        this.Damage = data.Damage;
        this.GetDamageReduce = data.GetDamageReduce;
        this.UnitFaction = data.UnitFaction;
        this.UnitType = data.UnitType;
    }

    public int Hp;

    public int Speed;

    public int Damage;

    public float GetDamageReduce;

    public Faction UnitFaction;

    public UnitType UnitType;
}
