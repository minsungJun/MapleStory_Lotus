using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void End()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //플레이어 히트판정
            Destroy(this.gameObject);
        }
    }
}
