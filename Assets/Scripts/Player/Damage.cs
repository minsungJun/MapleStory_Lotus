using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerGage gage;
    PlayerStat stat;
    public GameObject damage_prefeb;
    EnemyStat enemyStat;
    public Sprite[] damage_icon;
    public Sprite empty_icon;
    public string type;
    public float damage = 0f;
    public float count = 0f;
    void Awake()
    {
        gage = FindObjectOfType<PlayerGage>();
        stat = GameObject.Find("Player").GetComponent<PlayerStat>();
        enemyStat = GameObject.Find("Enemy").GetComponent<EnemyStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void show(float damage, float count)
    {
        GameObject damageshow = Instantiate(damage_prefeb, new Vector2(enemyStat.damageposition.position.x, enemyStat.damageposition.position.y + (count * 0.1f)), Quaternion.identity);
        SpriteRenderer spriteRenderer = damageshow.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5 + (int)count;
        
        
        //Debug.Log(damage + " , " + 800f / 10000f);
        if (damage_prefeb != null)
        {
            float tenthousand = (int)damage / 10000;
            float thousand = (int)damage % 10000 / 1000;
            float hundred = (int)damage % 1000 / 100;
            float ten = (int)damage % 100 / 10;
            float one = (int)damage % 10;

            //Debug.Log("test " + tenthousand + " " + thousand + " " + hundred + " " + ten + " " + one);


            if(tenthousand == 0)
            {
                Transform childObject = damageshow.transform.Find("tenthousand");
                childObject.GetComponent<SpriteRenderer>().sprite = empty_icon;
                childObject.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
            }
            else
            {
                for(float i = 0f; i < 10f; i++)
                {
                    if(tenthousand == i)
                    {
                        Transform childObject = damageshow.transform.Find("tenthousand");
                        childObject.GetComponent<SpriteRenderer>().sprite = damage_icon[(int)i];
                        childObject.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
                        //크리티컬 이미지
                        Transform childObject0 = damageshow.transform.Find("effect");
                        childObject0.position = new Vector2(childObject.position.x - 0.2f , childObject.position.y + 0.25f);
                        childObject0.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
                    }
                }
            }

            if(tenthousand == 0 && thousand == 0)
            {
                Transform childObject = damageshow.transform.Find("thousand");
                childObject.GetComponent<SpriteRenderer>().sprite = empty_icon;
                
            }
            else
            {
                
                for(float i = 0f; i < 10f; i++)
                {
                    if(thousand == i)
                    {

                        Transform childObject = damageshow.transform.Find("thousand");
                        childObject.GetComponent<SpriteRenderer>().sprite = damage_icon[(int)i];
                        childObject.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
                        if(tenthousand == 0)
                        {
                            //크리티컬 이미지
                            Transform childObject0 = damageshow.transform.Find("effect");
                            childObject0.position = new Vector2(childObject.position.x - 0.2f , childObject.position.y + 0.25f);
                            childObject0.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
                        }

                    }
                }
            }


            if(tenthousand == 0 && thousand == 0 && hundred == 0)
            {
                Transform childObject = damageshow.transform.Find("hundred");
                childObject.GetComponent<SpriteRenderer>().sprite = empty_icon;
            }
            else
            {
                
                for(float i = 0f; i < 10f; i++)
                {
                    if(hundred == i)
                    {

                        Transform childObject = damageshow.transform.Find("hundred");
                        childObject.GetComponent<SpriteRenderer>().sprite = damage_icon[(int)i];
                        childObject.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
                        if(tenthousand == 0 && thousand == 0)
                        {
                            //크리티컬 이미지
                            Transform childObject0 = damageshow.transform.Find("effect");
                            childObject0.position = new Vector2(childObject.position.x - 0.2f , childObject.position.y + 0.25f);
                            childObject0.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
                        }

                    }
                }
            }

            if(tenthousand == 0 && thousand == 0 && hundred == 0 && ten == 0)
            {
                Transform childObject = damageshow.transform.Find("ten");
                childObject.GetComponent<SpriteRenderer>().sprite = empty_icon;
            }
            else
            {
                
                for(float i = 0f; i < 10f; i++)
                {
                    if(ten == i)
                    {

                        Transform childObject = damageshow.transform.Find("ten");
                        childObject.GetComponent<SpriteRenderer>().sprite = damage_icon[(int)i];
                        childObject.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
                        if(tenthousand == 0 && thousand == 0 && hundred == 0)
                        {
                            //크리티컬 이미지
                            Transform childObject0 = damageshow.transform.Find("effect");
                            childObject0.position = new Vector2(childObject.position.x - 0.2f , childObject.position.y + 0.25f);
                            childObject0.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
                        }

                    }
                }
            }

            if(tenthousand == 0 && thousand == 0 && hundred == 0 && ten == 0 && one == 0)
            {
                Transform childObject = damageshow.transform.Find("one");
                childObject.GetComponent<SpriteRenderer>().sprite = empty_icon;
            }
            else
            {
                
                for(float i = 0f; i < 10f; i++)
                {
                    if(one == i)
                    {

                        Transform childObject = damageshow.transform.Find("one");
                        childObject.GetComponent<SpriteRenderer>().sprite = damage_icon[(int)i];
                        childObject.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
                        if(tenthousand == 0 && thousand == 0 && hundred == 0 && ten == 0)
                        {
                            //크리티컬 이미지
                            Transform childObject0 = damageshow.transform.Find("effect");
                            childObject0.position = new Vector2(childObject.position.x - 0.2f , childObject.position.y + 0.25f);
                            childObject0.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
                        }

                    }
                }
            }
            int rand = 0;
            rand = Random.Range(0, 10); 
            Transform childObject1 = damageshow.transform.Find("else0");
            childObject1.GetComponent<SpriteRenderer>().sprite = damage_icon[rand];
            childObject1.GetComponent<SpriteRenderer>().sortingOrder = 5 + (int)count;
            rand = Random.Range(0, 10); 
            Transform childObject2 = damageshow.transform.Find("else1");
            childObject2.GetComponent<SpriteRenderer>().sprite = damage_icon[rand];
            childObject2.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
            rand = Random.Range(0, 10); 
            Transform childObject3 = damageshow.transform.Find("else2");
            childObject3.GetComponent<SpriteRenderer>().sprite = damage_icon[rand];
            childObject3.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;
            rand = Random.Range(0, 10); 
            Transform childObject4 = damageshow.transform.Find("else3");
            childObject4.GetComponent<SpriteRenderer>().sprite = damage_icon[rand];
            childObject4.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)count;


            // // 자식 오브젝트 찾기 (예: "1_1" 이름의 자식)
            // Transform childObject = damage_prefeb.transform.Find("tenthousand");
            // if (childObject != null)
            // {
            //     // 자식 오브젝트의 SpriteRenderer 컴포넌트 가져오기
            //     SpriteRenderer spriteRenderer = childObject.GetComponent<SpriteRenderer>();
                
            //     if (spriteRenderer != null)
            //     {
            //         // 스프라이트를 데미지 스프라이트로 변경
            //         spriteRenderer.sprite = damageSprite;
            //         Debug.Log("Sprite successfully changed!");
            //     }
            //     else
            //     {
            //         Debug.LogError("No SpriteRenderer found on the child object!");
            //     }
            // }
            // else
            // {
            //     Debug.LogError("Child object '1_1' not found!");
            //}
        }
        else
        {
            //Debug.LogError("Parent object '1' not found!");
        }
        Invoke("Next", 0.5f);
    }

    void Next()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {
            if(other != null)
            {
                //Debug.Log("not null" + other.name);
            }  
            if(other == null)
            {
                //Debug.Log("null" + other.name);
            }
            for(float i = 0f; i < count; i++)
            {
                Damagecheck(other.gameObject, i);
            }   
            
                    
            if(type == "blast")
            {
                gage.Relicgage += 20f;
                if(gage.Relicgage < gage.Maxgage)
                {
                    gage.rizegage += 20f;
                }
                
                //Debug.Log("gage :" + gage.Relicgage + " " + gage.rizegage);
                int rand = Random.Range(0, 4);
                if(rand == 0)
                {
                    enemyStat.Curse++;
                } 
            }
            else if(type == "transition")
            {
                gage.Relicgage += 20f;
                if(gage.Relicgage < gage.Maxgage)
                {
                    gage.rizegage += 20f;
                }
                enemyStat.Curse++;
                
                //Debug.Log("gage :" + gage.Relicgage + " " + gage.rizegage);
            }
            else if(type == "discharge")
            {
                gage.Relicgage += 10f;
                if(gage.Relicgage < gage.Maxgage)
                {
                    gage.rizegage += 10f;
                }
                //Debug.Log("gage :" + gage.Relicgage + " " + gage.rizegage);
                int rand = Random.Range(0, 4);
                if(rand == 0)
                {
                    enemyStat.Curse++;
                } 
            }
            else if(type == "TransitionAssult")
            {
                enemyStat.Curse = 5;
            }


            
        }
    }
    void Damagecheck(GameObject other, float count)
    {
        int rand = Random.Range(120, 151); 
        rand += enemyStat.Curse * 2;
        
        float total_damage = 0f;
        
        if(stat.isGuidance)
        {
            if(type == "Resonance")
            {
                total_damage = (damage * ((float)rand / (float)100)) * (1.08f + (enemyStat.Curse * 0.1f));
                show(total_damage, count);
                other.GetComponent<EnemyStat>().hp -= total_damage;
            }
            else
            {
                //
                total_damage = (damage * ((float)rand / (float)100)) * 1.08f;
                show(total_damage, count);
                other.GetComponent<EnemyStat>().hp -= total_damage;
            }
            //Debug.Log("rand : " + (rand - enemyStat.Curse * 2) + ", Curse :" + enemyStat.Curse + ", isGuidance :" + stat.isGuidance + ", damage :" + damage  +", total :" + (damage * (float)rand / (float)100) * (1.08f + (enemyStat.Curse * 0.1f)) + " ?? :" + (1.08f + (enemyStat.Curse * 0.1f)));
            //Debug.Log("rand : " + (rand - enemyStat.Curse * 2) + ", Curse :" + enemyStat.Curse + ", isGuidance :" + stat.isGuidance + ", damage :" + damage  +", total :" + (damage * (float)rand / (float)100) * 1.08f);
        }
        else
        {
            if(type == "Resonance")
            {
                total_damage = damage * ((float)rand / (float)100) * (enemyStat.Curse * 0.1f);
                show(total_damage, count);
                other.GetComponent<EnemyStat>().hp -= total_damage;
                //Debug.Log("rand : " + (rand - enemyStat.Curse * 2) + ", Curse :" + enemyStat.Curse + ", damage :" + damage  +", total :" + (damage * ((float)rand / (float)100))+ " ?? :" + enemyStat.Curse * 0.1f);
            }   
            else
            {
                total_damage = damage * ((float)rand / (float)100);
                show(total_damage, count);
                //Debug.Log("rand : " + (rand - enemyStat.Curse * 2) + ", Curse :" + enemyStat.Curse + ", damage :" + damage  +", total :" + (damage * ((float)rand / (float)100))+ " ?? :" + ((float)rand / (float)100));
                other.GetComponent<EnemyStat>().hp -= total_damage;
            }
            
            
        }

    }
}
