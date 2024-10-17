using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerStat playerstat;
    //PlayerMove player;
    void Awake()
    {
        playerstat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void revive()
    {
        playerstat.revive();

    }
}
