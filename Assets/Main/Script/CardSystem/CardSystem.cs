using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript
{
    public int ReduceCost = 0;

    public int UseCost()
    {
        return 0;
    }


    public virtual void OnUse(Player player)
    {
        return;
    }

}

[Serializable]
public class CardBase : CardScript
{
    public CardBase(CardBase data)
    {
        this._id = data._id;
        this.Artwork = data.Artwork;
        this.Cost = data.Cost;
        this.Name = data.Name;
        this.Scrpit = data.Scrpit;
        this.Damage = data.Damage;
        this.Barrier = data.Barrier;
        this.Rarity = data.Rarity;
        this.Type = data.Type;
    }

    //Xml È®ÀÎ¿ë ID
    private int _id;

    public int Id
    {
        get { return this._id; }
    }

    public Sprite Artwork;

    public int Cost;

    public string Name;

    public string Scrpit;

    public int Damage;

    public int Barrier;

    public Rarity Rarity;

    public CardType Type;


}
