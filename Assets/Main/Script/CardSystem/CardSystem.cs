using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript
{

    public virtual void ShowCard(Player player)
    {
        return;
    }

    public virtual void OnUse(Player player)
    {
        Debug.Log("카드 사용");
        return;
    }

}

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

class Script_BaseAttack : CardScript
{

    public override void ShowCard(Player player)
    {
        
    }



}
