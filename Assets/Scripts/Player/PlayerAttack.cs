using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer rend;
    Rigidbody2D rigid;
    BoxCollider2D boxcol;
    public Transform shootTransform;
    private Animator anim;  

    public GameData gamedata;

//basic_
    //프리팹
    public GameObject blast_projectile_prefeb;
    public GameObject blast_effect_prefeb;
    public GameObject blast_symbol_prefeb;
    public GameObject discharge_projectile_prefeb;
    public GameObject discharge_effect_prefeb;
    public GameObject discharge_symbol_prefeb;
    public GameObject resonance_projectile_prefeb;
    public GameObject resonance_effect_prefeb;
    public GameObject tripleimpact_prjectile_prefeb;
    public GameObject tripleimpact_effect_prefeb;

    public GameObject comboassult_special_prefeb; //기본 활대
    
    public GameObject comboassult_default_projectile_prefeb; //속성 활대
    public GameObject comboassult_default_effect1_prefeb; //이펙트 1
    public GameObject comboassult_default_effect2_prefeb; //이펙트 2


    public GameObject comboassult_discharge_projectile_prefeb;
    public GameObject comboassult_discharge_effect1_prefeb;
    public GameObject comboassult_discharge_effect2_prefeb;

    
    public GameObject comboassult_blast_projectile_prefeb;
    public GameObject comboassult_blast_effect1_prefeb;
    public GameObject comboassult_blast_effect2_prefeb;
    
    public GameObject comboassult_transition_projectile_prefeb;
    public GameObject comboassult_transition_effect1_prefeb;
    public GameObject comboassult_transition_effect2_prefeb;

    public GameObject transition_projectile_prefeb;
    public GameObject transition_effect_prefeb;
    public GameObject transition_effect_prefeb2;
    public GameObject transition_symbol_prefeb;
    public GameObject transition_special_prefeb;

    public GameObject addtional_blast;
    public GameObject addtional_discharge;

    public GameObject astra_blast_projectile_prefeb;
    public GameObject astra_blast_effect_prefeb;

    public GameObject astra_discharge_projectile_prefeb;
    public GameObject astra_discharge_effect_prefeb;

    public GameObject astra_transition_projectile_prefeb;
    public GameObject astra_transition_effect_prefeb;

    public GameObject astra_default_projectile_prefeb;
    public GameObject astra_default_effect_prefeb;
    

    bool BlastDischargeKeyDown = false;



    PlayerInputActions actions;
    InputAction Blast;
    InputAction Discharge;
    InputAction Resonance;
    InputAction Tripleimpact;
    InputAction Comboassult;
    InputAction Transition;
    InputAction BlastDischarge;
    InputAction astra;
    //InputAction Raven;
    InputAction Potion;
    public float Potion_MaxCool = 5f;
    public float Potion_Cool = 0f;

    SymbolSkills blast_class;
    SymbolSkills discharge_class;
    public Skills resonance_class;
    public Triple_Impact tripleimpact_class;
    public ComboAssult comboassult_default_class;
    ComboAssult comboassult_discharge_class;
    ComboAssult comboassult_blast_class;
    ComboAssult comboassult_transition_class;
    public Transition transition;
    public Astra astra_default_class;
    public Astra astra_discharge_class;
    public Astra astra_blast_class;
    public Astra astra_transition_class;

    PlayerStat playerStat;

    public enum Symbol
    {
        normal,
        blast,
        discharge,
        transition
    }

    public  Symbol PlayerSymbol = Symbol.normal;
    PlayerGage gage;
    


    void Awake()
    {
        if(gamedata.blast_class != null)
        {
            Debug.Log("data is not null");
            blast_class = gamedata.blast_class;
            discharge_class = gamedata.discharge_class;
            resonance_class = gamedata.resonance_class;
            tripleimpact_class = gamedata.tripleimpact_class;
            comboassult_default_class = gamedata.comboAssult_default_class;
            comboassult_blast_class = gamedata.comboassult_blast_class;
            comboassult_discharge_class = gamedata.comboassult_discharge_class;
            comboassult_transition_class = gamedata.comboassult_transition_class;



            transition = gamedata.transition;
            astra_default_class = gamedata.astra_default_class;
            astra_blast_class = gamedata.astra_blast_class;
            astra_discharge_class = gamedata.astra_discharge_class;
            astra_transition_class = gamedata.astra_transition_class;



        }
        else
        {
            Debug.Log("data is null");
            blast_class = new SymbolSkills(0.48f, blast_projectile_prefeb, blast_effect_prefeb, blast_symbol_prefeb);
            discharge_class = new SymbolSkills(0.48f, discharge_projectile_prefeb, discharge_effect_prefeb, discharge_symbol_prefeb);
            resonance_class = new Skills(15f,resonance_projectile_prefeb ,resonance_effect_prefeb);
            tripleimpact_class = new Triple_Impact(10f, tripleimpact_prjectile_prefeb, tripleimpact_effect_prefeb);

            comboassult_default_class = new ComboAssult(20f, comboassult_default_projectile_prefeb, comboassult_default_effect1_prefeb, comboassult_default_effect2_prefeb, comboassult_special_prefeb);
            comboassult_discharge_class = new ComboAssult(20f, comboassult_discharge_projectile_prefeb, comboassult_discharge_effect1_prefeb, comboassult_discharge_effect2_prefeb, comboassult_special_prefeb);
            comboassult_blast_class = new ComboAssult(20f, comboassult_blast_projectile_prefeb, comboassult_blast_effect1_prefeb, comboassult_blast_effect2_prefeb, comboassult_special_prefeb);
            comboassult_transition_class = new ComboAssult(20f, comboassult_transition_projectile_prefeb, comboassult_transition_effect1_prefeb, comboassult_transition_effect2_prefeb, comboassult_special_prefeb);
            transition = new Transition(0.39f, transition_projectile_prefeb, transition_effect_prefeb, transition_effect_prefeb2, transition_symbol_prefeb, transition_special_prefeb);
            astra_default_class = new Astra(60f, astra_default_projectile_prefeb, astra_default_effect_prefeb);
            astra_discharge_class = new Astra(60f, astra_discharge_projectile_prefeb, astra_discharge_effect_prefeb);
            astra_blast_class = new Astra(60f, astra_blast_projectile_prefeb, astra_blast_effect_prefeb);
            astra_transition_class = new Astra(60f, astra_transition_projectile_prefeb, astra_transition_effect_prefeb);
        }
        
        
        
        actions = new PlayerInputActions();

        Blast = actions.Player.Blast;
        Discharge = actions.Player.Discharge;
        Resonance = actions.Player.Resonance;
        Tripleimpact = actions.Player.TripleImpact;
        Comboassult = actions.Player.ComboAssult;
        Transition  = actions.Player.Transition;
        BlastDischarge = actions.Player.BlastDischarge;
        astra = actions.Player.Astra;
        //Raven = actions.Player.Raven;
        Potion = actions.Player.Potion;
 
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        playerStat = GetComponent<PlayerStat>();
        gage = FindObjectOfType<PlayerGage>();

        GameObject otherObject = GameObject.Find("Player");
        anim = otherObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Blast.Enable();
        Blast.performed += OnBlastPerformed;
        Blast.canceled += OnBlastCanceled;

        Discharge.Enable();
        Discharge.performed += OnDischargePerformed;
        Discharge.canceled += OnDischargeCanceled;

        Resonance.Enable();
        Resonance.performed += OnResonancePerformed;
        Resonance.canceled += OnResonanceCanceled;

        Tripleimpact.Enable();
        Tripleimpact.performed += OnTripleimpactPerformed;
        Tripleimpact.canceled += OnTripleimpactCanceled;

        Comboassult.Enable();
        Comboassult.performed += OnComboassultPerformed;
        Comboassult.canceled += OnComboassultCanceled;

        Transition.Enable();
        Transition.performed += OnTeleportPerformed;
        Transition.canceled += OnTeleportCanceled;

        BlastDischarge.Enable();
        BlastDischarge.performed += OnBlastDischargePerformed;
        BlastDischarge.canceled += OnBlastDischargeCanceled;

        astra.Enable();
        astra.performed += OnastraPerformed;
        astra.canceled += OnastraCanceled;

        // Raven.Enable();
        // Raven.performed += OnRavenPerformed;
        // Raven.canceled += OnRavenCanceled;
        
        Potion.Enable();
        Potion.performed += OnPotionPerformed;
    }

     private void OnDisable()
    {
        Blast.performed -= OnBlastPerformed;
        Blast.canceled -= OnBlastCanceled;
        Blast.Disable();

        Discharge.performed -= OnDischargePerformed;
        Discharge.canceled -= OnDischargeCanceled;
        Discharge.Disable();

        Resonance.performed -= OnResonancePerformed;
        Resonance.canceled -= OnResonanceCanceled;
        Resonance.Disable();

        Tripleimpact.performed -= OnTripleimpactPerformed;
        Tripleimpact.canceled -= OnTripleimpactCanceled;
        Tripleimpact.Disable();

        Comboassult.performed -= OnComboassultPerformed;
        Comboassult.canceled -= OnComboassultCanceled;
        Comboassult.Disable();

        Transition.performed -= OnTeleportPerformed;
        Transition.canceled -= OnTeleportCanceled;
        Transition.Disable();

        BlastDischarge.performed -= OnBlastDischargePerformed;
        BlastDischarge.canceled -= OnBlastDischargeCanceled;
        BlastDischarge.Disable();

        astra.performed -= OnastraPerformed;
        astra.canceled -= OnastraCanceled;
        astra.Disable();

        // Raven.performed -= OnRavenPerformed;
        // Raven.canceled -= OnRavenCanceled;
        // Raven.Disable();

        Potion.performed -= OnPotionPerformed;
        Potion.Disable();

        gamedata.blast_class = blast_class;
        gamedata.discharge_class = discharge_class;
        gamedata.resonance_class = resonance_class;
        gamedata.tripleimpact_class = tripleimpact_class;
        gamedata.comboAssult_default_class = comboassult_default_class;
        gamedata.comboassult_blast_class = comboassult_blast_class;
        gamedata.comboassult_discharge_class = comboassult_discharge_class;
        gamedata.comboassult_transition_class = comboassult_transition_class;
        gamedata.transition = transition;
        gamedata.astra_default_class = astra_default_class;
        gamedata.astra_blast_class = astra_blast_class;
        gamedata.astra_discharge_class = astra_discharge_class;
        gamedata.astra_transition_class = astra_transition_class;
    }

    private void OnBlastPerformed(InputAction.CallbackContext context)
    {
        
        blast_class.KeyDown = true;
        Debug.Log(blast_class.KeyDown);
    }

    private void OnDischargePerformed(InputAction.CallbackContext context)
    {
        discharge_class.KeyDown = true;
    }

    private void OnResonancePerformed(InputAction.CallbackContext context)
    {
        resonance_class.KeyDown = true;
    }

    private void OnTripleimpactPerformed(InputAction.CallbackContext context)
    {
        tripleimpact_class.KeyDown = true;
    }

    private void OnComboassultPerformed(InputAction.CallbackContext context)
    {
        comboassult_default_class.KeyDown = true;
    }
    private void OnTeleportPerformed(InputAction.CallbackContext context)
    {
        Teleport();
    }

    private void OnBlastDischargePerformed(InputAction.CallbackContext context)
    {
        BlastDischargeKeyDown = true;
        Debug.Log(BlastDischargeKeyDown);
    }

    private void OnastraPerformed(InputAction.CallbackContext context)
    {
        astra_default_class.KeyDown = true;
    }

    private void OnRavenPerformed(InputAction.CallbackContext context)
    {
        
    }


    private void OnPotionPerformed(InputAction.CallbackContext context)
    {
        Heal();
    }


    private void OnBlastCanceled(InputAction.CallbackContext context)
    {
        anim.SetBool("Attack", false);
        blast_class.KeyDown = false;
        //Debug.Log(blast_class.KeyDown);
    }

    private void OnDischargeCanceled(InputAction.CallbackContext context)
    {
        anim.SetBool("Attack", false);
        discharge_class.KeyDown = false;
    }

    private void OnResonanceCanceled(InputAction.CallbackContext context)
    {
        anim.SetBool("Attack", false);
        resonance_class.KeyDown = false;
    }

    private void OnTripleimpactCanceled(InputAction.CallbackContext context)
    {

        anim.SetBool("Attack", false);
        tripleimpact_class.KeyDown = false;
    }
    private void OnComboassultCanceled(InputAction.CallbackContext context)
    {
        anim.SetBool("Attack", false);
        comboassult_default_class.KeyDown = false;
         //문양별 추가 BlastDischargeKeyDown
    }
    private void OnTeleportCanceled(InputAction.CallbackContext context)
    {
        anim.SetBool("Attack", false);
    }

    private void OnBlastDischargeCanceled(InputAction.CallbackContext context)
    {
        anim.SetBool("Attack", false);
        BlastDischargeKeyDown = false;
        //Debug.Log(BlastDischargeKeyDown);
    }
    private void OnastraCanceled(InputAction.CallbackContext context)
    {
        astra_default_class.KeyDown = false;
        astra_default_class.isOn = false;
        anim.SetBool("Attack_Astra", false);
        anim.SetBool("Attack_Astra_transition", false);
        anim.SetBool("Attack", false);

    }
    private void OnRavenCanceled(InputAction.CallbackContext context)
    {
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
        transition_stack();
        
        if(rigid.velocity.y != 0 && tripleimpact_class.isUsing == true)
        {
            //Debug.Log("TI velo != 0");
        }
        else
        {
            
        }
    
        
    }

    void Heal()
    {
        if(Potion_Cool >= Potion_MaxCool)
        {
            playerStat.hp = playerStat.Maxhp;
            playerStat.mp = playerStat.Maxmp;

            Potion_Cool = 0f;
        }
        
    }

    void BlastDischarge_Trigger()
    {
        //Debug.Log(Blast_Reload() + ", " + Discharge_Reload() + ", " + BlastDischargeKeyDown);
        if(Blast_Reload() && Discharge_Reload() && BlastDischargeKeyDown == true && anim.GetBool("Attack_Astra") == false)
        {
            anim.SetBool("Attack", true);
            anim.SetTrigger("Attack_BlastDischarge");
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            Additonal(Symbol.blast, isFacingRight);
            ChangeSymbol(Symbol.blast);
            Additonal(Symbol.discharge, isFacingRight);
            ChangeSymbol(Symbol.discharge);

            blast_class.Draw_Symbol(shootTransform.position, shootTransform);
            blast_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            blast_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);

            discharge_class.Draw_Symbol (shootTransform.position, shootTransform);
            discharge_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            discharge_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            discharge_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            discharge_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
            
        }
    }
    
    void Blast_Trigger()
    {
       
        if(Blast_Reload() && blast_class.KeyDown == true && anim.GetBool("Attack_Astra") == false)
        {
            //Debug.Log(Blast_Reload() + ", " + blast_class.KeyDown + ", " +  discharge_class.KeyDown + ", " +  BlastDischargeKeyDown);
            anim.SetBool("Attack", true);
            anim.SetTrigger("Attack_Blast");
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            Additonal(Symbol.blast, isFacingRight);
            ChangeSymbol(Symbol.blast);
            //Debug.Log(PlayerSymbol);

            blast_class.Draw_Symbol(shootTransform.position, shootTransform);
            blast_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            blast_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
        }
    }

    void Discharge_Trigger()
    {
        if(Discharge_Reload() && discharge_class.KeyDown == true && anim.GetBool("Attack_Astra") == false)
        {   
            anim.SetBool("Attack", true);
            anim.SetTrigger("Attack_Discharge");
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            Additonal(Symbol.discharge, isFacingRight);
            ChangeSymbol(Symbol.discharge);
            //Debug.Log(PlayerSymbol);

            discharge_class.Draw_Symbol(shootTransform.position, shootTransform);
            discharge_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            discharge_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
        }
    }

    

    void Resonance_Trigger()
    {
        //레조는 상태 상관 X
        if(Resonance_Reload() && resonance_class.KeyDown == true && anim.GetBool("Attack_Astra") == false && gage.Relicgage >= 100f)
        {
            gage.Relicgage -= 100f;
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;

            resonance_class.Draw_Skill(shootTransform.position, shootTransform, isFacingRight);
            
        }
    }

    void Triple_Impact_Trigger()
    {
        
        if(Tripleimpact_Reload() && tripleimpact_class.KeyDown == true && tripleimpact_class.isUsing != true && anim.GetBool("Attack_Astra") == false && gage.Relicgage >= 50f)
        {
            gage.Relicgage -= 50f;
            anim.SetBool("Attack", true);
            anim.SetTrigger("Attack_Triple");

            bool isFacingRight = GetComponent<PlayerMove>().Horizon;

            tripleimpact_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
            Tripleimpact_Action();
            
        }
    }

    void Combo_Assult_Trigger()
    {
        
        if(ComboAssult_Reload() && comboassult_default_class.KeyDown == true && anim.GetBool("Attack_Astra") == false && gage.Relicgage >= 100f)
        {
            gage.Relicgage -= 100f;
            anim.SetBool("Attack", true);
            anim.SetTrigger("Attack_Combo");
            switch(PlayerSymbol)
            {
                case Symbol.blast:
                    //Debug.Log(PlayerSymbol);
                    comboassult_blast_class.Draw_Special_Effect(shootTransform.position, shootTransform);
                    comboassult_blast_class.Draw_Skill(shootTransform.position, shootTransform);
                    comboassult_default_class.ShotDelay = 0f;
                    break;
                case Symbol.discharge:
                    //Debug.Log(PlayerSymbol);
                    comboassult_discharge_class.Draw_Special_Effect(shootTransform.position, shootTransform);
                    comboassult_discharge_class.Draw_Skill(shootTransform.position, shootTransform);
                    comboassult_default_class.ShotDelay = 0f;
                    break;
                case Symbol.transition:
                    //Debug.Log(PlayerSymbol);
                    comboassult_transition_class.Draw_Special_Effect(shootTransform.position, shootTransform);
                    comboassult_transition_class.Draw_Skill(shootTransform.position, shootTransform);
                    comboassult_default_class.ShotDelay = 0f;
                    break;
                default:
                    //Debug.Log(PlayerSymbol);
                    comboassult_default_class.Draw_Special_Effect(shootTransform.position, shootTransform);
                    comboassult_default_class.Draw_Skill(shootTransform.position, shootTransform);
                    break;
            }

            Invoke("Comboassult_Action", 0.58f);
        }
    }

    void Fire()
    {
        if(!anim.GetBool("Dead"))
        {
            BlastDischarge_Trigger();
            Blast_Trigger();
            Discharge_Trigger();
            Resonance_Trigger();
            Triple_Impact_Trigger();
            Combo_Assult_Trigger();
            

            testastra();
        }
        
    }

    private void Tripleimpact_Action()
    {
        if(rigid.velocity.y == 0)//지상에서 사용시
        {
            tripleimpact_class.isUsing = true;
            
            rigid.AddForce(Vector2.up * 14f, ForceMode2D.Impulse);
            Invoke("Tripleimpact_Wake_Up", 0.5f);
        }
            
        else    
        {
            tripleimpact_class.isUsing = true;
            Tripleimpact_Wait();
        }

    }
    void Tripleimpact_Wait()
    {
        //Debug.Log("TI wait");
        rigid.velocity = Vector3.zero; 
        
        StartCoroutine(ForceSleep());
        //Invoke("Wake_Up", 0.5f);
        //Invoke("Tripleimpact_Wake_Up", 0.65f);
    }
    private void Comboassult_Action()
    {
        //문양 구분
        switch(PlayerSymbol)
        {
            case Symbol.blast:
                comboassult_blast_class.Draw_Skill_Effect(shootTransform.position, shootTransform);
                comboassult_blast_class.Draw_Skill_Effect2(shootTransform.position, shootTransform);
                PlayerSymbol = Symbol.normal;
                break;
            case Symbol.discharge:
                comboassult_discharge_class.Draw_Skill_Effect(shootTransform.position, shootTransform);
                comboassult_discharge_class.Draw_Skill_Effect2(shootTransform.position, shootTransform);
                PlayerSymbol = Symbol.normal;
                break;
            case Symbol.transition:
                comboassult_transition_class.Draw_Skill_Effect(shootTransform.position, shootTransform);
                comboassult_transition_class.Draw_Skill_Effect2(shootTransform.position, shootTransform);
                PlayerSymbol = Symbol.normal;
                break;
            default:
                comboassult_default_class.Draw_Skill_Effect(shootTransform.position, shootTransform);
                comboassult_default_class.Draw_Skill_Effect2(shootTransform.position, shootTransform);
                PlayerSymbol = Symbol.normal;
                break;
        }
        
    }
    void Wake_Up()
    {
        bool isFacingRight = GetComponent<PlayerMove>().Horizon;
        //Debug.Log("TI wake");
        rigid.WakeUp();
        tripleimpact_class.Draw_Skill(shootTransform.position, shootTransform, isFacingRight);
        tripleimpact_class.isUsing = false;
    }
    IEnumerator ForceSleep()
    {
        rigid.Sleep();
        yield return new WaitForSeconds(0.5f);
        
        // 타이머 후 깨어날 수 있음
        rigid.WakeUp();
        bool isFacingRight = GetComponent<PlayerMove>().Horizon;
        //Debug.Log("TI wake");
        tripleimpact_class.Draw_Skill(shootTransform.position, shootTransform, isFacingRight);
        tripleimpact_class.isUsing = false;
    }
    void Tripleimpact_Wake_Up()
    {
        bool isFacingRight = GetComponent<PlayerMove>().Horizon;

        tripleimpact_class.Draw_Skill(shootTransform.position, shootTransform, isFacingRight);
        tripleimpact_class.isUsing = false;
    }

    void Teleport()
    {
        if(transition.stack > 0  && anim.GetBool("Attack_Astra_transition") == false)
        {
            transition.stack--;
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            Vector2 moveInput = GetComponent<PlayerMove>().moveInput;
            ChangeSymbol(Symbol.transition);
            anim.SetBool("Attack", true);
            anim.SetTrigger("Attack_Transition");

            transition.Draw_Symbol(shootTransform.position, shootTransform);
            //Debug.Log(moveInput.x + " " + isFacingRight);
            if(moveInput.x < 0 && isFacingRight == false)//left
            {
                transition.Draw_Special_Effect(transform.position, transform, true);
                transform.position = new Vector2(transform.position.x - 3, transform.position.y);
                transition.Draw_Skill_Effect(transform.position, transform, isFacingRight);
                transition.Draw_Skill(transform.position, transform, isFacingRight);
            }
            else if(moveInput.x > 0 && isFacingRight == true)//right
            {
                transition.Draw_Special_Effect(transform.position, transform, true);
                transform.position = new Vector2(transform.position.x + 3, transform.position.y);
                transition.Draw_Skill_Effect(transform.position, transform, isFacingRight);
                transition.Draw_Skill(transform.position, transform, isFacingRight);
            }
            else if(moveInput.y > 0)//up
            {
                transition.Draw_Special_Effect(transform.position, transform, true);
                transform.position = new Vector2(transform.position.x, transform.position.y + 3);
                transition.Draw_Skill_Effect2(transform.position, transform);
                transition.Draw_Skill(transform.position, transform, isFacingRight);
            }
            else
            {
                //transition.Draw_Special_Effect(transform.position, transform);
                //transform.position = new Vector2(transform.position.x, transform.position.y + 3);
                //transition.Draw_Skill_Effect2(transform.position, transform);
                transition.Draw_Skill(transform.position, transform, isFacingRight);
            }
            rigid.velocity = Vector3.zero; 
        }
        


        //스프라이트 변경 코드 넣기
    }

    private void Reload(){
        blast_class.ShotDelay = blast_class.ShotDelay + Time.deltaTime >= blast_class.MaxShotDelay ?  blast_class.MaxShotDelay : blast_class.ShotDelay + Time.deltaTime;
        discharge_class.ShotDelay = discharge_class.ShotDelay + Time.deltaTime >= discharge_class.MaxShotDelay ? discharge_class.MaxShotDelay : discharge_class.ShotDelay + Time.deltaTime;
        resonance_class.ShotDelay = resonance_class.ShotDelay + Time.deltaTime >= resonance_class.MaxShotDelay ? resonance_class.MaxShotDelay : resonance_class.ShotDelay + Time.deltaTime;
        tripleimpact_class.ShotDelay = tripleimpact_class.ShotDelay + Time.deltaTime >= tripleimpact_class.MaxShotDelay ? tripleimpact_class.MaxShotDelay : tripleimpact_class.ShotDelay + Time.deltaTime;
        comboassult_default_class.ShotDelay = comboassult_default_class.ShotDelay + Time.deltaTime >= comboassult_default_class.MaxShotDelay ? comboassult_default_class.MaxShotDelay : comboassult_default_class.ShotDelay + Time.deltaTime;
        transition.ShotDelay = transition.ShotDelay + Time.deltaTime >= transition.stack_charage ? transition.stack_charage : transition.ShotDelay + Time.deltaTime;

        astra_default_class.ShotDelay = astra_default_class.ShotDelay + Time.deltaTime >= astra_default_class.MaxShotDelay ?  astra_default_class.MaxShotDelay : astra_default_class.ShotDelay + Time.deltaTime;

        Potion_Cool = Potion_Cool + Time.deltaTime >= Potion_MaxCool ? Potion_MaxCool : Potion_Cool + Time.deltaTime;
       // Debug.Log("Potion" + Potion_Cool);
    }
    private void reduce()
    {
        resonance_class.ShotDelay += 1f;
        tripleimpact_class.ShotDelay += 1f;
        comboassult_default_class.ShotDelay += 1f;
        astra_default_class.ShotDelay += 1f;
        //Debug.Log("reducing");
    }
    private void transition_stack()
    {
        if(transition.ShotDelay >= transition.stack_charage && transition.stack < 5)
        {
            transition.ShotDelay = 0f;
            transition.stack++;
            //Debug.Log(transition.stack);
        }
    }

    private bool Blast_Reload(){
        //Debug.Log(blast.MaxShotDelay + " , " + blast.ShotDelay);
        return blast_class.ShotDelay < blast_class.MaxShotDelay ? false : true;//장전시간이 충족이안되면
    }
    
    private bool Discharge_Reload(){
         return discharge_class.ShotDelay < discharge_class.MaxShotDelay ? false : true;//장전시간이 충족이안되면
    }

    private bool Resonance_Reload(){
         return resonance_class.ShotDelay < resonance_class.MaxShotDelay ? false : true;//장전시간이 충족이안되면
    }

    private bool Tripleimpact_Reload(){
         return tripleimpact_class.ShotDelay < tripleimpact_class.MaxShotDelay ? false : true;//장전시간이 충족이안되면
    }

    private bool ComboAssult_Reload(){
         return comboassult_default_class.ShotDelay < comboassult_default_class.MaxShotDelay ? false : true;//장전시간이 충족이안되면
    }

    private bool astra_Reload(){
         return astra_default_class.ShotDelay < astra_default_class.MaxShotDelay ? false : true;//장전시간이 충족이안되면
    }


    public InputAction GetSkillAction(string skillName)
    {
        // 스킬 이름에 따라 InputAction 반환
        switch (skillName)
        {
            case "Blast":
                return actions.Player.Blast;
            case "Discharge":
                return actions.Player.Discharge;
            case "Resonance":
                return actions.Player.Resonance;
            case "TripleImpact":
                return actions.Player.TripleImpact;
            case "ComboAssult":
                return actions.Player.ComboAssult;
            case "Transition":
                return actions.Player.Transition;
            case "BlastDischarge":
                return actions.Player.BlastDischarge;
            case "Astra":
                return actions.Player.Astra;
            case "Potion":
                return actions.Player.Potion;
                
            // 필요한 만큼 케이스 추가
            default:
                return null; // 유효하지 않은 경우 null 반환
        }
    }

    public void ChangeSymbol(Symbol symbol)
    {
        if(PlayerSymbol != symbol)
        {
            reduce();
            PlayerSymbol = symbol;
        }
        
    }
    
    public void Additonal(Symbol symbol, bool isFacingRight)
    {
        if(PlayerSymbol == Symbol.blast && symbol == Symbol.discharge) //에디셔널 디스차지 생성
        {
            ChangeSymbol(Symbol.discharge);
            int rand = Random.Range(0, 4);
            if(rand < 2)
            {
                //Debug.Log("에디셔널 디스차지");
                GameObject addtional1 = Instantiate(addtional_discharge, shootTransform.position, Quaternion.identity);
                GameObject addtional2 = Instantiate(addtional_discharge, shootTransform.position, Quaternion.identity);
                GameObject addtional3 = Instantiate(addtional_discharge, shootTransform.position, Quaternion.identity);
                if(isFacingRight == true)
                {
                    addtional1.transform.localScale = new Vector2(-addtional1.transform.localScale.x, addtional1.transform.localScale.y);
                    addtional2.transform.localScale = new Vector2(-addtional2.transform.localScale.x, addtional2.transform.localScale.y);
                    addtional3.transform.localScale = new Vector2(-addtional3.transform.localScale.x, addtional3.transform.localScale.y);
                }
                else
                {
                    addtional1.transform.localScale = new Vector2(addtional1.transform.localScale.x, addtional1.transform.localScale.y);
                    addtional2.transform.localScale = new Vector2(addtional2.transform.localScale.x, addtional2.transform.localScale.y);
                    addtional3.transform.localScale = new Vector2(addtional3.transform.localScale.x, addtional3.transform.localScale.y);
                }
            }

        }

        if(PlayerSymbol == Symbol.discharge && symbol == Symbol.blast)//에디셔널 블래스트 생성
            {
                ChangeSymbol(Symbol.blast);
                int rand = Random.Range(0, 4);
                if(rand < 2)
                {
                    //Debug.Log("에디셔널 블래스트");
                    GameObject addtional1 = Instantiate(addtional_blast, shootTransform.position, Quaternion.identity);
                    GameObject addtional2 = Instantiate(addtional_blast, shootTransform.position, Quaternion.identity);
                    GameObject addtional3 = Instantiate(addtional_blast, shootTransform.position, Quaternion.identity);
                    if(isFacingRight == true)
                    {
                        addtional1.transform.localScale = new Vector2(-addtional1.transform.localScale.x, addtional1.transform.localScale.y);
                        addtional2.transform.localScale = new Vector2(-addtional2.transform.localScale.x, addtional2.transform.localScale.y);
                        addtional3.transform.localScale = new Vector2(-addtional3.transform.localScale.x, addtional3.transform.localScale.y);
                    }
                    else
                    {
                        addtional1.transform.localScale = new Vector2(addtional1.transform.localScale.x, addtional1.transform.localScale.y);
                        addtional2.transform.localScale = new Vector2(addtional2.transform.localScale.x, addtional2.transform.localScale.y);
                        addtional3.transform.localScale = new Vector2(addtional3.transform.localScale.x, addtional3.transform.localScale.y);
                    }
                }
            }



    }
    
    public void testastra()
    {
        
        //Debug.Log(astra_default_class.KeyDown);
        if(PlayerSymbol == Symbol.blast && astra_default_class.KeyDown == true && astra_Reload() && astra_default_class.isOn != true)
        {
            astra_default_class.isOn = true;
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            astra_blast_class.Draw_Skill(shootTransform.position, null, isFacingRight);
            astra_blast_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
            astra_default_class.ShotDelay = 0f;
            
            anim.SetBool("Attack", true);
            anim.SetBool("Attack_Astra", true);
        }
        else if(PlayerSymbol == Symbol.discharge && astra_default_class.KeyDown == true && astra_Reload() && astra_default_class.isOn != true)
        {
            astra_default_class.isOn = true;
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            astra_discharge_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
            astra_default_class.ShotDelay = 0f;

            anim.SetBool("Attack", true);
            anim.SetBool("Attack_Astra", true);
        }
        else if(PlayerSymbol == Symbol.transition && astra_default_class.KeyDown == true && astra_Reload() && astra_default_class.isOn != true)
        {
            astra_default_class.isOn = true;
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            GetComponent<PlayerMove>().Horizon = isFacingRight;
            astra_transition_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);
            astra_default_class.ShotDelay = 0f;

            
            anim.SetBool("Attack_Astra", true);
            anim.SetBool("Attack_Astra_transition", true);
        }
        else if(PlayerSymbol == Symbol.normal && astra_default_class.KeyDown == true && astra_Reload() && astra_default_class.isOn != true)
        {
            astra_default_class.isOn = true;
            bool isFacingRight = GetComponent<PlayerMove>().Horizon;
            astra_default_class.Draw_Skill_Effect(shootTransform.position, shootTransform, isFacingRight);

            anim.SetBool("Attack", true);
            anim.SetBool("Attack_Astra", true);
        }
    }

    public void SaveBindingToScriptableObject() {
        string bindings = actions.SaveBindingOverridesAsJson();  // 바인딩을 JSON으로 저장
        gamedata.bindingJson = bindings;  // ScriptableObject에 저장
        //Debug.Log("Save" + gamedata.bindingJson + " , " + bindings);
    }

    public void LoadBindingFromScriptableObject() {
        if (!string.IsNullOrEmpty(gamedata.bindingJson)) {
            actions.LoadBindingOverridesFromJson(gamedata.bindingJson);  // 저장된 바인딩을 적용
            
            //Debug.Log("Load");
        }
    }

}
