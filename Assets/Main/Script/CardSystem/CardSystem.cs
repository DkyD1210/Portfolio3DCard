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

    public int Damage;

    public int Barrier;

    public Rarity Rarity;

    public CardRangeType Type;


}



public class CardScript
{
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


    public virtual void OnUse(Player player, CardBase cardBase, CardScript cardScript)
    {
        cardScript = cardBase.Script;
        return;
    }

}


class Script_BaseMeleeAttack : CardScript
{
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


    public override void OnUse(Player player, CardBase cardBase, CardScript cardScript)
    {
        base.OnUse(player, cardBase, cardScript);
        Script_BaseMeleeAttack sript = (Script_BaseMeleeAttack)cardScript;
    }

}


class Script_BaseRangeAttack : CardScript
{
    public override void OnUse(Player player, CardBase cardBase, CardScript cardScript)
    {
        base.OnUse(player, cardBase, cardScript);
        Script_BaseRangeAttack sript = (Script_BaseRangeAttack)cardScript;

    }
}

class Script_BaseDodgeRoll : CardScript
{
    public override void OnUse(Player player, CardBase cardBase, CardScript cardScript)
    {
        base.OnUse(player, cardBase, cardScript);
        Script_BaseDodgeRoll sript = (Script_BaseDodgeRoll)cardScript;
    }
}
class Script_BaseSpeedBuf : CardScript
{
    public override void OnUse(Player player, CardBase cardBase, CardScript cardScript)
    {
        base.OnUse(player, cardBase, cardScript);
        Script_BaseSpeedBuf sript = (Script_BaseSpeedBuf)cardScript;
    }
}
