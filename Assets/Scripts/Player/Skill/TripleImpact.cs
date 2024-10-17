using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleImpact : MonoBehaviour
{
    public GameObject Triple_Hit;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Next()
    {
        Instantiate(Triple_Hit, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {
           
            Destroy(this.gameObject);
            Next();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall_y"))
        {
            Destroy(this.gameObject);
            Next();
        }
    }
}
