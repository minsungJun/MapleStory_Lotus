using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hp_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    float Max_Boss_Hp;
    float Now_Boss_Hp;
    Vector3 initialScale;
    EnemyStat Enemy;

    void Start()
    {
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyStat>();
        Max_Boss_Hp = Enemy.hp;
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Now_Boss_Hp = Enemy.hp;

       

        changehp();
    }

    void changehp()
    {
        float scaleRatio = Now_Boss_Hp / Max_Boss_Hp;
        if(Now_Boss_Hp >= 0)
        {
            transform.localScale = new Vector3(initialScale.x * scaleRatio, initialScale.y);
        }
    }
}
