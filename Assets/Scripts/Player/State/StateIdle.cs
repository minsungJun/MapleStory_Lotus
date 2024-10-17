using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    private PlayerMove Player;

    public StateIdle(PlayerMove Player)
    {
        this.Player = Player;
    }


    public void OperateEnter()
    {
        Player.anim.SetBool("Move", false);
        //Player.anim.SetBool("Jump", false);
        Player.anim.SetBool("Attack", false);
        Player.anim.SetBool("Dead", false);

        //Player.Double_jump = true;
        //Debug.Log("StateIdle enter");
        //anim.SetBool("", false);
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        if(Player.anim.GetBool("Dead"))
        {
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Dead]);
        }
    }
}
