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
        get { return "�̸��� �ȳ־ ������ �ؽ�Ʈ"; }
    }
    public virtual string CardDesc
    {
        get { return "��ũ��Ʈ ������ �ȳ־ ������ �ؽ�Ʈ"; }
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
            return "�ֵθ���";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"���濡 ���� �ֵѷ� ���ظ� 8 �����ϴ�";
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
            Debug.Log("�÷��̾� ����!");

        }
    }

}


class CardScript_BaseRangeAttack : CardScript
{
    public override string CardName
    {
        get
        {
            return "ī�������";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"ī�带 ���� ���ظ� 5 �����ϴ�";
        }
    }

}

class CardScript_BaseDodgeRoll : CardScript
{
    public override string CardName
    {
        get
        {
            return "������";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"�̵� �������� �����ϴ�";
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
            Debug.Log("�÷��̾� ������!");
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
            return "����";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"5�ʵ��� �̵��ӵ��� 40% �����մϴ�";
        }
    }


}
