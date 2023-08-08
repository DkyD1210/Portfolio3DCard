using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instace;

    [SerializeField]
    private GameObject Player;

    private void Awake()
    {
        if(Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SetPlayer();
    }

    
    void Update()
    {
        
    } 
    
    private void SetPlayer()
    {
        UnitBase _base = Player.GetComponent<UnitBase>();
    }
}
