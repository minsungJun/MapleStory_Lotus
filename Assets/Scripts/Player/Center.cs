using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    // Start is called before the first frame update
    Transform PlayerPosition;
    Transform EnemyPosition;
    public float Height;
    void Start()
    {
        PlayerPosition = GameObject.Find("Player").transform;
        EnemyPosition = GameObject.Find("Enemy").transform;
        if (EnemyPosition != null)
        {
            // 오브젝트가 있을 때 처리
            Debug.Log("Object found: " );
        }
        else
        {
            // 오브젝트가 없을 때 처리
            Debug.Log("Object not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyPosition != null)
        {
            transform.position = new Vector3((PlayerPosition.position.x + EnemyPosition.position.x)/2f, PlayerPosition.position.y + Height);
        }
        
    }


}
