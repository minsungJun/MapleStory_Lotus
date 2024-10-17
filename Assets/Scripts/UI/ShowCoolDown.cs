using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class ShowCoolDown : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text childTMPText;
    float cool;
    float maxcool;
    int stack;
    PlayerAttack player;
    public Image cooldownImage; 
    public string inputtype;
    Transform parentTransform;

    public Sprite AssultIcon1; //기본 디 블 트 
    public Sprite AssultIcon2;
    public Sprite AssultIcon3;    
    public Sprite AssultIcon4;

    public Sprite AstraIcon1; //기본 디 블 트 
    public Sprite AstraIcon2;
    public Sprite AstraIcon3;    
    public Sprite AstraIcon4;

    void Start()
    {
        parentTransform = transform.parent;
        player = GameObject.Find("Player").GetComponent<PlayerAttack>();
        childTMPText = transform.Find("CoolDownText").GetComponent<TMP_Text>();
        
        //Transform childTransform = 
        //스택 표시에 사용해야 함
        //Transform childTransform = transform.Find(quickslotbutton.name + 
        //ShowCoolDown childCoolDown = childTransform.GetComponent<ShowCoolDown>();
        //childCoolDown.inputtype = selectedSkillName;
        

    }
    void CheckCoolDown()
    {
        

        if(inputtype == "Resonance")
        {
            cool = player.resonance_class.ShotDelay;
            maxcool = player.resonance_class.MaxShotDelay;
        }
        else if(inputtype == "TripleImpact")
        {
            cool = player.tripleimpact_class.ShotDelay;
            maxcool = player.tripleimpact_class.MaxShotDelay;
        }
        else if(inputtype == "ComboAssult")
        {
            cool = player.comboassult_default_class.ShotDelay;
            maxcool = player.comboassult_default_class.MaxShotDelay;
        }
        else if(inputtype == "Transition")
        {
            cool = player.transition.ShotDelay;
            maxcool = player.transition.stack_charage;
            stack = player.transition.stack;
        }
        else if(inputtype == "Astra")
        {
            cool = player.astra_default_class.ShotDelay;
            maxcool = player.astra_default_class.MaxShotDelay;
        }
        else if(inputtype == "Potion")
        {
            cool = player.Potion_Cool;
            maxcool = player.Potion_MaxCool;
            //Debug.Log("" + inputtype);
        }
        else
        {
            cool = 0f;
            maxcool = 0f;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if(inputtype != null)
        {
            if(inputtype == "ComboAssult")
            {
                AssultIcon();
            }
            if(inputtype == "Astra")
            {
                AstraIcon();
            }
            CheckCoolDown();

            if(cool == maxcool)
            {
                cooldownImage.fillAmount = 0; 
            }
            cooldownImage.fillAmount = (maxcool - cool) / maxcool;
            //Debug.Log("" + cooldownImage.fillAmount);
            
            // 1이 꽉참 0이 빈거
            // 쿨타임이 0에서 max로 차니까 max에서 cool을 뺌 
            update_UI();
        }
        else
        {
            cooldownImage.fillAmount = 0;
        }
    }

    void update_UI()
    {
        if(inputtype == "Transition")
        {
            childTMPText.text = stack.ToString();
        }
        else
        {
            childTMPText.text = null;
        }
        

    }
    void AssultIcon()
    {
        if(player.PlayerSymbol == PlayerAttack.Symbol.normal)
        {
            parentTransform.GetComponent<Image>().sprite = AssultIcon1;
        }
        else if(player.PlayerSymbol == PlayerAttack.Symbol.discharge)
        {
            parentTransform.GetComponent<Image>().sprite = AssultIcon2;
        }
        else if(player.PlayerSymbol == PlayerAttack.Symbol.blast)
        {
            parentTransform.GetComponent<Image>().sprite = AssultIcon3;
        }
        else if(player.PlayerSymbol == PlayerAttack.Symbol.transition)
        {
            parentTransform.GetComponent<Image>().sprite = AssultIcon4;
        }

    }
    void AstraIcon()
    {
        if(player.PlayerSymbol == PlayerAttack.Symbol.normal)
        {
            parentTransform.GetComponent<Image>().sprite = AstraIcon1;
        }
        else if(player.PlayerSymbol == PlayerAttack.Symbol.discharge)
        {
            parentTransform.GetComponent<Image>().sprite = AstraIcon2;
        }
        else if(player.PlayerSymbol == PlayerAttack.Symbol.blast)
        {
            parentTransform.GetComponent<Image>().sprite = AstraIcon3;
        }
        else if(player.PlayerSymbol == PlayerAttack.Symbol.transition)
        {
            parentTransform.GetComponent<Image>().sprite = AstraIcon4;
        }

    }
}
