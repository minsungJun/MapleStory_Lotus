using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase1 : MonoBehaviour
{
    public int phaseType = 1;
    public float speed = 2f;
    public GameObject Player;
    public GameObject falling_xs;
    public GameObject falling_s;
    public GameObject falling_m;    

    public Transform elec_line_1;
    public Transform elec_line_2;

    public int rand;

    private IEnumerator m_Coroutine;

    Rigidbody2D rigid;
    Vector3 dir;
    Animator anim;

    float rotate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(Rotate());
        Choose_fall();
        
    }

    

    // Update is called once per frame
    void Update()
    {
       
        Choose_pattern();
        //Debug.Log(rotate);
        if(anim.GetBool("Dead") == true)
        {
            if(m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
            }
            
        }
    }

    void FixedUpdate()
    {
        // Quaternion rotationDelta = Quaternion.Euler(0, 0, rotate);
        // transform.rotation *= rotationDelta;
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            yield return null;
            transform.Rotate(0, 0, rotate);
        }
    }
    
    void Choose_pattern()
    {
        if(transform.rotation.z == 1f || transform.rotation.z == -1f)
        {
            rand = Random.Range(0, 5); 
            if(rand == 3)
            {
                rotate *= -1f;
            }
            if(rand == 4)
            {
                if(rotate % 0.3f == 0.1f)// 돌아가는 속도가 1, -1 , 2, -2 이렇게 있을거 임
                {
                    rotate *= 2f;
                }
                else if(rotate % 0.3f == 0.2f)
                {
                    rotate /= 2f;
                    rotate *= 3f;
                }   
                else
                {
                    rotate /= 3f;
                }
            }
        }
        
        
    }

    // 똥
    void Choose_fall()
    {
        int result = Random.Range(0, 3);
        switch(result)
        {
            case 0:
                Falling_xs();
                break;
            case 1:
                Falling_s();
                break;
            case 2:
                Falling_m();
                break;
            default:
                Falling_xs();
                Debug.Log("error");
                break;
        }
        Invoke("Choose_fall", 0.3f);
    }
    void Falling_xs()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_xs, new Vector3(Player.transform.position.x,8f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 27);
            Instantiate(falling_xs, new Vector3(-6.7f + (result*0.5f),8f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-6.7 13.4
    }
    void Falling_s()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_s, new Vector3(Player.transform.position.x,8f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 25);
            Instantiate(falling_s, new Vector3(-6.4f + (result*0.5f),8f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-6.4 12.8
    }
    void Falling_m()
    {
        int result = 0;
        result = Random.Range(0, 6);
        if(result == 0)
        {
            Instantiate(falling_m, new Vector3(Player.transform.position.x,8f,0f), Quaternion.identity);
        }
        else
        {
            result = Random.Range(0, 25);
            Instantiate(falling_m, new Vector3(-6.3f + (result*0.5f),8f,0f), Quaternion.identity);
        }
        //x좌표 난수로 지정
        //x +-6.3 12.6
    }

}
