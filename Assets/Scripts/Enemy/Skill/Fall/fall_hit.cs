using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_hit : MonoBehaviour
{

    public GameObject Platform_Hit;
    public GameObject Player_Hit;
    private int Phasetype;
    
    // Start is called before the first frame update
    void Start()
    {
        //Phase = GameObject.Find("Enemy");
        GameObject Phase = GameObject.Find("Enemy");
        if (Phase != null)
        {
            EnemyPhase1 targetScript = Phase.GetComponent<EnemyPhase1>();

            if (targetScript != null)
            {
                //Debug.Log("오브젝트가 EnemyPhase1를 가지고 있습니다.");
                Phasetype = targetScript.phaseType;
            }
            else
            {
                //Debug.Log("오브젝트가 EnemyPhase1를 가지고 있지 않습니다.");
                EnemyPhase2_3 targetScript2 = Phase.GetComponent<EnemyPhase2_3>();
                Phasetype = targetScript2.phaseType;
               // Debug.Log(Phasetype);

            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hit1() //바닥에 닿음
    {
        Instantiate(Platform_Hit, transform.position, Quaternion.identity);
    }

    void Hit2() //플레이어한테 닿음
    {
        Instantiate(Player_Hit, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {

            Destroy(this.gameObject);
            Hit2();
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall_y") && Phasetype == 1) 
        {

            Destroy(this.gameObject);
            Hit1();
        }
        
        else if (other.gameObject.layer == LayerMask.NameToLayer("PlatForm") && Phasetype != 1) // 닿은 오브젝트 이름이 Monster일때
        {

            Destroy(this.gameObject);
            Hit1();
        }
        
    }
}
