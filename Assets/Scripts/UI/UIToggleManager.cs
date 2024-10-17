using UnityEngine;
using UnityEngine.InputSystem;  // New Input System을 사용
using UnityEngine.UI;

public class UIToggleManager : MonoBehaviour
{
    public GameObject SkillPanel;  // UI 패널을 드래그 앤 드롭하여 연결
    public GameObject ShortCutsPanel;
    public InputActionAsset inputActions;  // Input Action Asset 연결

    PlayerInputActions actions;
    private InputAction toggleUIAction;
    private InputAction SkillUIAction;
    private InputAction ShortCutUIAction;


    void Awake()
    {
        actions = new PlayerInputActions();
        //MoveAction = actions.Player.Move;
        toggleUIAction = actions.UI.UIToggle;
        // InputActionAsset에서 ToggleUI 액션을 찾습니다.
    
        SkillUIAction = actions.UI.SkillPOP;
        ShortCutUIAction = actions.UI.ShortcutsPOP;


        // 액션이 트리거될 때, UI를 토글하는 메서드를 호출
        toggleUIAction.performed += CloseTab;
        SkillUIAction.performed += SKillToggleUI;
        ShortCutUIAction.performed += ShortcutsToggleUI;
        //Debug.Log("" + toggleUIAction);
    }
    void CloseTab(InputAction.CallbackContext context)
    {
        if(SkillPanel.activeSelf)
        {
            SkillPanel.SetActive(false);
        }
        if(ShortCutsPanel.activeSelf)
        {
            ShortCutsPanel.SetActive(false);
        }
    }
    void SKillToggleUI(InputAction.CallbackContext context)
    {
        // UI 패널의 활성화 상태를 반전
        SkillPanel.SetActive(!SkillPanel.activeSelf);
    }
    void ShortcutsToggleUI(InputAction.CallbackContext context)
    {
        // UI 패널의 활성화 상태를 반전
        ShortCutsPanel.SetActive(!ShortCutsPanel.activeSelf);
    }

    private void OnEnable()
    {
        // ToggleUI 액션 활성화
        toggleUIAction.Enable();
        SkillUIAction.Enable();
        ShortCutUIAction.Enable();

    }

    private void OnDisable()
    {
        // ToggleUI 액션 비활성화
        toggleUIAction.Disable();
        SkillUIAction.Disable();
        ShortCutUIAction.Disable();
    }

    public InputAction GetSkillAction(string skillName)
    {
        // 스킬 이름에 따라 InputAction 반환
        switch (skillName)
        {
            case "SkillPOP":
                return actions.UI.SkillPOP;
            case "ShortcutsPOP":
                return actions.UI.ShortcutsPOP;
                
            // 필요한 만큼 케이스 추가
            default:
                return null; // 유효하지 않은 경우 null 반환
        }
    }
}