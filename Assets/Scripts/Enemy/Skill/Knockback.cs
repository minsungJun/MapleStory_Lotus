using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyPhase2_3 enemy;
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
        enemy = GameObject.Find("Enemy").GetComponent<EnemyPhase2_3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //KnockBackPosition
    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        Rigidbody2D rigid = other.gameObject.GetComponent<Rigidbody2D>();

        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 닿은 오브젝트 이름이 Monster일때
        {
            if (enemy.KnockBackPosition == false)
            {
                rigid.AddForce(Vector2.left * 10f, ForceMode2D.Impulse);//여러개 맞을 수 있음 중복 막아야 함
            }
            else
            {
                rigid.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
            }
        }
    }
}
