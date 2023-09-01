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
            Vector3 creatVector = PlayerTrs.position + (randPos * 30f);

            if (NavMesh.SamplePosition(creatVector, out NavMeshHit hit, 30f, NavMesh.AllAreas))
            {
                if (Vector3.Distance(PlayerTrs.position, hit.position) >= 20)
                {
                    return hit.position;
                }
            }

        }
    }
}
