using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_line2 : MonoBehaviour
{
    EnemyPhase2_3 otherScript;
    // Start is called before the first frame update
    public GameObject hit;
    
    void Start()
    {
        otherScript = GameObject.Find("Enemy").GetComponent<EnemyPhase2_3>();
        Invoke("Next", 3f);

        Destroy(this.gameObject, 3f);
        
        
        Debug.Log("elec_line2 test"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Next()
    {
        otherScript.elec_line_on = false;
        Debug.Log("elec_line_on = false;"); 
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("asdf");
            Instantiate(hit, other.gameObject.transform.position, Quaternion.identity);
        }
    }
}
