using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse_before : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gravity2;
    public GameObject gravity3;
    public int PhaseType;
    public Transform mainposition;

    public bool check = false;
    void Start()
    {
        Next();
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Next()
    {
        //mainposition.positon
        if(PhaseType == 2)
        {
            Instantiate(gravity2, mainposition.position, Quaternion.identity);
            Instantiate(gravity2, new Vector3(mainposition.position.x - 4.5f, mainposition.position.y, 0f), Quaternion.identity);
            Instantiate(gravity2, new Vector3(mainposition.position.x + 4.5f, mainposition.position.y, 0f), Quaternion.identity);
        }

        if(PhaseType == 3)
        {
            Instantiate(gravity3, mainposition.position, Quaternion.identity);
            Instantiate(gravity3, new Vector3(mainposition.position.x - 4.5f, mainposition.position.y, 0f), Quaternion.identity);
            Instantiate(gravity3, new Vector3(mainposition.position.x + 4.5f, mainposition.position.y, 0f), Quaternion.identity);
        }
        
    }
}
