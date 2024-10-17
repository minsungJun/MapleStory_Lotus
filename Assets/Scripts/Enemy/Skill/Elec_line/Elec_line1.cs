using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_line1 : MonoBehaviour
{
    public GameObject elec_line_obj;
    Rigidbody2D rigid;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        EnemyPhase2_3 otherScript = GameObject.Find("Enemy").GetComponent<EnemyPhase2_3>();
        otherScript.elec_line_on = true;
        Invoke("Fall", 1.5f);
        Destroy(this.gameObject, otherScript.elec_line_time + 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fall()
    {
        //Debug.Log("Falling!");
        anim.SetBool("Fall", true);
        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        this.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
    }

   



    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("PlatForm"))
        {
            Debug.Log("Hit");
            anim.SetBool("Fall", false);
            GetComponent<BoxCollider2D>().isTrigger = false;
            rigid.constraints |= ~RigidbodyConstraints2D.FreezePositionY;
            this.gameObject.layer = LayerMask.NameToLayer("PlatForm");
        }
    }
}
