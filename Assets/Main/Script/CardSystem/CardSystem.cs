using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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


[Serializable]
public class CardScript
{

    public string test = "B";

    public CardScript()
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
            return $"전방에 검을 휘둘러 피해를 8 입힙니다";
        }
    }


    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        Vector3 hitBox = new Vector3(3f, 3f, 1f);
        RaycastHit[] hit = Physics.BoxCastAll(player.transform.position + new Vector3(0, 1, 0), hitBox * 2, player.transform.rotation * Vector3.forward, Quaternion.identity, 2f, LayerMask.GetMask("Enemy"));
        int count = hit.Length;
        for (int i = count - 1; i >= 0; i--)
        {
            GameObject unit = hit[i].transform.gameObject;
            UnitBase target = unit.GetUnitBase();
            target.LoseHp(cardBase.Damage);
            Debug.Log("플레이어 공격!");

        }
    }

}


class CardScript_BaseRangeAttack : CardScript
{
    public override string CardName
    {
        get
        {
            return "카드던지기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"카드를 던져 피해를 5 입힙니다";
        }
    }

}

class CardScript_BaseDodgeRoll : CardScript
{
    public override string CardName
    {
        get
        {
            return "구르기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"이동 방향으로 구릅니다";
        }
    }

    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        Vector3 Rollposition = player.transform.position + (player.transform.rotation * player.MoveDir * 5f);

        if (NavMesh.SamplePosition(Rollposition, out NavMeshHit hit, 50f, NavMesh.AllAreas))
        {
            Debug.Log(Rollposition);
            Debug.Log(hit.position);
            Debug.Log("플레이어 구르기!");
            player.StartCoroutine(player.PlayerRollAnima(hit.position, 0.6f));
        }
    }


}
class CardScript_BaseSpeedBuf : CardScript
{
    public override string CardName
    {
        get
        {
            return "도주";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"5초동안 이동속도가 40% 증가합니다";
        }
    }


}
