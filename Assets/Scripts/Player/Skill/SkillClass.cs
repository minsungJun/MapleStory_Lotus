using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Skills
{
    public float MaxShotDelay;
    public float ShotDelay = 0f;
    public GameObject ProjectilePrefab;
    public GameObject EffectPrefab;
    
    public bool KeyDown = false;

    

    public Skills(float max, GameObject projectileprefab, GameObject effectprefab)
    {
        MaxShotDelay = max;
        ProjectilePrefab = projectileprefab;
        EffectPrefab = effectprefab;
    }



    public virtual void Draw_Skill(Vector3 shootTransform, Transform parentTransform, bool move_direction)
    {
        GameObject instance = UnityEngine.Object.Instantiate(ProjectilePrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;
        if(move_direction == true)//right
        {
            instance.transform.localScale = new Vector3(-instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        else //left
        {
            instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }

        ShotDelay = 0f;
    }

    public virtual void Draw_Skill_Effect(Vector3 shootTransform, Transform parentTransform, bool move_direction)
    {
        GameObject instance = UnityEngine.Object.Instantiate(EffectPrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;
        if(move_direction == true)//right
        {
            instance.transform.localScale = new Vector3(-instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        else //left
        {
            instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        ShotDelay = 0f;
    }

    

    
}
public class SymbolSkills : Skills //블, 디에 쓸 클래스
{
    public GameObject SymbolPrefab;

    public SymbolSkills(float max, GameObject projectileprefab, GameObject effectprefab, GameObject symbolprefab) : base(max, projectileprefab, effectprefab) 
    { 
        SymbolPrefab = symbolprefab;
    }



    public virtual void Draw_Symbol(Vector3 shootTransform, Transform parentTransform)
    {
        GameObject instance = UnityEngine.Object.Instantiate(SymbolPrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;
        
        instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);

        ShotDelay = 0f;
    }
}
public class Triple_Impact : Skills
{
    public bool isUsing = false;
    public float speed = 10;
    public Triple_Impact(float max, GameObject projectileprefab, GameObject effectprefab ) : base(max, projectileprefab, effectprefab) { }

    public override void Draw_Skill(Vector3 shootTransform, Transform parentTransform, bool move_direction)
    {
        
        Vector2[] directions = new Vector2[]
        {
            new Vector2(Mathf.Cos(40f * Mathf.Deg2Rad), -Mathf.Sin(40f * Mathf.Deg2Rad)), 
            new Vector2(Mathf.Cos(55f * Mathf.Deg2Rad), -Mathf.Sin(55f * Mathf.Deg2Rad)), 
            new Vector2(Mathf.Cos(70f * Mathf.Deg2Rad), -Mathf.Sin(70f * Mathf.Deg2Rad)) 
        };

        if(move_direction == false)
        {
            directions = new Vector2[]
            {
                new Vector2(-Mathf.Cos(40f * Mathf.Deg2Rad), -Mathf.Sin(40f * Mathf.Deg2Rad)), 
                new Vector2(-Mathf.Cos(55f * Mathf.Deg2Rad), -Mathf.Sin(55f * Mathf.Deg2Rad)), 
                new Vector2(-Mathf.Cos(70f * Mathf.Deg2Rad), -Mathf.Sin(70f * Mathf.Deg2Rad)) 
            };
        }

        foreach (Vector2 direction in directions)
        {

            //방향 전환 추가할 것 (현재 오른쪽 방향 발사)
            GameObject skillInstance = UnityEngine.Object.Instantiate(ProjectilePrefab, shootTransform, Quaternion.Euler(180, 180, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

            // 진행 방향 설정
            Rigidbody2D rb = skillInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                    rb.velocity = direction * speed;
                    //rb.velocity = new Vector2(-direction.x * speed, direction.y * speed);
            }
        }

        ShotDelay = 0f;
    }
}
public class Transition : SymbolSkills //트랜지션
{
    public GameObject EffectPrefab2;
    public GameObject SpecialEffectPrefab;
    public int stack = 0;
    public float stack_charage = 7f;

    public Transition(float max, GameObject projectileprefab, GameObject effectprefab, GameObject effectprefab2, GameObject symbolprefab, GameObject specialeffectprefab) : base(max, projectileprefab, effectprefab, symbolprefab)
    {
        EffectPrefab2 = effectprefab2;
        SpecialEffectPrefab = specialeffectprefab;
    }

    public override void Draw_Skill_Effect(Vector3 shootTransform, Transform parentTransform, bool move_direction) //좌우
    {
        GameObject instance = UnityEngine.Object.Instantiate(EffectPrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;

        if(move_direction == true)//right
        {
            instance.transform.localScale = new Vector3(-instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        else //left
        {
            instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }

    }

    public virtual void Draw_Skill_Effect2(Vector3 shootTransform, Transform parentTransform) //위로
    {
        GameObject instance = UnityEngine.Object.Instantiate(EffectPrefab2, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;

        instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
    }

    public virtual void Draw_Special_Effect(Vector3 shootTransform, Transform parentTransform, bool move_direction) //방구
    {
        GameObject instance = UnityEngine.Object.Instantiate(SpecialEffectPrefab, shootTransform, Quaternion.identity);

        if(move_direction == true)//right
        {
            instance.transform.localScale = new Vector3(-instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        else //left
        {
            instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
    }

}
public class ComboAssult : Skills // 속성활대 draw skill, 공격 이펙트1 effect, 공격 이펙트2 effect2,  기본 활대 special
{
    public GameObject EffectPrefab2;
    public GameObject EffectPrefab3;
    public GameObject SpecialEffectPrefab;

    public ComboAssult(float max, GameObject projectileprefab, GameObject effectprefab, GameObject effectprefab2, GameObject specialeffectprefab) : base(max, projectileprefab, effectprefab)
    {
        EffectPrefab2 = effectprefab2;
        SpecialEffectPrefab = specialeffectprefab;
    }
    public virtual void Draw_Skill(Vector3 shootTransform, Transform parentTransform)
    {
        GameObject instance = UnityEngine.Object.Instantiate(ProjectilePrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;
        
        instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);

        ShotDelay = 0f;
    }
    public virtual void Draw_Skill_Effect(Vector3 shootTransform, Transform parentTransform)
    {
        GameObject instance = UnityEngine.Object.Instantiate(EffectPrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;

        instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
    }

    public virtual void Draw_Skill_Effect2(Vector3 shootTransform, Transform parentTransform) //공격 이펙트1
    {
        GameObject instance = UnityEngine.Object.Instantiate(EffectPrefab2, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;

        instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
    }

    public virtual void Draw_Special_Effect(Vector3 shootTransform, Transform parentTransform) //기본 활대
    {
        GameObject instance = UnityEngine.Object.Instantiate(SpecialEffectPrefab, shootTransform, Quaternion.identity);
        instance.transform.parent = parentTransform;

        instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
    }




    

}
public class Astra : Skills //블, 디에 쓸 클래스
    {
        public GameObject SymbolPrefab;
        public bool isOn = false;

        public Astra(float max, GameObject projectileprefab, GameObject effectprefab) : base(max, projectileprefab, effectprefab) { }


    }