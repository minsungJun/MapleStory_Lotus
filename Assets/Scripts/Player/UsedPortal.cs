using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedPortal : MonoBehaviour
{
    public GameObject In_Portal1;
    public GameObject In_Portal2;
    public GameObject Out_Portal1;
    public GameObject Out_Portal2;

    PlayerMove otherScript;

    public bool On_Portal1 = false;
    public bool On_Portal2 = false;
    // Start is called before the first frame update
    void Start()
    {
        otherScript = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Use_Portal();
    }
    void Use_Portal()
    {
        if(otherScript.moveInput.y > 0 )
        {
            if (On_Portal1 == true)
            {
                transform.position = new Vector2(Out_Portal1.transform.position.x, Out_Portal1.transform.position.y);
            }

            if (On_Portal2 == true)
            {
                transform.position = new Vector2(Out_Portal2.transform.position.x, Out_Portal2.transform.position.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal1")) // 닿은 오브젝트 이름이 Monster일때
        {
            On_Portal1 = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal2")) // 닿은 오브젝트 이름이 Monster일때
        {
            On_Portal2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal1")) // 닿은 오브젝트 이름이 Monster일때
        {
            On_Portal1 = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal2")) // 닿은 오브젝트 이름이 Monster일때
        {
            On_Portal2 = false;
        }
    }
}
