using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downcheck : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isOn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isOn = false;
        }
    }
}
