using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float start_x;
    [SerializeField] private float start_y;  
    
    [SerializeField] private GameObject Purple2;
    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        start_x = this.transform.position.x;
        start_y = this.transform.position.y;

        this.transform.position = new Vector2(0, 6);

        for (int i = 0; i < 10; i++)
        {
            Invoke("Acting", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Acting()
    {
        Instantiate(Purple2, new Vector3(0f,6f,0f), Quaternion.identity);
    }
}
