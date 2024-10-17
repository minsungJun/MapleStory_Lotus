using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    PlayerMove otherScript;
    [SerializeField] private GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
        otherScript = GameObject.Find("Player").GetComponent<PlayerMove>();
        Instantiate(Effect, transform.position, Quaternion.identity);
        Invoke("Wake", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Wake()
    {
        otherScript.max_speed = 3f;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            
            otherScript.max_speed = 1f;
            //Debug.Log("enter : "+otherScript.max_speed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            otherScript.max_speed = 3f;
            //Debug.Log("exit : "+otherScript.max_speed);
        }
    }
}
