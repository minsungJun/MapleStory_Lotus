using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateDead : IState
{
    private EnemyPhase2_3 Enemy;
    public EnemyStateDead(EnemyPhase2_3 Enemy)
    {
        this.Enemy = Enemy;
    }
    
    public void OperateEnter()
    {
        //Debug.Log("StateDead enter");
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        
    }
}
