using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardBase
{
    public CardBase(CardXmlInfo data)
    {
        this._id = data.Id.id;
        this._textID = data._textId;
        this.Artwork = data.artWork;
        this.Name = data.Name;
        this.Damage = data.CardEffect.Damage;
        this.Barrier = data.CardEffect.Barrier;
        this.Rarity = data.Rarity;
        this.Type = data.CardRange;
    }

    public CardBase(CardBase data)
    {
        this._id = data._id;
        this._textID = data._textID;
        this.Artwork = data.Artwork;
        this.Name = data.Name;
        this.Script = data.Script;
        this.Damage = data.Damage;
        this.Barrier = data.Barrier;
        this.Rarity = data.Rarity;
        this.Type = data.Type;
    }

    //Xml 확인용 ID
    private int _id;

    public int Id
    {
        get { return this._id; }
    }

    private int _textID;

    public int TextID
    {
        get { return this._textID; }
    }

    public string Name;

    public string Artwork;

    public CardScript Script;

    public Type _script;

    public int Damage;

    public int Barrier;

    public Rarity Rarity;

    public CardRangeType Type;


}


[Serializable]
public class CardScript
{

    public string test = "B";

    public CardScript()
    {
    }

    public CardScript(Type type)
    {
        
    }

    public virtual string CardName
    {
        get { return "이름를 안넣어서 나오는 텍스트"; }
    }
    public virtual string CardDesc
    {
        get { return "스크립트 설명을 안넣어서 나오는 텍스트"; }
    }


    public virtual void OnUse(Player player, CardBase cardBase)
    {
        return;
    }

}


public class CardScript_BaseMeleeAttack : CardScript
{

    private string _test
    {
        get { return "A"; }
        set { test = value; }
    }

    public override string CardName
    {
        get
        {
            return "휘두르기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return "테스트용 설명";
        }
    }


    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        Debug.Log("플레이어 공격!");
        Vector3 hitBox = new Vector3(3f, 3f, 1f);
        RaycastHit[] hit = Physics.BoxCastAll(player.transform.position + new Vector3(0, 1, 0), hitBox, player.transform.rotation * Vector3.forward, Quaternion.identity, 2f, LayerMask.GetMask("Enemy"));
        int count = hit.Length;
        for (int i = count - 1; i >= 0; i--)
        {
            GameObject unit = hit[i].transform.gameObject;
            UnitBase target = unit.GetUnitBase();
            target.LoseHp(cardBase.Damage);

        }
    }

}


class Script_BaseRangeAttack : CardScript
{



}

class Script_BaseDodgeRoll : CardScript
{

}
class Script_BaseSpeedBuf : CardScript
{


}
