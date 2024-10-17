using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astra_discharge : MonoBehaviour
{
    // Start is called before the first frame update
    public string type;
    public GameObject Discharge_Additional_Projectile;
    public GameObject Discharge_Projectile;
    Animator anim;
    public PlayerAttack playerAttack;
    Astra astra;
    PlayerGage gage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        gage = FindObjectOfType<PlayerGage>();
        astra = playerAttack.astra_default_class;
    }

    // Update is called once per frame
    void Update()
    {
        
        input();
    }

    void input()
    {
        if(astra.KeyDown == true)
        {
            
            anim.SetBool("Keydown", true);
            
        }

        else
        {
            
            anim.SetBool("Keydown", false);
            Invoke("Next", 0.5f);
            
        }
    }

    void Shoot()
    {
        if(gage.Relicgage >= 30f)
        {
            gage.Relicgage -= 30f;
            bool isFacingRight = GameObject.Find("Player").GetComponent<PlayerMove>().Horizon;
            GameObject Projectile = Instantiate(Discharge_Projectile, transform.position, Quaternion.identity);
            if (isFacingRight)
            {
                Projectile.transform.localScale = new Vector2(-Projectile.transform.localScale.x, Projectile.transform.localScale.y);
            }
            else
            {
                Projectile.transform.localScale = new Vector2(Projectile.transform.localScale.x, Projectile.transform.localScale.y);
            }


            
            if(type == "discharge")
            {
                addtional();
            }
        
        }
        else
        {
            afew();
        }
    }
    void Next()
    {
        
        Destroy(this.gameObject);
    }
    void afew()
    {
        anim.SetBool("Keydown", false);
        Destroy(this.gameObject);
    }

    void addtional()
    {
        int rand = Random.Range(0, 9);
        if(rand < 3)
        {
            bool isFacingRight =  GameObject.Find("Player").GetComponent<PlayerMove>().Horizon;
            GameObject addtional = Instantiate(Discharge_Additional_Projectile,transform.position, Quaternion.identity);
            if (isFacingRight)
            {
                addtional.transform.localScale = new Vector2(-addtional.transform.localScale.x, addtional.transform.localScale.y);
            }
            else
            {
                addtional.transform.localScale = new Vector2(addtional.transform.localScale.x, addtional.transform.localScale.y);
            }
            

        }
    }
}

