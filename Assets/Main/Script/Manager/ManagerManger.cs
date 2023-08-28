using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManger : MonoBehaviour
{
    [Header("리스트 순서대로 불러옵니다")]
    public List<GameObject> m_MangerList;

    private void Awake()
    {
        InitMangers();
    }

    void Start()
    {
            
    }


    void Update()
    {
        
    }

    private void InitMangers()
    {
        int count = m_MangerList.Count;
        for(int i = 0; i > count; i++)
        {
            GameObject manager = m_MangerList[i];
            manager.SetActive(true);
        }
    }
}
