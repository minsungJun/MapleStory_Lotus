using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase2_3 : MonoBehaviour
{
    public int phaseType;

    public enum EnemyState
    {
        Move,
        Attack,
        Dead
    }

    public enum AttackType
    {
        Smash,
        Special,
        Random
    }
    public AttackType attackType;
    public StateMachine stateMachine;
    public Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

    [SerializeField] private float speed = 2f;
    public GameObject Player;
    [SerializeField] private GameObject Knockback_attack;
    [SerializeField] private GameObject elec_circle;
    [SerializeField] private GameObject gravity;
    [SerializeField] private GameObject elec_line_Obj;
    [SerializeField] private GameObject elec_line;
    [SerializeField] private GameObject slow;
    [SerializeField] private GameObject falling_xs;
    [SerializeField] private GameObject falling_s;
    [SerializeField] private GameObject falling_m;
    [SerializeField] private GameObject falling_l;
    [SerializeField] private GameObject falling_xl;
    [SerializeField] private GameObject Purple2;
    [SerializeField] private GameObject Purple_Start;
    [SerializeField] private GameObject Purple_Background;
    

    public Transform KnockBackPositionLeft;
    public Transform KnockBackPositionRight;
    public Transform shootTransform_center;

    public bool KnockBackPosition;

    public int rand;
    public bool elec_line_on = false;
    public float elec_line_time = 0f;
    public bool Move_On = true;
    public float check_time = 0f;
    public float Purlple_cooldown = 120f;
    public float Purlple_Max_cooldown = 120f;
    public float elec_line_cooldown = 60f;
    public float elec_line_Max_cooldown = 60f;

    

    Rigidbody2D rigid;
    Vector3 dir;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        IState move = new EnemyStateMove(this);
        IState dead = new EnemyStateDead(this);
        IState attack = new EnemyStateAttack(this);

        dicState.Add(EnemyState.Move, move);
        dicState.Add(EnemyState.Dead, dead);
        dicState.Add(EnemyState.Attack, attack);

        anim.SetBool("Move", true);
        stateMachine = new StateMachine(move);
        attackType = AttackType.Special;
        Choose_fall();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        stateMachine.DoOperateUpdate();
        check_time += Time.deltaTime;
        Purlple_cooldown += Time.deltaTime;
        elec_line_cooldown += Time.deltaTime;
    }
    
    // 이동
    public void Move()
    {
        dir = (Player.transform.position - transform.position).normalized;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Player"));
        if(colliders == null || colliders.Length == 0)
        {
            if(dir.x < 0) //left
            {
                transform.localScale = new Vector3(1f, 1f, 1f); 
                KnockBackPosition = false;
            }
            else //right
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                KnockBackPosition = true;
            }
            if(Move_On == true)
                rigid.velocity = new Vector2(dir.x * speed, 0);
        }
        else
        {
            //Debug.Log("move_else");
            rigid.velocity = new Vector2(0, 0);
            stateMachine.SetState(dicState[EnemyState.Attack]);
        }
    }
    public void Choose_pattern()
    {
        Debug.Log(attackType);

        dir = (Player.transform.position - transform.position).normalized;
        if(dir.x < 0) //left
        {
            transform.localScale = new Vector3(1f, 1f, 1f); 
            KnockBackPosition = false;
        }
        else //right
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            KnockBackPosition = true;
        }
        
        if(attackType == AttackType.Special)
        {
            Debug.Log("Special On");
            // 9.11 여기서 다른 패턴들 실행하기
            if(elec_line_on == true) //전기줄이면 밀격으로
                attackType = AttackType.Smash;
            else // 패턴 선택
            {
                Debug.Log("else");
                Choose_pattern_random();
                check_time = 0f;
                attackType = AttackType.Smash;
            }
        }
        else if(attackType == AttackType.Smash)
        {
            // 9.11 밀격 실행하기
            Debug.Log("Smash On");
            if(elec_line_on == true)// 전기줄 일 떄 밀격만
            {
                Debug.Log("Smash if");
                check_time = 0f;
                anim.SetTrigger("Smash");
                Invoke("Knockback", 0.7f);
                attackType = AttackType.Smash;
            }
            else if(check_time < 5f) // 실행 안된지 5초 이하면 밀격
            {
                Debug.Log("Smash elif");
                check_time = 0f;
                anim.SetTrigger("Smash");
                Invoke("Knockback", 0.7f);
                attackType = AttackType.Random;
            }
            else    // 넘으면 특수
            {
                Debug.Log("Smash else");
                check_time = 0f;
                Choose_pattern_random();
                attackType = AttackType.Smash;
            }
        }
        else if(attackType == AttackType.Random)
        {
            int sub_rand = Random.Range(0, 2);
            Debug.Log("Random On" + sub_rand);
            if(elec_line_on == true)
                sub_rand = 1;
            if(sub_rand == 0)
            {
                // 9.11 확률 반반으로 해서 정하기
                // 패턴 쿨타임 지정하기
                check_time = 0f;
                Choose_pattern_random();
                attackType = AttackType.Smash;
            }
            else if(sub_rand == 1)
            {
                // 9.11 확률 반반으로 해서 정하기
                // 패턴 쿨타임 지정하기
                if(check_time < 5f)
                {
                    anim.SetTrigger("Smash");
                    Invoke("Knockback", 0.7f);
                    attackType = AttackType.Special;
                }
                else
                {
                    attackType = AttackType.Special;
                }
            }
            
        }
    }

    void Choose_pattern_random()
    {
        if(phaseType == 2)
        {
            rand = Random.Range(0, 4);
            while(true)
            {
                if(rand == 3 && elec_line_cooldown < elec_line_Max_cooldown)
                {
                    Debug.Log(Purlple_cooldown + " " + elec_line_cooldown);
                    rand = Random.Range(0, 4);
                }
                else
                {
                    break;
                }
            }
            switch(rand)
            {
                case 0:
                    anim.SetTrigger("Teleport");
                    this.gameObject.layer = LayerMask.NameToLayer("Default");
                    Invoke("Teleport", 1.5f);
                    break;
                case 1:
                    anim.SetTrigger("Ball");
                    Elec_circle();
                    break;
                case 2:
                    anim.SetTrigger("Reverse");
                    Reverse();
                    break;
                case 3:
                    Elec_line();
                    break;
                default:
                    Debug.Log("error");
                    break;
            }

            
            
        }
        if(phaseType == 3)
        {
            rand = Random.Range(0, 6);
            while(true)
            {
                if((rand == 5 && Purlple_cooldown < Purlple_Max_cooldown) || (rand == 3 && elec_line_cooldown < elec_line_Max_cooldown))
                {
                    Debug.Log(Purlple_cooldown + " " + elec_line_cooldown);
                    rand = Random.Range(0, 6);
                }
                else
                {
                    break;
                }
            }
            switch(rand)
            {
                case 0:
                    anim.SetTrigger("Teleport");
                    this.gameObject.layer = LayerMask.NameToLayer("Default");
                    Invoke("Teleport", 1.5f);
                    break;
                case 1:
                    anim.SetTrigger("Ball");
                    Elec_circle();
                    break;
                case 2:
                    anim.SetTrigger("Reverse");
                    Reverse();
                    break;
                case 3:
                    Elec_line();
                    elec_line_cooldown = 0f;
                    break;
                case 4:
                    anim.SetTrigger("Slow");
                    Slow_Field();
                    break;
                case 5:
                    anim.SetTrigger("Purple");
                    Purlple_cooldown = 0f;
                    Purple_Circle();
                    Invoke("Delay", 3f);
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }
        
    }

    // 밀격
    void Knockback()
    {
        
        Instantiate(Knockback_attack, KnockBackPositionLeft.position, Quaternion.identity);
        //좌우 변경 추가
        //애니메이션 어느정도 출력 후 밀격 출력
    }

    // 구체
    void Elec_circle()
    {
        //에니메이션 어느정도 출력 후 구체 출력
        //2,3페 다르게
        Instantiate(elec_circle, shootTransform_center.position, Quaternion.identity);
    }
    // 전기줄
    void Elec_line()
    {
        //발판 낙하 -> 바닥에 발판 생성 -> 전기줄 생성
        //2,3페 다르게
        //Instantiate(elec_line_Obj, new Vector3(4.5f,8f,0f), Quaternion.identity);
        int result = 0;
        int result_check = 0;
        for (int i = 0; i < 6; i++)
        {
            result = Random.Range(0, 2); 
            if(result != 0)
            {
                Instantiate(elec_line_Obj, new Vector3(-6.0f + (i*2.3f),7f,0f), Quaternion.identity);
            }
            else if(i == 5 && result_check == 5 && result == 0)
            {
                Instantiate(elec_line_Obj, new Vector3(-6.0f + (i*2.3f),7f,0f), Quaternion.identity);
            }
            else
            {
                result_check++;
            }
        }
        result = Random.Range(4, 10); 
        elec_line_time = (float)result;
        Invoke("Elec_line_Next", elec_line_time);
    }

    void Elec_line_Next()
    {
        Instantiate(elec_line, new Vector3(0 , 2f, 0), Quaternion.identity);
    }
    // 반중력 
    void Reverse()
    {
        //x좌표 랜덤으로 흔들기
        float rand_float = Random.Range(-1f, 1f);
        Instantiate(gravity, new Vector3(rand_float,0f,0f), Quaternion.identity);
    }

    void Teleport()
    {
        if(phaseType == 2)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
            transform.position = new Vector2(Player.transform.position.x, 2.45f);
            
        }
        if(phaseType == 3)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
            transform.position = new Vector2(Player.transform.position.x, 2.8f);
        }
    }

    void Purple_Circle()
    {
        Instantiate(Purple_Start, transform.position, Quaternion.identity);
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        float start_x = this.transform.position.x;
        float start_y = this.transform.position.y;
        //Debug.Log(start_x + "" + start_y);
        Move_On = false;
        this.transform.position = new Vector2(0, 5.3f);

        StartCoroutine(ResetRoutine(start_x, start_y));

    }



    IEnumerator ResetRoutine(float start_x, float start_y)
    {
        yield return new WaitForSeconds(7.1f);
        Reset_Position(start_x, start_y);
    }
    void draw_purple_background()
    {
        Instantiate(Purple_Background, transform.position, Quaternion.identity);
    }
    void Purple_Circle_Acting()
    {
        Instantiate(Purple2, new Vector3(0f,5.3f,0f), Quaternion.identity);
    }

    void Reset_Position(float start_x, float start_y)
    {
        Move_On = true;
        check_time = 0f;
        transform.position = new Vector3(start_x, start_y, 0f);
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    void Slow_Field()
    {
        Instantiate(slow, new Vector2(shootTransform_center.position.x , shootTransform_center.position.y + 1.32f), Quaternion.identity);
    }

    void Delay()
    {
        //Debug.Log("Delayed...");
        // 이곳에 추가 작업을 수행
    }




    // 똥
    void Choose_fall()
    {
        //2,3페 다르게
        if(phaseType == 2)
        {
            int result = Random.Range(0, 4);
            //Debug.Log("fall : " + result);
            switch(result)
            {
                case 0:
                    Falling_xs();
                    break;
                case 1:
                    Falling_s();
                    break;
                case 2:
                    Falling_m();
                    break;
                case 3:
                    Falling_l();
                    break;
                case 4:
                    Falling_xl();
                    break;
                default:
                    Falling_xs();
                    Debug.Log("error");
                    break;
            }
            Invoke("Choose_fall", 0.3f);
        }
        if(phaseType == 3)
        {
            int result = Random.Range(0, 5);
            //Debug.Log("fall : " + result);
            switch(result)
            {
                case 0:
                    Falling_xs();
                    break;
                case 1:
                    Falling_s();
                    break;
                case 2:
                    Falling_m();
                    break;
                case 3:
                    Falling_l();
                    break;
                case 4:
                    Falling_xl();
                    break;
                default:
                    Falling_xs();
                    Debug.Log("error");
                    break;
            }
            Invoke("Choose_fall", 0.3f);
        }
    }
    void Falling_xs()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_xs, new Vector3(Player.transform.position.x,7f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 27);
            Instantiate(falling_xs, new Vector3(-6.7f + (result*0.5f),7f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-6.7 13.4
    }
    void Falling_s()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_s, new Vector3(Player.transform.position.x,7f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 25);
            Instantiate(falling_s, new Vector3(-6.4f + (result*0.5f),7f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-6.4 12.8
    }
    void Falling_m()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_m, new Vector3(Player.transform.position.x,7f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 25);
            Instantiate(falling_m, new Vector3(-6.3f + (result*0.5f),7f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-6.3 12.6
    }
    void Falling_l()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_l, new Vector3(Player.transform.position.x,7f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 23);
            Instantiate(falling_l, new Vector3(-5.8f + (result*0.5f),7f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-5.8 11.6
    }
    void Falling_xl()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_xl, new Vector3(Player.transform.position.x,7f,0f), Quaternion.identity);
        }
        else
        {   
            result = Random.Range(0, 23);
            Instantiate(falling_xl, new Vector3(-5.45f + (result*0.5f),7f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-5.8 11.6
    }
}
