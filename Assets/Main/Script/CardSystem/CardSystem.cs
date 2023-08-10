using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame_CardSystem
{

    public class CardScript
    {
        BattleManager battleManager = BattleManager.Instance;

        public virtual void UseCard(int cost)
        {
            battleManager.UseCost(cost);
        }

    }
}

public class CardModel
{
    private CardXmlInfo cardXmlInfo;

    public CardXmlInfo XmlData
    {
        get
        {
            return cardXmlInfo;
        }
    }

    public int m_UseCost
    {
        get
        {
            return XmlData.CardEffect.Cost;
        }
    }

}
