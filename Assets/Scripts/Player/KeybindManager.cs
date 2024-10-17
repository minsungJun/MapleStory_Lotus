using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class KeybindManager : MonoBehaviour
{
    public InputActionAsset inputActionAsset; // InputActionAsset을 인스펙터에서 참조
    public GameData gamedata; // ScriptableObject 참조
    void Start()
    {

    }

    public void SaveBindings()
    {
        // ScriptableObject에 있는 리스트를 초기화
        gamedata.bindings = new List<GameData.BindingInfo>();

        // 모든 액션맵을 순회
        foreach (var actionMap in inputActionAsset.actionMaps)
        {
            // 각 액션을 순회
            foreach (var action in actionMap.actions)
            {
                // 해당 액션의 모든 바인딩을 가져옴
                foreach (var binding in action.bindings)
                {
                    Debug.Log(" binding info : " + binding);
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
        // ScriptableObject에 저장된 각 바인딩을 순회
        foreach (var bindingInfo in gamedata.bindings)
        {
            // 액션맵에서 해당 액션을 찾음
            InputAction action = inputActionAsset.FindAction(bindingInfo.actionName);
            Debug.Log("bind" + bindingInfo.bindingPath + " , " + action);

            if (action != null)
            {
                // Composite 바인딩 확인
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    var binding = action.bindings[i];

                    // Composite 바인딩인 경우
                    if (binding.isComposite)
                    {
                        Debug.Log("Composite binding found for action: " + action.name);

                        // 해당 Composite에 포함된 바인딩들 (Up, Down, Left, Right)
                        for (int j = i + 1; j < action.bindings.Count && action.bindings[j].isPartOfComposite; j++)
                        {
                            // 저장된 bindingPath와 일치하는지 확인
                            if (action.bindings[j].effectivePath == bindingInfo.bindingPath)
                            {
                                action.ApplyBindingOverride(j, bindingInfo.bindingPath);
                                Debug.Log($"Applied binding {bindingInfo.bindingPath} to composite part of action {bindingInfo.actionName} at index {j}");
                            }
                        }
                    }
                    else
                    {
                        // Composite가 아닌 경우에는 그냥 적용
                        action.ApplyBindingOverride(i, bindingInfo.bindingPath);
                        Debug.Log($"Applied binding {bindingInfo.bindingPath} to action {bindingInfo.actionName} at index {i}");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Action '{bindingInfo.actionName}' not found in InputActionAsset.");
            }
        }
    }
}
