using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame_Xml;

public class UnitBaseModel : MonoBehaviour
{
    private Vector3 _positon = Vector3.zero;

    public UnitBase _unitBase;
}

[Serializable]
public class UnitBase
{

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
            float Num = (float)UnitData.Hp;
            return (int)Num;
        }
    }

    public float hp { get; private set; }

    public float SetHp(int newHp)
    {
        return this.hp = newHp;
    }

    public int LoseHp(int dmg)
    {
        int beforeHp = (int)this.hp;
        if(dmg <= 0)
        {
            dmg = 1;
        }
        this.hp -= dmg;
        int resultHp = (int)this.hp;
        return resultHp;
    }


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