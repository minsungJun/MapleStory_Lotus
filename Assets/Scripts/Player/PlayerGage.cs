using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGage : MonoBehaviour
{
    PlayerStat stat;
    public GameData gamedata;
    public float Relicgage = 0f;
    public float rizegage = 0f;
    public float Maxgage = 1000f;
    public Image verticalImage; 
    public Image horizonImage; 
    
    void Awake()
    {
        if(gamedata.blast_class != null)
        {
            Relicgage = gamedata.Relicgage;
            rizegage = gamedata.rizegage;
        }

        stat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    void Update()
    {
        // 쿨타임이 진행 중이면 시간 감소
        if(Relicgage >= Maxgage)
        {
            Relicgage = Maxgage;
            verticalImage.fillAmount = 1f; 
        }
        if(rizegage >= Maxgage) 
        {
            guidance();
            rizegage = 0f;
            horizonImage.fillAmount = 0f; 
        }
        else
        {
            verticalImage.fillAmount = Relicgage / Maxgage;
            horizonImage.fillAmount = rizegage / Maxgage;
        }
       
    }
    void OnDisable()
    {
        gamedata.Relicgage = Relicgage;
        gamedata.rizegage = rizegage;
    }

    void guidance()
    {
        stat.guidanceOn();
    }

    // 쿨타임 감소 기능 호출

}
