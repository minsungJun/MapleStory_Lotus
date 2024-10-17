using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Hp_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    float Max_Hp;
    float Now_Hp;
    float Max_Mp;
    float Now_Mp;
    [SerializeField] string type;
    Vector3 initialScale;
    TMP_Text childTMPText;
    PlayerStat player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStat>();
        Max_Hp = player.Maxhp;
        Max_Mp = player.Maxmp;
        if(type == "HP")
        {
            childTMPText = GameObject.Find("Hp_text").GetComponent<TMP_Text>();
        }
        if(type == "MP")
        {
            childTMPText = GameObject.Find("Mp_text").GetComponent<TMP_Text>();
        }
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Now_Hp = player.hp;
        Now_Mp = player.mp;

        update_UI();
    }

    void update_UI()
    {
        if(type == "HP")
        {
            float scaleRatio = Now_Hp / Max_Hp;
            if(Now_Hp >= 0)
            {
                transform.localScale = new Vector3(initialScale.x * scaleRatio, initialScale.y);
                childTMPText.text = Now_Hp +  " / " + Max_Hp;
            }
            else if(Now_Hp < 0)
            {
                transform.localScale = new Vector3(0, initialScale.y);
                childTMPText.text = "0 / " + Max_Hp;
            }
        }
        if(type == "MP")
        {
            float scaleRatio = Now_Mp / Max_Mp;
            if(Now_Mp >= 0)
            {
                transform.localScale = new Vector3(initialScale.x * scaleRatio, initialScale.y);
                childTMPText.text = Now_Mp +  " / " + Max_Mp;
            }
            else if(Now_Mp < 0)
            {
                transform.localScale = new Vector3(0, initialScale.y);
                childTMPText.text = "0 / " + Max_Hp;
            }
        }
        
    }
}
