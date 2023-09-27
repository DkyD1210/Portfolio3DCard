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

    public CardScript()
    {
    }

    protected Player m_Player;

    protected int _Damage;
    public virtual string CardName
    {
        get { return "�̸� ����"; }
    }
    public virtual string CardDesc
    {
        get { return "���� ����"; }
    }


    public virtual void OnUse(Player player, CardBase cardBase)
    {
        m_Player = player;
        _Damage = cardBase.Damage + player.m_UnitBase.Damage;
    }

}


public class CardScript_BaseMeleeAttack : CardScript
{

    public override string CardName
    {
        get
        {
            return "���";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"������ ���� �� ���ظ� 11 �����ϴ�";
        }
    }


    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        Vector3 hitBox = new Vector3(1f, 3f, 3f);
        RaycastHit[] hit = Physics.BoxCastAll(player.transform.position + new Vector3(0, 1, 0), hitBox, player.transform.rotation * Vector3.forward, Quaternion.identity, 0f, LayerMask.GetMask("Enemy"));
        int count = hit.Length;
        for (int i = count - 1; i >= 0; i--)
        {
            GameObject unit = hit[i].transform.gameObject;
            UnitBase target = unit.GetUnitBase();
            target.LoseHp(_Damage);
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
            return $"������ ī�带 ���� ���ظ� 6 �����ϴ�";
        }
    }
    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        Vector3 hitBox = new Vector3(5f, 5f, 1f);
        bool targetLock = Physics.BoxCast(player.transform.position + new Vector3(0, 1, 0), hitBox, player.transform.rotation * Vector3.forward, out RaycastHit hit, Quaternion.identity, 20f, LayerMask.GetMask("Enemy"));
        Bullet bullet = GameManager.Instance.CreatBullet(player.transform, 0).GetComponent<Bullet>();
        if (targetLock == true)
        {
            bullet.transform.LookAt(hit.transform);
            bullet.transform.Rotate(new Vector3(90f, 0, 0));
        }
        bullet.Damage = _Damage;
        bullet.Speed = 20;

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

    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        CardBuff_SpeedBuff _buff = new CardBuff_SpeedBuff(5f, 40f);
        m_Player.m_UnitBase.AddBuff(_buff);
        _buff.Init(m_Player);

    }

}
public class CardScript_DamageReduceBuff : CardScript
{

    public override string CardName
    {
        get
        {
            return "��Ƽ��";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"3�ʰ� 30% ���ظ� ���� �޽��ϴ�";
        }
    }


    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        CardBuff_DamageReduceBuff _buff = new CardBuff_DamageReduceBuff(3f, 30f);
        m_Player.m_UnitBase.AddBuff(_buff);
        _buff.Init(m_Player);

    }

}

public class CardScript_SlashAttack : CardScript
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
        Vector3 hitBox = new Vector3(3f, 3f, 2f);
        RaycastHit[] hit = Physics.BoxCastAll(m_Player.transform.position + new Vector3(0, 1, 0), hitBox, m_Player.transform.rotation * Vector3.forward, Quaternion.identity, 0f, LayerMask.GetMask("Enemy"));
        int count = hit.Length;
        for (int i = count - 1; i >= 0; i--)
        {
            GameObject unit = hit[i].transform.gameObject;
            UnitBase target = unit.GetUnitBase();
            target.LoseHp(_Damage);
            Debug.Log("�÷��̾� ����!");

        }
    }

}

public class CardScript_DrawOneCard : CardScript
{

    public override string CardName
    {
        get
        {
            return "ī��̱�";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"������ ������ ���ظ� 8 �ݴϴ�\nī�带 ���� �̽��ϴ�";

        }
    }


    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        CardManager.Instance.DrawCard(1);

        base.OnUse(player, cardBase);
        Vector3 hitBox = new Vector3(1f, 4f, 1.5f);
        RaycastHit[] hit = Physics.BoxCastAll(player.transform.position + new Vector3(0, 1, 0), hitBox, player.transform.rotation * Vector3.forward, Quaternion.identity, 0f, LayerMask.GetMask("Enemy"));
        int count = hit.Length;
        for (int i = count - 1; i >= 0; i--)
        {
            GameObject unit = hit[i].transform.gameObject;
            UnitBase target = unit.GetUnitBase();
            target.LoseHp(_Damage);
            Debug.Log("�÷��̾� ����!");

        }
    }

}

public class CardScript_SpredRangeAttack : CardScript
{
    public override string CardName
    {
        get
        {
            return "ī�� ��Ѹ���";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"���濡 ī����� ���� ���ظ� 6 �����ϴ�";
        }
    }

    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        int count = 6;
        for (int i = 0; i < count; i++)
        {
            Quaternion angle = Quaternion.Euler(0, 0, -40 + (15 * i));
            Bullet bullet = GameManager.Instance.CreatBullet(player.transform, 0, angle).GetComponent<Bullet>();

            bullet.Damage = _Damage;
            bullet.Speed = 20;
        }

    }

}

class CardScript_Heal : CardScript
{
    public override string CardName
    {
        get
        {
            return "ü�� ȸ��";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"ü���� 3 ȸ���մϴ�";
        }
    }
    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        player.m_UnitBase.RecoverHP(cardBase.Barrier);

    }

}


