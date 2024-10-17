using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead : IState
{
    private PlayerMove Player;
    public float speed = 2.0f;  // 속도
    private float angle = 0;

    public StateDead(PlayerMove Player)
    {
        this.Player = Player;
    }
    
    public void OperateEnter()
    {
        //Debug.Log("StateDead enter");
        Player.anim.SetBool("Move", false);
        Player.anim.SetBool("Attack", false);
        Player.transform.position = new Vector2(Player.transform.position.x , 2.3f);
        Player.rigid.velocity = Vector2.zero;
        Player.rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        if(Player.anim.GetBool("Dead") == false)
        {
            Player.rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            Player.stateMachine.SetState(Player.dicState[PlayerMove.PlayerState.Idle]);
        }
        angle += speed * Time.deltaTime;
        Player.transform.position = Player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 0.0015f;

    }
}
