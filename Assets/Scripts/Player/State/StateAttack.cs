using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IState
{
    private PlayerMove Player;

    public StateAttack(PlayerMove Player)
    {
        this.Player = Player;
    }
    
    
    public void OperateEnter()
    {
        //Debug.Log("StateAttack enter");
        AnimatorStateInfo stateInfo = Player.anim.GetCurrentAnimatorStateInfo(0);
        
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        AnimatorStateInfo stateInfo = Player.anim.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(stateInfo.IsName("Transition") + " " + stateInfo.normalizedTime);
        // 애니메이션이 끝에 도달했는지 확인 (normalizedTime >= 1.0f)
        if(Player.anim.GetBool("Dead"))
        {
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Dead]);
        }
        else if (Player.anim.GetBool("Attack") == false && stateInfo.normalizedTime >= 1.0f && (stateInfo.IsName("Blast")||stateInfo.IsName("Discharge")||stateInfo.IsName("TripleImpact")||stateInfo.IsName("ComboAssult")||stateInfo.IsName("Transition")||stateInfo.IsName("BlastDischarge")))
        {
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Idle]);
            //Debug.Log("애니메이션이 끝났습니다!");
        }
        else if(Player.anim.GetBool("Attack") == false && Player.anim.GetBool("Attack_Astra_transition") == true)
        {
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Idle]);
            Debug.Log("애니메이션이 끝났습니다!");
        }
        
    }
}
