using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManger : MonoBehaviour
{
    [Header("����Ʈ ������� �ҷ��ɴϴ�")]
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
