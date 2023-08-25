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

    public string Artwork;

    public string Name;

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

    public virtual void OnUse(Player player, CardBase cardBase, CardScript cardScript)
    {
        Debug.Log("카드 사용 : " + GetType());
        cardScript = cardBase.Script;
        return;
    }

}


class Script_BaseMeleeAttack : CardScript
{
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
