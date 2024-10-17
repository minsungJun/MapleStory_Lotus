using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    [SerializeField] private float delay_time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
