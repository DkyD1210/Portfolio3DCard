using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript
{

    public virtual int UseCost()
    {
        return 0;
    }

    public virtual void GetUse()
    {
        return;
    }

}

[Serializable]
public class CardBase : CardScript
{
    //Xml È®ÀÎ¿ë ID
    private int _id;

    public int Id
    {
        get { return this._id; }
    }

    public Sprite Artwork;

    public int Cost;

    public string Name;

    public string Description;

    public int Damage;

    public Rarity Rarity;

    public CardType Type;
}
