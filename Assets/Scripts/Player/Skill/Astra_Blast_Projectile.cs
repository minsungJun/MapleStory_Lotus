using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astra_Blast_Projectile : MonoBehaviour
{
    public GameObject Blast_Hit;
    private float moveSpeed = 10;

    private bool direction = true;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Next", 0.5f);
        Destroy(this.gameObject, 0.5f);
        direction = GameObject.Find("Player").GetComponent<PlayerMove>().Horizon == true; //player 오브젝트의 playermove 스크립트에서 Horizon 변수를 참조
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == true)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        
    }

    void Next()
    {
        Instantiate(Blast_Hit, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {
            Destroy(this.gameObject);
            Next();
        }
    }
}
