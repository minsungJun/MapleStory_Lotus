using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discharge : MonoBehaviour
{
    public GameObject Discharge_Hit;
    GameObject target;
    //[SerializeField] GameObject explosion;
    [SerializeField] float speed = 20f, rotSpeed = 20f;

    Quaternion rotTarget;
    Vector3 dir;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        GuidedMissile();
    }

    void GuidedMissile()
    {
        dir = (target.transform.position - transform.position).normalized;
        //Debug.Log(dir); 
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
        Destroy(gameObject, 2.5f);
    } 

    void Next()
    {
        Instantiate(Discharge_Hit, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {

            Destroy(this.gameObject);
            Next();
        }
    }
/*
    void OnTriggerEnter2D(Collider2D collision)
    {
        MissileSpawner.SoundPlay();
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
*/
}

