using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMove player;
    public GameData gamedata;
    UsingWorldPortal portal;
    public float hp = 55000;
    public float mp = 50000;

    public float Maxhp = 55000;
    public float Maxmp = 50000;

    public int deathcount = 5;
    public int time = 1800;
    public GameObject DeadPanel;
    public GameObject guidanceprefeb;

    public bool isGuidance = false;
    public bool isResist = false;
    

    Animator anim;
    void Awake()
    {
        if(gamedata.blast_class != null)
        {
            hp = gamedata.hp;
            mp = gamedata.mp;
            deathcount = gamedata.deathcount;
            time = gamedata.time;
            isGuidance = gamedata.isGuidance;
        }
        anim = GameObject.Find("Player").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        portal = GameObject.Find("WorldPortal").GetComponent<UsingWorldPortal>();
        //DeadPanel = GameObject.Find("Dead");
    }

    void OnDisable()
    {
        gamedata.hp = hp;
        gamedata.mp = mp;
        gamedata.deathcount = deathcount;
        gamedata.time = time;
        gamedata.isGuidance = isGuidance;
    }
    void Update()
    {
        isDie();
        isresist();
    }

    void isDie()
    {
        if(hp <= 0f && !anim.GetBool("Dead"))
        {
            anim.SetBool("Dead", true);
            deathcount--;
            DeadPanel.SetActive(true); 
        }
    }

    public void revive()
    {
        if(deathcount > 0)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            Invoke("Next2", 2f);
            hp = Maxhp;
            mp = Maxmp;
            anim.SetBool("Dead", false);
            player.transform.position = new Vector2(-3.5f , 3f);
            //DeadPanel.SetActive(false);     
        }
        else
        {
            hp = Maxhp;
            mp = Maxmp;
            anim.SetBool("Dead", false);
            deathcount = 5;
            portal.Dead();
        }
        
    }
    public void guidanceOn()
    {
        isGuidance = true;
        GameObject prefeb = Instantiate(guidanceprefeb, transform.position, Quaternion.identity);
        Invoke("Next", 1.4f);
    }
    void Next(GameObject prefeb)
    {
        Destroy(prefeb);
    }

    void Next2()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    void isresist()
    {
        if(anim.GetBool("Attack_Astra") == true)
        {
            isResist = true;
        }
        else
        {
            isResist = false;
        }
    }

    // Update is called once per frame

}
