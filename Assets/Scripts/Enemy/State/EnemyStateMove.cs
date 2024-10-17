using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMove : IState
{
    private EnemyPhase2_3 Enemy;
    public EnemyStateMove(EnemyPhase2_3 Enemy)
    {
        this.Enemy = Enemy;
    }
    
    public void OperateEnter()
    {
        //Debug.Log("StateEnemyMove enter");
        Enemy.anim.SetBool("Move", true);
        //애니메이션 변경
    }

    public void OperateExit()
    {
        Enemy.anim.SetBool("Move", false);
    }

    public void OperateUpdate()
    {
        Enemy.Move();
    }
}
