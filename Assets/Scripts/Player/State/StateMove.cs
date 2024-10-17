using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : IState 
{
    private PlayerMove Player;

    public StateMove(PlayerMove Player)
    {
        this.Player = Player;
    }
    public void OperateEnter()
    {
        
        //Player.anim.SetBool("Jump", false);
        //Debug.Log("StateMove enter");
    }
    
//스킬(공격) 이동 기본 죽음 엔터에선 애니메이션 변경 이런거만 확인하고, 업데이트에선 플레이어 안쪽에 함수를 실행한다. 상태에 따라서 유동적으로 엔터에서 함수실행하거 이런 식으로
    public void OperateExit()
    {
        //Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Idle]);
    }

    public void OperateUpdate()
    {
        if(Player.anim.GetBool("Dead"))
        {
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Dead]);
        }
        if(Player.keyinput[PlayerMove.PlayerState.Move] == true)
        {
            Player.anim.SetBool("Move", true);
            Player.Move();
        }

        if(Player.keyinput[PlayerMove.PlayerState.Jump] == true)
        {
            Player.anim.SetBool("Jump", true);
            Player.Jump();
        }

        if(Player.keyinput[PlayerMove.PlayerState.Jump] == false && Player.isJumping == true)
        {
            Player.checkjump = true;
        }

        if(Player.keyinput[PlayerMove.PlayerState.Jump] == true && Player.canDoubleJump == true && Player.checkjump == true && Player.anim.GetBool("Attack_Astra_transition") == false)
        {
            Player.DoubleJump();
        }

        //if(Player.keyinput[PlayerMove.PlayerState.Move] == false)
        if(Player.keyinput[PlayerMove.PlayerState.Move] == false && Player.anim.GetBool("Jump") == false)
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Idle]);
        
        

        //Debug.Log(Player.keyinput[PlayerMove.PlayerState.Jump]+" "+ Player.Double_jump+" "+ Player.checkjump );
    }
}

