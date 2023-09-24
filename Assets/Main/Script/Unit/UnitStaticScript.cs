using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitBaseScrpit
{
    public static UnitBase GetUnitBase(this GameObject _unit)
    {
        UnitBase resultBase = new UnitBase();
        switch (_unit.tag)
        {
            case "Player":
                resultBase = _unit.GetComponent<Player>().m_UnitBase;
                break;
            case "Enemy":
                resultBase = _unit.GetComponent<Enemy>().m_UnitBase;
                break;
            case "Paladin":
                resultBase = _unit.GetComponent<Boss_Paladin>().m_UnitBase;
                break;
            default:
                Debug.LogError("플레이어와 에네미 둘 다 없음");
                break;
        }
        return resultBase;
    }
}