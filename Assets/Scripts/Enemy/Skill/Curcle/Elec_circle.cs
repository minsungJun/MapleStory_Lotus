using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_circle : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 currentScale;

    public float x = 2f;
    public float y = -2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 22f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(x , y);
        currentScale = transform.localScale;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall_x")) 
        {
            x *= -1f;
            //Debug.Log("Hit x"); // 디버그용
            if(currentScale.x <= 2.4f)
            {
                transform.localScale = new Vector2(currentScale.x + 0.2f, currentScale.y + 0.2f);
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall_y"))
        {
            y *= -1f;
            //Debug.Log("Hit y"); // 디버그용
            if(currentScale.x <= 2.4f)
            {
            transform.localScale = new Vector2(currentScale.x + 0.2f, currentScale.y + 0.2f);
            }
        }
    }
}
