using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    public GameObject gravity;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Next", 0.75f);
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Next()
    {
        Instantiate(gravity, new Vector3(transform.position.x , transform.position.y- 0.7f, 0), Quaternion.identity);
    }
}
