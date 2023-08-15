using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CardScript : CardModel
    {
        BattleManager battleManager = BattleManager.Instance;

        public virtual void UseCard(int cost)
        {
            battleManager.UseCost(cost);
        }

    }


public class CardModel
{
    private CardData Data;

    public CardData CardData
    {
        get
        {
            return Data;
        }
    }

    public int m_UseCost
    {
        get
        {
            return CardData.Cost;
        }
    }

}
