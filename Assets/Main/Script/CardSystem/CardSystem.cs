using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardBase
{
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

    //Xml Ȯ�ο� ID
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

    public virtual void OnUse(Player player)
    {
        Debug.Log("ī�� ���");
        return;
    }

}


class Script_BaseAttack : CardScript
{
    public override void OnUse(Player player)
    {
        base.OnUse(player);
        Script_BaseAttack sript = new Script_BaseAttack();
    }

}


class Script_BaseDefence : CardScript
{
    public override void OnUse(Player player)
    {
        base.OnUse(player);
        Script_BaseDefence sript = new Script_BaseDefence();
    }

}
