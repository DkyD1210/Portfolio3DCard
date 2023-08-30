using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonTransform : MonoBehaviour
{
    private Transform PlayerTrs;

    [SerializeField]
    private float VectorTimer = 0.25f;

    private float timer;

    void Start()
    {
        PlayerTrs = GameManager.StaticPlayer.transform;
    }


    void Update()
    {
        TrsTimer();
    }

    private void OnBecameVisible()
    {
        Debug.Log("소환 위치 이동");
        transform.position = RandTrs();
    }
    private void TrsTimer()
    {
        timer += Time.deltaTime;
        if (timer >= VectorTimer)
        {
            timer = 0;
            transform.position = RandTrs();
        }

    }


    private Vector3 RandTrs()
    {
        while (true)
        {
            Vector3 randPos = Random.insideUnitSphere;
            Vector3 creatVector = PlayerTrs.position + randPos + (randPos * 30f);

            if (NavMesh.SamplePosition(creatVector, out NavMeshHit hit, 30f, NavMesh.AllAreas))
            {
                if (Vector3.Distance(PlayerTrs.position, creatVector) >= 30)
                {
                    return hit.position;
                }
            }

        }
    }
}
