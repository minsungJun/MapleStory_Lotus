using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling : MonoBehaviour
{
    // Start is called before the first frame update
    public string prefabType;
    public bool locking = false;

    public GameObject Platform_Hit;
    public GameObject Player_Hit;
    Rigidbody2D rigid;
    Animator anim;
    EnemyPhase1 targetScript;
    EnemyPhase2_3 targetScript2;
    public string input;

    private int Phasetype;
    void Start()
    {
        GameObject enemyCoreObject = GameObject.Find("EnemyCore");
        
        if (enemyCoreObject != null)
        {

            //Debug.Log("오브젝트가 EnemyPhase1를 가지고 있습니다.");
            targetScript = enemyCoreObject.GetComponent<EnemyPhase1>();
            Phasetype = targetScript.phaseType;
        }
        else
        {
            //Debug.Log("오브젝트가 EnemyPhase1를 가지고 있지 않습니다.");
            targetScript2 = GameObject.Find("Enemy").GetComponent<EnemyPhase2_3>();
            Phasetype = targetScript2.phaseType;
            // Debug.Log(Phasetype);

        
        }


        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("Fall", 1.417f);
        // 프리팹의 타입에 따라 다르게 동작하는 코드

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fall()
    {

        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        this.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.003f);

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
