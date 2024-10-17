using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMove Player;
    private PlatformEffector2D effector;
    public bool isOn;
    BoxCollider2D boxcol;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        Player = GameObject.Find("Player").GetComponent<PlayerMove>();
        boxcol = GetComponent<BoxCollider2D>();
        
        if (Player != null)
        {
            //Debug.Log("Player 객체가 정상적으로 참조되었습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Climb == true && isOn == true)
        {
            boxcol.isTrigger = true;
        }
        else
        {
            boxcol.isTrigger = false;
        }
        

    }

    private void OnCollisionEnter2D(Collision2D other)//무언가에 닿앗을때 호출
    {
        //Debug.Log(Player.Climb + " | " + Player.moveInput.y);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && Player.Climb == true && Player.moveInput.y < 0) // 닿은 오브젝트 이름이 Monster일때
        {
            Debug.Log("test");
            //effector.rotationalOffset = 180f;
            boxcol.isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 닿은 오브젝트 이름이 Monster일때
        {
            //effector.rotationalOffset = 0f;
            boxcol.isTrigger = false;
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //effector.rotationalOffset = 180f;
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //effector.rotationalOffset = 0f;
            isOn = false;
        }
    }
}
