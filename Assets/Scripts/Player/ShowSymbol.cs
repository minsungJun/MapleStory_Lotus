using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSymbol : MonoBehaviour
{
    // Start is called before the first frame update

    
    public PlayerAttack player;
    
    PlayerAttack.Symbol currentSymbol;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentSymbol = player.PlayerSymbol;
    }

    // Update is called once per frame
    void Update()
    {
        

        currentSymbol = player.PlayerSymbol;
        //Debug.Log("" + currentSymbol);
        Symbolcontrol();

        
    }

    void Symbolcontrol()
    {
        switch(currentSymbol)
        {
            
            case PlayerAttack.Symbol.normal:
            //Debug.Log("" + currentSymbol);
                anim.SetBool("N", true);

                anim.SetBool("B", false);
                anim.SetBool("Dis", false);
                anim.SetBool("T", false);
                // anim.ResetTrigger("B Trigger");
                // anim.ResetTrigger("D Trigger");
                // anim.ResetTrigger("T Trigger");
                break;

            case PlayerAttack.Symbol.blast:
            //Debug.Log("" + currentSymbol);
                anim.SetBool("B", true);

                anim.SetBool("N", false);
                anim.SetBool("Dis", false);
                anim.SetBool("T", false);
                break;

            case PlayerAttack.Symbol.discharge:
            //Debug.Log("" + currentSymbol);
                anim.SetBool("Dis", true);

                anim.SetBool("N", false);
                anim.SetBool("B", false);
                anim.SetBool("T", false);

                break;

            case PlayerAttack.Symbol.transition:
            
                //anim.SetTrigger("T Trigger");
                anim.SetBool("T", true); 

                anim.SetBool("B", false);
                anim.SetBool("Dis", false);
                anim.SetBool("N", false);
                // anim.ResetTrigger("N Trigger");
                // anim.ResetTrigger("B Trigger");
                // anim.ResetTrigger("D Trigger");
                break;

        }
    }






}
