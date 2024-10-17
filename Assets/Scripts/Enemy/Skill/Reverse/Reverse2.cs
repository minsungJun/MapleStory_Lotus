using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse2 : MonoBehaviour
{
    // Start is called before the first frame update
    Reverse_before target;
    void Start()
    {
        target = GameObject.FindWithTag("Parent").GetComponent<Reverse_before>();
        
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Player"));
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        Rigidbody2D rigid = other.gameObject.GetComponent<Rigidbody2D>();

        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 닿은 오브젝트 이름이 Monster일때
        {
            if (target.check == false)
            {
                rigid.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);//여러개 맞을 수 있음 중복 막아야 함
                target.check = true;
            }
        }
    }
}
