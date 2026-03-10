using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnCameraMoveDirectionChanged;
    [SerializeField] private UnityEvent<int> OnCameraScroll;
    [SerializeField] private UnityEvent<Vector2> OnScreenSelected;

    private Vector2 mousePosition;

    private bool pointerOverUI;

    public void OnMousePress(CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case UnityEngine.InputSystem.InputActionPhase.Performed:
                if (pointerOverUI) 
                    return;
                OnScreenSelected?.Invoke(Camera.main.ScreenToWorldPoint(mousePosition));
                break;
            default:
                break;
        }
    }

    public void OnMousePositionChanged(CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case UnityEngine.InputSystem.InputActionPhase.Performed:
                mousePosition = ctx.ReadValue<Vector2>();
                break;
            default:
                break;
        }
    }

    public void OnMoveCamera(CallbackContext ctx)
    {
        switch (ctx.phase) { 
            case UnityEngine.InputSystem.InputActionPhase.Performed:
                OnCameraMoveDirectionChanged?.Invoke(ctx.ReadValue<Vector2>());
                break;
            case UnityEngine.InputSystem.InputActionPhase.Canceled:
                OnCameraMoveDirectionChanged?.Invoke(Vector2Int.zero);
                break;
            default:
                break;
        }
    }

    public void OnScrollCamera(CallbackContext ctx) 
    {
        switch (ctx.phase) {
            case UnityEngine.InputSystem.InputActionPhase.Performed:
                OnCameraScroll?.Invoke((int)ctx.ReadValue<float>());
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }
}
