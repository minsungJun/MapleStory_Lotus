using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsingWorldPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public int scenes;
    bool inPortal = false;
    PlayerMove player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        loadScene(scenes);
    }

    void loadScene(int scenesnumber) 
    {
        if(player.moveInput.y == 1 && inPortal)
        {
            if(scenesnumber == 1)
            {
                inPortal = false;
                player.moveInput.y = 0;
                SceneManager.LoadScene("Phase1");
                
            }
            if(scenesnumber == 0)
            {
                inPortal = false;
                player.moveInput.y = 0;
                SceneManager.LoadScene("Entrance");
            }
        }

    }

    public void Dead() 
    {
        SceneManager.LoadScene("Entrance");
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 닿은 오브젝트 이름이 Monster일때
        {
            inPortal = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 닿은 오브젝트 이름이 Monster일때
        {
            inPortal = false;
        }
        
    }
}
