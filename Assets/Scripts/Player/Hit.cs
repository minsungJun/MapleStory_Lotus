using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public float destroytime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

}
