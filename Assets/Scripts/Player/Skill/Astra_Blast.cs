using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astra_Blast : MonoBehaviour
{
    // Start is called before the first frame update+63
    public GameObject Blast_Projectile;
    Animator anim;
    public PlayerAttack playerAttack;
    Astra astra;
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
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
            Invoke("Next", 1.167f);
        }
    }

    void Shoot()
    {
        Instantiate(Blast_Projectile, transform.position, Quaternion.identity);
    }
    void Next()
    {
        Destroy(this.gameObject);
    }
}
