using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//플레이어블 캐릭터
public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Move,
        Jump,
        DoubleJump,
        Attack,
        Dead
    }

    private StateMachine stateMachine;

    //스테이트들을 보관
    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

    // Start is called before the first frame update
    void Start()
    {/*
        //상태 생성
        IState idle = new StateIdle();
        IState move = new StateMove();
        IState jump = new StateJump();
        IState doublejump = new StateDoubleJump();
        IState attack = new StateAttack();
        IState dead = new StateDead();


        //키입력 등에 따라서 언제나 상태를 꺼내 쓸 수 있게 딕셔너리에 보관
        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Move, move);
        dicState.Add(PlayerState.Jump, jump);
        dicState.Add(PlayerState.DoubleJump, doublejump);
        dicState.Add(PlayerState.Attack, attack);
        dicState.Add(PlayerState.Dead, dead);

        //기본상태는 달리기로 설정.
        stateMachine = new StateMachine(idle);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //키입력 받기
        //KeyboardInput();

        //매프레임 실행해야하는 동작 호출.
        //stateMachine.DoOperateUpdate();
    }
/*
    //키보드 입력
    void KeyboardInput()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //달리기, 슬라이딩 중일 때만 점프 가능
            if (stateMachine.CurrentState == dicState[PlayerState.Run] || stateMachine.CurrentState == dicState[PlayerState.Sliding])
            {
                stateMachine.SetState(dicState[PlayerState.Jump]);
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            //달리기 중에만 슬라이딩 가능.
            if(stateMachine.CurrentState == dicState[PlayerState.Run])
            {
                stateMachine.SetState(dicState[PlayerState.Sliding]);
            }
        }
    }
    */
/*
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            stateMachine.SetState(dicState[PlayerState.Dead]);
        }
    }*/
}
