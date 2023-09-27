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

    //Xml 확인용 ID
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
        get { return "이름 없음"; }
    }
    public virtual string CardDesc
    {
        get { return "설명 없음"; }
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
            return "찌르기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"전방의 적을 찔러 피해를 11 입힙니다";
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
            return $"적에게 카드를 던져 피해를 6 입힙니다";
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
            return "버티기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"3초간 30% 피해를 적게 받습니다";
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
        Vector3 hitBox = new Vector3(3f, 3f, 2f);
        RaycastHit[] hit = Physics.BoxCastAll(m_Player.transform.position + new Vector3(0, 1, 0), hitBox, m_Player.transform.rotation * Vector3.forward, Quaternion.identity, 0f, LayerMask.GetMask("Enemy"));
        int count = hit.Length;
        for (int i = count - 1; i >= 0; i--)
        {
            GameObject unit = hit[i].transform.gameObject;
            UnitBase target = unit.GetUnitBase();
            target.LoseHp(_Damage);
            Debug.Log("플레이어 공격!");

        }
    }

}

public class CardScript_DrawOneCard : CardScript
{

    public override string CardName
    {
        get
        {
            return "카드뽑기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"전방의 적에게 피해를 8 줍니다\n카드를 한장 뽑습니다";

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
            Debug.Log("플레이어 공격!");

        }
    }

}

public class CardScript_SpredRangeAttack : CardScript
{
    public override string CardName
    {
        get
        {
            return "카드 흩뿌리기";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"전방에 카드들을 던져 피해를 6 입힙니다";
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
            return "체력 회복";
        }
    }
    public override string CardDesc
    {
        get
        {
            return $"체력을 3 회복합니다";
        }
    }
    public override void OnUse(Player player, CardBase cardBase)
    {
        base.OnUse(player, cardBase);
        player.m_UnitBase.RecoverHP(cardBase.Barrier);

    }

}


