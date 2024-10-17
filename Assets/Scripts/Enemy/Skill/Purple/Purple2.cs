using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple2 : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject Purple3;
    [SerializeField] private float moveSpeed;

    Vector3 dir;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        Acting();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x <= 2f)
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.015f, transform.localScale.y + 0.015f);
        }
    }

    void Acting()
    {
        
        int rand = Random.Range(0, 5);
        if (rand != 0)
        {
            rand = Random.Range(0, 11);
            rb.velocity = new Vector2(-7f + rand, -moveSpeed);
            RaycastHit2D Hit = Physics2D.Raycast(transform.position, new Vector2(-7f + rand , -moveSpeed), 15f, LayerMask.GetMask("PlatForm"));
            if (Hit.collider != null)
            {
                Instantiate(Purple3, new Vector2(Hit.point.x, Hit.point.y + 0.5542f), Quaternion.identity);
            }
        }
        else
        {
            Instantiate(Purple3, new Vector3(target.transform.position.x, 2.4f, 0), Quaternion.identity);
            rb.velocity = new Vector2(target.transform.position.x-1f, -4f);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlatForm"))
        {
            //Debug.Log("Destroy"+transform.position.x + " " + transform.position.y);
            //Instantiate(Purple3, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }
}
