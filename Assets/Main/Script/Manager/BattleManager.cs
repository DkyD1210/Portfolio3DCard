using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame_CardSystem;
public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;


    [SerializeField]
    private List<GameObject> m_AllUnits = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_Enemy = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_Neutral = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_Player = new List<GameObject>();


    [SerializeField]
    private int m_MaxCardCost = 10;

    [SerializeField]
    private float m_CardCost;

    public int m_RecoverCost = 100;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        Cost();
    }

    private void Cost()
    {
        float recoverCost = (m_RecoverCost) * 0.01f;
        m_CardCost += recoverCost * Time.deltaTime;
        m_CardCost = Mathf.Clamp(m_CardCost, 0, m_MaxCardCost);
    }

    public int UseCost(int cost)
    {
        if((int)m_CardCost < cost)
        {
            Debug.Log("코스트 부족");
            return 0;
        }
        m_CardCost -= cost;
        return (int)m_CardCost;
    }

    public int RecoverCost(int cost)
    {
        m_CardCost += cost;
        if((int)cost >= 10)
        {
            cost = 10;
        }
        return 0;
    }

    public int GetMaxCost()
    {
        return (int)m_MaxCardCost;
    }

    public int GetCost()
    {
        return (int)m_CardCost;   
    }
}
