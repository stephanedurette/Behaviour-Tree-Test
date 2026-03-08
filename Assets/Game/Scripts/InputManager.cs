using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnCameraMoveDirectionChanged;
    [SerializeField] private UnityEvent<int> OnCameraScroll;

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
}
