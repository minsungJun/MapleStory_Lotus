using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMove playerMove;
    PlayerStat playerStat;

    public float stuntime = 0f;
    public float damage = 0f;
    public bool knockback_phase3 = false;
    void Awake()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 닿은 오브젝트 이름이 Monster일때
        {
            if(knockback_phase3)
            {
                damage = 40f;
            }
            if(playerStat.isResist)
            {
                playerStat.hp -= (playerStat.Maxhp * (damage / 100f)) * 0.4f;
            }
            else
            {
                playerStat.hp -= playerStat.Maxhp * (damage / 100f);
            }
            
            
            if(stuntime > 0f)
            {
                playerMove.stun(stuntime);
            }
            
        }
    }
}
