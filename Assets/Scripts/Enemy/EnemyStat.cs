using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStat : MonoBehaviour
{
    public float hp = 100f;
    public int Curse = 0;
    [SerializeField] int phaseType;

    public List<GameObject> childObjects;// 하위 오브젝트를 할당

    public GameObject Die_effect;
    public Transform damageposition;
    GameObject is_dead;

    Vector3 pos = new Vector3(0f, 4.9f, 0f);
    

    [SerializeField] private Animator parentAnimator;
    [SerializeField] private List<Animator> childAnimators;
    [SerializeField] private List<GameObject> childs;
    // Start is called before the first frame update
    void Start()
    {
       parentAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Curse > 5)
        {
            Curse = 5;
        }
    }

    IEnumerator loadScene(int phasenumber) {
        
        if(phasenumber == 1)
        {
            yield return new WaitForSeconds(4.7f);
            SceneManager.LoadScene("Phase2");
        }
        if(phasenumber == 2)
        {
            yield return new WaitForSeconds(3.5f);
            SceneManager.LoadScene("Phase3");
        }
        if(phasenumber == 3)
        {
            yield return new WaitForSeconds(3.5f);
            SceneManager.LoadScene("End");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) // 닿은 오브젝트 이름이 Monster일때
        {
            Debug.Log("Hit : " + hp); // 디버그용
            is_dead = GameObject.Find("die(Clone)");
            Debug.Log(null + " ?? " + is_dead);
            if(hp <= 0f && is_dead == null)
            {
                
                if(phaseType == 1)
                {
                    parentAnimator.SetBool("Dead", true);
                    foreach (Animator childAnimator in childAnimators)
                    {
                        SyncAnimatorParameters(childAnimator); //파라미터 동기화
                        Invoke("Next", 0.1f);
                    }
                    Instantiate(Die_effect, new Vector3(0f, 4.9f, 0), Quaternion.identity);//사망 애니메이션
                    //사망 애니메이션 실행 후 장면 전환 넣기
                    StartCoroutine(loadScene(1));
                }
                
                if(phaseType == 2)
                {
                    parentAnimator.SetTrigger("Dead");
                    foreach (Animator childAnimator in childAnimators)
                    {
                        SyncAnimatorParameters(childAnimator);
                    }
                    // Instantiate(Die_effect, transform.position, Quaternion.identity);//사망 애니메이션
                    StartCoroutine(loadScene(2));
                }

                if(phaseType == 3)
                {
                    parentAnimator.SetTrigger("Dead");
                    foreach (Animator childAnimator in childAnimators)
                    {
                        SyncAnimatorParameters(childAnimator);
                    }
                    // Instantiate(Die_effect, transform.position, Quaternion.identity);//사망 애니메이션
                    StartCoroutine(loadScene(3));
                }
                
            }
        }
    }
    void Next()
    {
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }
    }

    void SyncAnimatorParameters(Animator childAnimator)
    {
        // 여기서 파라미터들을 하나씩 동기화
        //bool isJumping = parentAnimator.GetBool("IsJumping"); // 파라미터 이름에 맞게 수정

        // 하위 오브젝트의 애니메이터에 전달
        childAnimator.SetBool("Dead", true);
        Debug.Log("working");
        // 필요에 따라 더 많은 파라미터를 추가할 수 있습니다.
    }
}
