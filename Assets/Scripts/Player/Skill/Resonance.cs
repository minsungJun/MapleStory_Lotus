using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resonance : MonoBehaviour
{
    public GameObject Resonance_Hit;
    Vector3 othervector;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Next()
    {
        Instantiate(Resonance_Hit, othervector, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {
            othervector = other.transform.position;
            Next();
        }
    }
}
