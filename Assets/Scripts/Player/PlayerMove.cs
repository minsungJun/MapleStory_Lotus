using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    public enum PlayerState
    {
        Idle,
        Move,
        Jump,
        Attack,
        Dead
    }

    public StateMachine stateMachine;
    public Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();
    public Dictionary<PlayerState, bool> keyinput = new Dictionary<PlayerState, bool>();


    
    [SerializeField] public float max_speed;
    [SerializeField] public float speed = 30f;
    [SerializeField] public float jump_power;
    [SerializeField] public bool canDoubleJump = true;
    [SerializeField] public bool isJumping = false;
    [SerializeField] public bool checkjump = false;
    [SerializeField] public bool levitation = false;
    [SerializeField] public bool On_ladder = false;
    [SerializeField] public bool Climb = false;
    [SerializeField] public bool Horizon = true;
    [SerializeField] public Transform PlatTransform;

    public Vector3 ladder_position;
    public GameObject platform;
    public Vector2 moveInput;

    [SerializeField] public bool ladder_key;
    PlayerInputActions actions;
    InputAction MoveAction;
    InputAction JumpAction;

    public Transform shootTransform;
    Transition DoubleJumpEffect;
    public GameObject DoubleJump_effect_prefeb;
    public GameObject DoubleJump_specialeffect_prefeb;

    public Rigidbody2D rigid;
    public BoxCollider2D boxcol;
    public Animator anim;

    public bool isStun = false;
    

    void Awake()
    {
        actions = new PlayerInputActions();

        MoveAction = actions.Player.Move;
        JumpAction = actions.Player.Jump;
        //LevAction  = actions.Player.Levitation;

        MoveAction.started += ctx => Ladder_Key_DownCheck();
        MoveAction.canceled += ctx => Ladder_Key_UpCheck();

        rigid = GetComponent<Rigidbody2D>();
        boxcol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        
        IState idle = new StateIdle(this);
        IState move = new StateMove(this);
        IState dead = new StateDead(this);
        IState attack = new StateAttack(this);

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Move, move);
        dicState.Add(PlayerState.Dead, dead);
        dicState.Add(PlayerState.Attack, attack);

        keyinput.Add(PlayerState.Idle, false);
        keyinput.Add(PlayerState.Move, false);
        keyinput.Add(PlayerState.Jump, false);
        keyinput.Add(PlayerState.Attack, false);

        stateMachine = new StateMachine(idle);

        DoubleJumpEffect = new Transition(0.2f, null, DoubleJump_effect_prefeb, null, null, DoubleJump_specialeffect_prefeb);
        //new Transition(0.2f, transition_projectile_prefeb, transition_effect_prefeb, transition_effect_prefeb2, transition_symbol_prefeb, transition_special_prefeb);
    }

    void Update()
    {
        isGround();
        KeyboardInput();
        Using_Ladder();
        stateMachine.DoOperateUpdate();
        
    }
    void FixedUpdate() 
    {
        
    }
 

    private void OnEnable()
    {
        MoveAction.Enable();
        MoveAction.performed += OnMovePerformed;
        MoveAction.canceled += OnMoveCanceled;

        JumpAction.Enable();
        JumpAction.performed += OnJumpPerformed;
        JumpAction.canceled += OnJumpCanceled;

       // LevAction.Enable();
       // LevAction.performed += OnLevitationPerformed;

    }
    private void OnDisable()
    {
        MoveAction.performed -= OnMovePerformed;
        MoveAction.canceled -= OnMoveCanceled;
        MoveAction.Disable();

        JumpAction.performed -= OnJumpPerformed;
        JumpAction.canceled -= OnJumpCanceled;
        JumpAction.Disable();

       // LevAction.performed -= OnLevitationPerformed;
       // LevAction.Disable();

    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        keyinput[PlayerState.Move] = true;
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        keyinput[PlayerState.Jump] = true;
    }
    private void OnLevitationPerformed(InputAction.CallbackContext context)
    {
        Levitation_Fn();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        keyinput[PlayerState.Move] = false;
    }
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        keyinput[PlayerState.Jump] = false;
        //Debug.Log("bool = "+dicStatebool[PlayerState.Jump]);
    }

    // Update is called once per frame
    

    void KeyboardInput()
    {
        if(!isStun && !anim.GetBool("Dead"))
        {
            if((keyinput[PlayerState.Jump] == true || keyinput[PlayerState.Move] == true) || anim.GetBool("Attack_Astra_transition") == true)// 이동
            {
                stateMachine.SetState(dicState[PlayerState.Move]);
            }
            if(anim.GetBool("Attack"))
            {
                stateMachine.SetState(dicState[PlayerState.Attack]);
            }
        }
        
       
        
    }


    public void Move()
    {
        if(moveInput.x > 0 && Climb != true)//Right
        {
            if(anim.GetBool("Attack_Astra_transition") == false)
            {
                Horizon = true;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            
           
            if(!anim.GetBool("Jump") /*&& Player.Double_jump == true */)// 지면
            {
                //rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
                
                rigid.AddForce(Vector2.right * +speed, ForceMode2D.Force);
                if(rigid.velocity.x > max_speed)
                    rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
            }
            else if(anim.GetBool("Jump") && moveInput.x > 0 && rigid.velocity.x > 0 && canDoubleJump)
            {
                rigid.AddForce(Vector2.right * +speed, ForceMode2D.Force);
                if(rigid.velocity.x > max_speed)
                    rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
            }
        }
        if(moveInput.x < 0 && Climb != true)//Left
        {
            if(anim.GetBool("Attack_Astra_transition") == false)
            {
                Horizon = false;
                transform.localScale = new Vector3(1f, 1f, 1f); 
            }

            if(!anim.GetBool("Jump") /*&& Player.Double_jump == true */)
            {
                //rigid.velocity = new Vector2(max_speed * (-1), rigid.velocity.y);
                rigid.AddForce(Vector2.right * -speed, ForceMode2D.Force);
                if(rigid.velocity.x < -max_speed)
                    rigid.velocity = new Vector2(-max_speed, rigid.velocity.y);
            }
            else if(anim.GetBool("Jump") && moveInput.x < 0 && rigid.velocity.x < 0 && canDoubleJump)
            {
                rigid.AddForce(Vector2.right * -speed, ForceMode2D.Force);
                if(rigid.velocity.x < -max_speed)
                    rigid.velocity = new Vector2(-max_speed, rigid.velocity.y);
            }
        }
    }

    public void Jump()
    {
        
        if(isJumping == false)
            {
                if(rigid.velocity.y < 0)
                    rigid.velocity = new Vector2(rigid.velocity.x , 0);

                //Debug.Log("J : " + rigid.velocity);
                rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
                isJumping = true;
            }
    }
    
    public void DoubleJump()
    {
        if(Climb != true)
        {
            //Debug.Log("DJ");
            if(rigid.transform.localScale.x > 0)
            {
                DoubleJumpEffect.Draw_Special_Effect(transform.position, transform, Horizon);
                DoubleJumpEffect.Draw_Skill_Effect(transform.position, transform, Horizon);
                rigid.AddForce(new Vector2(-13, 3), ForceMode2D.Impulse);
            }
            else if(rigid.transform.localScale.x < 0)
            {
                DoubleJumpEffect.Draw_Special_Effect(transform.position, transform, Horizon);
                DoubleJumpEffect.Draw_Skill_Effect(transform.position, transform, Horizon);
                rigid.AddForce(new Vector2(13, 3), ForceMode2D.Impulse);
            }
            canDoubleJump = false;
        }
    }

    void isGround()
    {
        if(rigid.velocity.y <= 0){
            //Debug.Log("isground");
            isJumping = true;
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            RaycastHit2D Hit = Physics2D.BoxCast(PlatTransform.position,new Vector2(0.1f,0.1f), 0 ,Vector2.down, 0.45f, LayerMask.GetMask("PlatForm"));
            if(Hit.collider != null){
                isJumping = false;
                checkjump = false;
                canDoubleJump = true;
                //rigid.velocity= new Vector2(rigid.velocity.x , 0f);
                if(keyinput[PlayerState.Jump] == false)
                    anim.SetBool("Jump", false);
            }
        }

    }

    void Using_Ladder()
    {
        //사다리 위 아래
        if(moveInput.y > 0 && On_ladder == true)
        {
            //Debug.Log("using");
            Climb = true;
            transform.position = new Vector2(ladder_position.x, transform.position.y);
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(0, max_speed * moveInput.y);

            RaycastHit2D hit = Physics2D.Raycast(PlatTransform.position, Vector2.down, 0.5f, LayerMask.GetMask("PlatForm")); 
            if (hit.collider != null)
            {
                // 다른 물체 위에 있다고 판단할 수 있음
                platform = hit.collider.gameObject;
                //platform.GetComponent<BoxCollider2D>().isTrigger = true;


                //Debug.Log("다른 물체 위에 있습니다.");
                //맨 밑바닥에 닿았을 때
                if(moveInput.y < 0)
                {
                    if(transform.position.y < 2.3)
                    {
                        Climb = false;
                        //platform.GetComponent<BoxCollider2D>().isTrigger = false;
                        rigid.gravityScale = 3f;
                    }
                }
            }
            else
            {
                // 다른 물체 위에 있지 않다고 판단할 수 있음
                //Debug.Log("다른 물체 위에 없습니다.");
            }
        }
        if(moveInput.y < 0)
        {
            
            RaycastHit2D hitladder = Physics2D.Raycast(PlatTransform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ladder")); 
            RaycastHit2D hitplatform = Physics2D.Raycast(PlatTransform.position, Vector2.down, 0.5f, LayerMask.GetMask("test")); 
            //상황 처음 내려가는 상황 , 내려가는 도중, 이제 빠져야하는 상황 9.26
            
            if(hitladder.collider != null && hitplatform.collider != null)
            {
                Debug.Log("case : 2");
                Climb = false;
                rigid.gravityScale = 3f;
            }
            else if (hitladder.collider != null)
            {
                Debug.Log("case : 1");
                Climb = true;
                transform.position = new Vector2(hitladder.collider.transform.position.x, transform.position.y);
                rigid.gravityScale = 0f;
                rigid.velocity = new Vector2(0, max_speed * moveInput.y);
            }


            else //if(hitladder.collider != null && hitplatform.collider != null && Climb == true)
            {
                Debug.Log("case : 3");
                Climb = false;
                rigid.gravityScale = 3f;
            }
        }
        if(ladder_key == true && On_ladder == true)
        {
            rigid.velocity = new Vector2(0, 0);
        }
    }

    //스프라이트 방향전환



    
    void Levitation_Fn()
    {
        levitation = true;
        rigid.velocity = Vector3.zero; 
        rigid.Sleep();
        Invoke("Wake_Up", 1f);
        
        levitation = !levitation;
        //스프라이트 변경 코드 넣기
    }


    void Wake_Up()
    {
        levitation = false;
        rigid.WakeUp();
    }

    void climb_ladder()
    {
        if(On_ladder == true)
        {
            Climb = true;
        }
    }

    void Ladder_Key_DownCheck()
    {
        ladder_key = false;
    }

    void Ladder_Key_UpCheck()
    {
        ladder_key = true;
        Invoke("Ladder_Key_DownCheck", 0.1f);

        
    }

    public void stun(float time)
    {
        isStun = true;
        Invoke("disStun", time);
    }

    void disStun()
    {
        isStun = false;
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        /*
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {
            Destroy(this.gameObject);
            Next();
        }
        */
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")) // 닿은 오브젝트 이름이 Monster일때
        {
            ladder_position = other.gameObject.transform.position;
            On_ladder = true;
            
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("test")) // 닿은 오브젝트 이름이 Monster일때
        {
            Climb = false;
            //Debug.Log("HIt test");
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")) // 닿은 오브젝트 이름이 Monster일때
        {
            On_ladder = false;
            Climb = false;
            platform.GetComponent<BoxCollider2D>().isTrigger = false;
            rigid.gravityScale = 3f;
        }
    }

    public InputAction GetSkillAction(string skillName)
    {
        // 스킬 이름에 따라 InputAction 반환
        switch (skillName)
        {
            case "Jump":
                return actions.Player.Jump;
            case "Move":
                return actions.Player.Move;

                
            // 필요한 만큼 케이스 추가
            default:
                return null; // 유효하지 않은 경우 null 반환
        }
    }
}
