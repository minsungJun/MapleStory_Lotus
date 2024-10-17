using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttack : IState
{
    private EnemyPhase2_3 Enemy;
    float checktime;
    public EnemyStateAttack(EnemyPhase2_3 Enemy)
    {
        this.Enemy = Enemy;
    }
    
    public void OperateEnter()
    {
        //Debug.Log("EnemyStateAttack enter");
        checktime = 0f;
        Enemy.anim.SetBool("Attack", true);
        //AnimatorStateInfo stateInfo = Enemy.anim.GetCurrentAnimatorStateInfo(0);
        Enemy.Choose_pattern();
    }

    public void OperateExit()
    {
        Enemy.anim.SetBool("Attack", false);
        //Enemy.anim.Play("Move", -1, 0);
        //Enemy.anim.CrossFade("Stand", 0.1f);
    }

    public void OperateUpdate()
    {
        
        AnimatorStateInfo stateInfo = Enemy.anim.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(stateInfo.IsName("End_Tel") + " " + stateInfo.normalizedTime);
        
        checktime += Time.deltaTime;
        //Debug.Log(checktime);
        if(Enemy.rand == 3)
        {
            if(checktime >= 2f)
                Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
        }
        
        
        else if(stateInfo.normalizedTime >= 0.95f)
        {
            if(stateInfo.IsName("Smash"))
            {
                Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
            }
            else
            {
                switch(Enemy.rand)
                {
                    case 0:
                        if(stateInfo.IsName("End_Tel"))
                        {
                            Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
                        }
                        break;
                    case 1:
                        if(stateInfo.IsName("Ball"))
                            Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
                        break; 
                    case 2:
                        if(stateInfo.IsName("Reverse"))
                            Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
                        break;
                    case 4:
                        if(stateInfo.IsName("Slow"))
                            Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
                        break;
                    case 5:
                        if(stateInfo.IsName("Purple_end"))
                            Enemy.stateMachine.SetState(Enemy.dicState[EnemyPhase2_3.EnemyState.Move]);
                        break;
                    default:
                        //Debug.Log("error");
                        break;

                    
                }
            }
        }
        
    }
}
