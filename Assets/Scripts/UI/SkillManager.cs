using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using TMPro; 

public class SkillManager : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerMove playerMove;
    public UIToggleManager uiToggleManager;
    public Button[] skillButtons;  // 스킬 버튼 목록
    public Button[] quickSlotButtons;  // 퀵슬롯 버튼 목록
    public Button[] shortcutsButtons;  // 숏컷 버튼 목록
    public Sprite[] icons;

    private Sprite selectedSkillIcon = null;  // 선택한 스킬의 아이콘 저장
    
    private string selectedSkillName = null;
    public Sprite defaultQuickSlotIcons;
    public Sprite JumpIcons;
    public Sprite SkillIcons;
    public Sprite ShortcutsIcons;
    

    public InputAction clickAction;  // 클릭 액션
    public InputActionAsset inputActionAsset;
    private InputActionMap playerActionMap; // Player Action Map
    public GameData gamedata;
    


    void OnEnable()
    {
        clickAction.Enable(); // 클릭 액션 활성화
        clickAction.performed += OnClickPerformed; // 클릭 이벤트 연결
    }

    void OnDisable()
    {
        clickAction.performed -= OnClickPerformed; // 클릭 이벤트 해제
        clickAction.Disable(); // 클릭 액션 비활성화

        SaveBindings();
    }

    void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerMove = FindObjectOfType<PlayerMove>();
        uiToggleManager = FindObjectOfType<UIToggleManager>();
        playerActionMap = inputActionAsset.FindActionMap("Player", true); // "Player"라는 이름의 Action Map을 가져옴
        if(playerActionMap != null)
        {
            
        }
        
        
    }
    void Start()
    {
        if(gamedata.bindings != null)
        {
            ApplyBindings();
        }
    }


    // 클릭 이벤트 처리
    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        // 클릭 위치에 있는 UI 오브젝트를 가져오기
        var pointerPosition = Pointer.current.position.ReadValue(); // 마우스 또는 터치 위치
        var raycastResults = RaycastUI(pointerPosition); // UI 오브젝트에 Raycast

        foreach (var result in raycastResults)
        {
            var clickedButton = result.gameObject.GetComponent<Button>();
            if (clickedButton != null)
            {
                if (IsSkillButton(clickedButton))
                {
                    // 스킬 버튼 클릭 처리
                    OnSkillClicked(clickedButton);
                }
                else if (IsQuickSlotButton(clickedButton))
                {
                    // 퀵슬롯 버튼 클릭 처리
                    OnButtonClicked(clickedButton);
                }
                else if (IsShortcutsButton(clickedButton))
                {
                    // 퀵슬롯 버튼 클릭 처리
                    OnButtonClicked(clickedButton);
                }
            }
        }
    }

    // UI에 Raycast 수행 (클릭된 UI 오브젝트를 찾기 위한 함수)
    private System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> RaycastUI(Vector2 pointerPosition)
    {
        var eventData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current)
        {
            position = pointerPosition
        };

        var results = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, results);
        return results;
    }

    // 스킬 버튼 클릭 시 호출
    private void OnSkillClicked(Button clickedSkillButton)
    {
        // 스킬의 아이콘을 저장 (Image 컴포넌트에서 아이콘을 가져옴)
        selectedSkillIcon = clickedSkillButton.GetComponent<Image>().sprite;
        selectedSkillName = clickedSkillButton.name;
        Debug.Log("선택한 스킬 아이콘: " + selectedSkillIcon.name);
        Debug.Log("선택한 스킬 이름: " + selectedSkillName);
    }

    // 퀵슬롯 클릭 시 호출
    public void OnButtonClicked(Button clickedButton)
    {
        if (selectedSkillIcon != null)
        {
            string selectedSlotname = clickedButton.name; //클릭된 슬롯 이름 가져옴
            string selectedSlotText = clickedButton.GetComponentInChildren<TMP_Text>().text; //클릭된 슬롯 text
            


            Button shortcutsbutton = FindShortcutsName(selectedSlotname); //숏컷 버튼 처리 시작
            TMP_Text shortcutsbuttonText = shortcutsbutton.GetComponentInChildren<TMP_Text>(); //버튼의 text 

            shortcutsbutton.GetComponent<Image>().sprite = selectedSkillIcon; //스킬 칸 아이콘 설정

            
            // 퀵슬롯에 스킬 아이콘을 표시 (Image 컴포넌트를 통해 아이콘 설정)

            TMP_Text quickslotbuttonText = GetComponent<TMP_Text>();
            Button quickslotbutton = FindQuickSlotName(selectedSlotname); //퀵슬롯 버튼 처리 시작
            
            if(quickslotbutton != null) // 퀵 슬롯에 있는 버튼일때
            {
                quickslotbuttonText = quickslotbutton.GetComponentInChildren<TMP_Text>();
                quickslotbutton.GetComponent<Image>().sprite = selectedSkillIcon; //스킬 칸 아이콘 설정
                

                Transform parentTransform = transform.parent;
                Transform transform1 = parentTransform.Find("Quick");
                Transform transform2 = transform1.Find("Keyname");
                Transform transform3 = transform2.Find(quickslotbutton.name);
                Transform transform4 = transform3.Find("CoolDown");

                //Debug.Log("error" + parentTransform.name + " " + transform1 + " " + transform2 + " " + transform3 + " " +  transform4);

                if(transform4 != null)
                {
                    ShowCoolDown childCoolDown = transform4.GetComponent<ShowCoolDown>();
                    childCoolDown.inputtype = selectedSkillName;
                    Debug.Log("error not" + selectedSkillName + " , " + childCoolDown.inputtype);
                    if(selectedSkillName == "Transition" )
                    {
                        TMP_Text childTMPText = transform4.Find("CoolDownText").GetComponent<TMP_Text>();
                    }
                }
                else
                {
                    //Transform parentTransform = transform.parent;
                    Debug.Log("error" + "Quick/Keyname/" + quickslotbutton.name + "/CoolDown");
                }
                
                
            }
            InputAction selectedSkill;
        
            if(selectedSkillName == "Jump")
            {
                selectedSkill = playerMove.GetSkillAction(selectedSkillName);//스킬 이름으로 action 참조
                // Debug.Log("Jump" + selectedSkill);
                 
            }
            else if(selectedSkillName == "SkillPOP" || selectedSkillName == "ShortcutsPOP")
            {
                selectedSkill = uiToggleManager.GetSkillAction(selectedSkillName);//스킬 이름으로 action 참조
                // Debug.Log("SkillPOP"  + selectedSkill);
            }
            else
            {
                selectedSkill = playerAttack.GetSkillAction(selectedSkillName);//스킬 이름으로 action 참조
                // Debug.Log("else"  + selectedSkill);
            }

            if (selectedSkill != null)
            {
                Debug.Log("null 아닙니다");
                
                CheckQuickSlotduplication(selectedSkillName);
                if(quickslotbutton != null)
                {
                    quickslotbuttonText.text = selectedSkillName;
                }
                CheckShortcutsduplication(selectedSkillName);
                
                InputAction existingSkill; //변경되기전 키에 할당된 스킬

                if(selectedSlotText == "Jump")
                {
                    existingSkill = playerMove.GetSkillAction(selectedSlotText);//스킬 이름으로 action 참조
                    Debug.Log("Jump" + existingSkill);
                    
                }
                else if(selectedSlotText == "SkillPOP" || selectedSlotText == "ShortcutsPOP")
                {
                    existingSkill = uiToggleManager.GetSkillAction(selectedSlotText);//스킬 이름으로 action 참조
                    Debug.Log("SkillPOP"  + existingSkill);
                }
                else
                {
                    existingSkill = playerAttack.GetSkillAction(selectedSlotText);//스킬 이름으로 action 참조
                    Debug.Log("else"  + existingSkill);
                }

                if(existingSkill != null)
                {
                    existingSkill.ApplyBindingOverride(""); //할당된 스킬의 바인드를 없앰
                    Debug.Log(existingSkill);
                }
                

                selectedSkill.ApplyBindingOverride("<Keyboard>/" + selectedSlotname); //참조한 action 슬롯 이름으로 변경
                playerActionMap.FindAction(selectedSkillName).ApplyBindingOverride("<Keyboard>/" + selectedSlotname);
                shortcutsbuttonText.text = selectedSkillName;


                // InputAction aaa = playerActionMap.FindAction(selectedSkillName);
                // Debug.Log("bind : "+(selectedSkill == aaa));
            }
            else
            {
                Debug.Log("null 입니다");
            }

            selectedSkillName = null; 
            selectedSkillIcon = null;  // 선택한 아이콘 초기화
            Debug.Log("퀵슬롯에 스킬 아이콘이 등록되었습니다.");
        }
        else
        {
            Debug.LogWarning("스킬이 선택되지 않았습니다.");
        }
    }
    


    private bool IsSkillButton(Button button)
    {
        foreach (var skillButton in skillButtons)
        {
            if (button == skillButton)
            {
                return true;
            }
        }
        return false;
    }

    // 퀵슬롯 버튼인지 확인
    private bool IsQuickSlotButton(Button button)
    {
        foreach (var quickSlotButton in quickSlotButtons)
        {
            if (button == quickSlotButton)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckQuickSlotduplication(string skillname)
    {

        foreach (var quickSlotButton in quickSlotButtons)
        {
            TMP_Text buttonText = quickSlotButton.GetComponentInChildren<TMP_Text>();


            if (buttonText.text == skillname)
            {
                Transform parentTransform = transform.parent;
                Transform transform1 = parentTransform.Find("Quick");
                Transform transform2 = transform1.Find("Keyname");
                Transform transform3 = transform2.Find(quickSlotButton.name);
                Transform transform4 = transform3.Find("CoolDown");
                ShowCoolDown childCoolDown = transform4.GetComponent<ShowCoolDown>();
                childCoolDown.inputtype = "";


                buttonText.text = "";
                quickSlotButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
            }
        }
    }

    private bool IsShortcutsButton(Button button)
    {
        foreach (var shortcutsButton in shortcutsButtons)
        {
            if (button == shortcutsButton)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckShortcutsduplication(string skillname)
    {

        foreach (var shortcutsButton in shortcutsButtons)
        {
            TMP_Text buttonText = shortcutsButton.GetComponentInChildren<TMP_Text>();
            if (buttonText.text == skillname)
            {
                buttonText.text = "";
                shortcutsButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
                Debug.Log("HI");
            }
        }
    }

    private Button FindQuickSlotName(string slotname)
    {
        foreach (var quickSlotButton in quickSlotButtons)
        {
            if (slotname == quickSlotButton.name)
            {
                return quickSlotButton;
            }
        }
        return null;
    }

    private Button FindShortcutsName(string slotname)
    {
        foreach (var shortcutsButton in shortcutsButtons)
        {
            if (slotname == shortcutsButton.name)
            {
                return shortcutsButton;
            }
        }
        return null;
    }

    public void reset()
    {
        foreach (var shortcutsButton in shortcutsButtons)
        {
            TMP_Text buttonText = shortcutsButton.GetComponentInChildren<TMP_Text>();
            if(shortcutsButton.name == "LeftAlt")
            {
                buttonText.text = "Jump";
                shortcutsButton.GetComponent<Image>().sprite = JumpIcons;
                Debug.Log("alt");
                InputAction selectedSkill = playerMove.GetSkillAction(buttonText.text);
                selectedSkill.ApplyBindingOverride("<Keyboard>/" + shortcutsButton.name);
            }
            else if(shortcutsButton.name == "Backslash")
            {
                buttonText.text = "ShortcutsPOP";
                shortcutsButton.GetComponent<Image>().sprite = ShortcutsIcons;
                Debug.Log("Backslash");
                InputAction selectedSkill = uiToggleManager.GetSkillAction(buttonText.text);
                selectedSkill.ApplyBindingOverride("<Keyboard>/" + shortcutsButton.name);
            }
            else if(shortcutsButton.name == "K")
            {
                buttonText.text = "SkillPOP";
                shortcutsButton.GetComponent<Image>().sprite = SkillIcons;
                InputAction selectedSkill = uiToggleManager.GetSkillAction(buttonText.text);
                selectedSkill.ApplyBindingOverride("<Keyboard>/" + shortcutsButton.name);
            }
            else if(buttonText.text != "")
            {
                InputAction selectedSkill = playerAttack.GetSkillAction(buttonText.text);
                selectedSkill.ApplyBindingOverride("");
                buttonText.text = "";
                shortcutsButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
            }

            else
            {
                buttonText.text = "";
                shortcutsButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
            }
            
        }
        

        foreach (var quickSlotButton in quickSlotButtons)
        {
            Transform parentTransform = transform.parent;
            Transform transform1 = parentTransform.Find("Quick");
            Transform transform2 = transform1.Find("Keyname");
            Transform transform3 = transform2.Find(quickSlotButton.name);
            Transform transform4 = transform3.Find("CoolDown");

            ShowCoolDown childCoolDown = transform4.GetComponent<ShowCoolDown>();
            
            TMP_Text buttonText = quickSlotButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = "";
            childCoolDown.inputtype = "";
            quickSlotButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
            
        }
    }

    
    public void SaveBindings()
    {
        Debug.Log("Save Start");
        // ScriptableObject에 있는 리스트를 초기화
        gamedata.bindings = new List<GameData.BindingInfo>();

        // 모든 액션맵을 순회
        foreach (var actionMap in inputActionAsset.actionMaps)
        {

            // 각 액션을 순회
            foreach (var action in actionMap.actions)
            {
                if(action.name == "Move" || (actionMap.name == "UI" && (action.name != "SkillPOP" && action.name != "ShortcutsPOP")))
                {
                    //Debug.Log("skip move" + action.name + "");
                    continue;
                }
                // 해당 액션의 모든 바인딩을 가져옴
                foreach (var binding in action.bindings)
                {
                    //Debug.Log(" binding info : " + binding);
                    // 바인딩 정보를 ScriptableObject에 저장
                    var bindingInfo = new GameData.BindingInfo
                    {
                        actionName = action.name,          // 액션 이름 저장
                        bindingPath = binding.effectivePath, // 바인딩된 경로 저장
                        controlName = binding.name         // 바인딩 이름 저장
                    };

                    // 바인딩 리스트에 추가
                    gamedata.bindings.Add(bindingInfo);
                }
            }
        }

        Debug.Log("Input bindings have been saved to ScriptableObject.");
    }

    public void ApplyBindings()
    {
        Debug.Log("Apply Start");
        // ScriptableObject에 저장된 각 바인딩을 순회
        foreach (var bindingInfo in gamedata.bindings)
        { //actionName 액션 이름 bindingPath 키 이름
            // 액션맵에서 해당 액션을 찾음
            InputAction action = inputActionAsset.FindAction(bindingInfo.actionName);
            if(action.name == "Move")
            {
                Debug.Log("skip move");
                continue;
            }
            //Debug.Log("bind" + (bindingInfo.bindingPath ) + " , " + action);

            if (action != null)
            {
                // Composite 바인딩 확인

                    InputAction other;
                    // Composite 바인딩인 경우

                    // Composite가 아닌 경우에는 그냥 적용
                    action.ApplyBindingOverride(bindingInfo.bindingPath);

                    //Debug.Log("isComposite == false  " + action.name );
                    if(action.name == "Jump" || action.name == "Move")
                    {
                        other = playerMove.GetSkillAction(action.name);//스킬 이름으로 action 참조
                        // Debug.Log("Jump" + selectedSkill);
                        
                    }
                    else if(action.name == "SkillPOP" || action.name == "ShortcutsPOP")
                    {
                        other = uiToggleManager.GetSkillAction(action.name);//스킬 이름으로 action 참조
                        // Debug.Log("SkillPOP"  + selectedSkill);
                    }
                    else
                    {
                        other = playerAttack.GetSkillAction(action.name);//스킬 이름으로 action 참조
                        //Debug.Log("else"  + action.name);
                    }
                    //Debug.Log("other : " + other);
                    other.ApplyBindingOverride(bindingInfo.bindingPath); //다른 스크립트에 있는 inputaction에 적용





                    //Debug.Log($"Applied binding {bindingInfo.bindingPath} to action {bindingInfo.actionName}");
                    
                
            }
            else
            {
                //Debug.LogWarning($"Action '{bindingInfo.actionName}' not found in InputActionAsset.");
            }
        }
        Debug.Log("Apply End");
        KeySetting();
    }


    public void KeySetting()
    {
        Debug.Log("keysetting start");
        foreach (var bindingInfo in gamedata.bindings)
        {
            string originalBindingPath = bindingInfo.bindingPath;
            string modifiedBindingPath = originalBindingPath.Replace("<Keyboard>/", "");
            modifiedBindingPath = modifiedBindingPath;
            //Debug.Log("modifiedBindingPath : " + modifiedBindingPath);
            //actionName 액션 이름 bindingPath 키 이름
            foreach (var shortcutsButton in shortcutsButtons)//숏컷을 순회하면서 bindingPath와 같은 이름의 버튼 검색 만약 같다면 text에 actionname 지정
            {                                                //이거 이후에 퀵슬롯, 아이콘 지정만 구현해보기
                TMP_Text buttonText = shortcutsButton.GetComponentInChildren<TMP_Text>();
                //Debug.Log("Key : "  + shortcutsButton.name + " , " + modifiedBindingPath + " , bindingInfo : " + bindingInfo.actionName);
                if(shortcutsButton.name.Equals(modifiedBindingPath, StringComparison.OrdinalIgnoreCase))//버튼의 이름이 저장된 키 바인드와 같으면 아이콘이랑 text에 이름 저장
                {//shortcutsButton.name.Equals(modifiedBindingPath, StringComparison.OrdinalIgnoreCase)
                    //Debug.Log("shortcutsButton.name : " + modifiedBindingPath);
                    
                    buttonText.text = bindingInfo.actionName;
                    foreach(var icon in icons)
                    {
                        if(icon.name == bindingInfo.actionName)
                        {
                            shortcutsButton.GetComponent<Image>().sprite = icon;
                            break;
                        }
                    }
                }
                else if(buttonText.text == bindingInfo.actionName)
                {
                    //Debug.Log("bindingInfo.actionName : " + modifiedBindingPath + ", " + buttonText.text);
                    shortcutsButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
                    buttonText.text = "";
                }
            }
            foreach (var quickSlotButton in quickSlotButtons)
            {
                TMP_Text buttonText = quickSlotButton.GetComponentInChildren<TMP_Text>();
                Transform parentTransform = transform.parent;
                Transform transform1 = parentTransform.Find("Quick");
                Transform transform2 = transform1.Find("Keyname");
                Transform transform3 = transform2.Find(quickSlotButton.name);
                Transform transform4 = transform3.Find("CoolDown");

                ShowCoolDown childCoolDown = transform4.GetComponent<ShowCoolDown>();
                
                if(quickSlotButton.name.Equals(modifiedBindingPath, StringComparison.OrdinalIgnoreCase))
                {
                    buttonText.text = bindingInfo.actionName;
                    foreach(var icon in icons)
                    {
                        if(icon.name == bindingInfo.actionName)
                        {
                            quickSlotButton.GetComponent<Image>().sprite = icon;
                        }
                    }
                    if(transform4 != null)
                    {  
                        childCoolDown.inputtype = bindingInfo.actionName;
                        //Debug.Log("error not" + bindingInfo.actionName + " , " + childCoolDown.inputtype);
                        if(bindingInfo.actionName == "Transition" )
                        {
                            TMP_Text childTMPText = transform4.Find("CoolDownText").GetComponent<TMP_Text>();
                        }
                    }
                }
                else if(buttonText.text == bindingInfo.actionName)
                {
                    //Debug.Log("bindingInfo.actionName : " + modifiedBindingPath + ", " + buttonText.text);
                    quickSlotButton.GetComponent<Image>().sprite = defaultQuickSlotIcons;
                    buttonText.text = "";
                    childCoolDown.inputtype = "";
                }
            }
        }
    }
}


