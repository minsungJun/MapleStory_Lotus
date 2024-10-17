using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UIDrag : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private bool isDragging = false;
    private Vector2 originalPosition;
    private Vector2 dragOffset;

    // Input Actions
    public InputActionAsset inputActions;
    private InputAction pointAction;
    private InputAction clickAction;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        
        // InputAction 초기화
        pointAction = inputActions.FindActionMap("UI").FindAction("Point");
        clickAction = inputActions.FindActionMap("UI").FindAction("Click");

        // 마우스 클릭 처리
        clickAction.performed += OnClickPerformed;
        clickAction.canceled += OnClickCanceled;
    }

    private void OnEnable()
    {
        pointAction.Enable();
        clickAction.Enable();
    }

    private void OnDisable()
    {
        pointAction.Disable();
        clickAction.Disable();
    }

    private void Update()
    {
        // 드래그 중일 때만 처리
        if (isDragging)
        {
            Vector2 mousePosition = pointAction.ReadValue<Vector2>();
            Vector2 delta = mousePosition - dragOffset;
            rectTransform.anchoredPosition = delta / canvas.scaleFactor;
            
        }

    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        // 클릭 시 마우스 위치 확인
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out var localPoint);

        // 클릭한 위치가 패널 안에 있으면 드래그 시작
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePosition, canvas.worldCamera))
        {
            isDragging = true;
            dragOffset = mousePosition - rectTransform.anchoredPosition * canvas.scaleFactor;
        }
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        // 마우스 클릭 해제 시 드래그 종료
        isDragging = false;
    }
}
