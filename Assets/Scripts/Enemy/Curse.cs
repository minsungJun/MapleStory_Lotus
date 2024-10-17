using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyStat stat;
    public Animator anim;
    void Awake()
    {
        stat = GameObject.Find("Enemy").GetComponent<EnemyStat>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkCurse();
    }
    
    void checkCurse()
    {
        switch(stat.Curse)
        {
            case 0:
                anim.SetBool("0", true);

                anim.SetBool("1", false);
                anim.SetBool("2", false);
                anim.SetBool("3", false);
                anim.SetBool("4", false);
                anim.SetBool("5", false);
                break;
            case 1:
                anim.SetBool("1", true);

                anim.SetBool("0", false);
                anim.SetBool("2", false);
                anim.SetBool("3", false);
                anim.SetBool("4", false);
                anim.SetBool("5", false);
                break;
            case 2:
                anim.SetBool("2", true);

                anim.SetBool("0", false);
                anim.SetBool("1", false);
                anim.SetBool("3", false);
                anim.SetBool("4", false);
                anim.SetBool("5", false);
                break;
            case 3:
                anim.SetBool("3", true);

                anim.SetBool("0", false);
                anim.SetBool("1", false);
                anim.SetBool("2", false);
                anim.SetBool("4", false);
                anim.SetBool("5", false);
                break;
            case 4:
                anim.SetBool("4", true);

                anim.SetBool("0", false);
                anim.SetBool("1", false);
                anim.SetBool("2", false);
                anim.SetBool("3", false);
                anim.SetBool("5", false);
                break;
            case 5:
                anim.SetBool("5", true);

                anim.SetBool("0", false);
                anim.SetBool("1", false);
                anim.SetBool("2", false);
                anim.SetBool("3", false);
                anim.SetBool("4", false);
                break;
        }
    }
}
