using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    // 버튼을 클릭하면 실행될 메서드
    public void CloseParent()
    {
        // 현재 객체의 부모를 비활성화
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}