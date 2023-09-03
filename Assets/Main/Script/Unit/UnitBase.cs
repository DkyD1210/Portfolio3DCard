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

    public float hp { get; private set; }


    private float _speed;

    public float Speed
    {
        get { return this.UnitData.Speed; }
        set { _speed = value; }
    }


    public float SetHp(int newHp)
    {
        return this.hp = newHp;
    }

    public int LoseHp(int dmg)
    {
        int beforeHp = (int)this.hp;
        dmg = (int)((float)dmg * UnitData.GetDamageReduce);
        if(dmg <= 0)
        {
            dmg = 1;
        }
        this.hp -= dmg;
        int resultHp = (int)this.hp;
        IsHit = true;
        return resultHp;
    }

    public bool Ondie()
    {
        return hp <= 0;
    }

    public void Init()
    {
        this.hp = MaxHp;
    }

    public bool IsHit = false;
}

[Serializable]
public class UnitData
{
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
