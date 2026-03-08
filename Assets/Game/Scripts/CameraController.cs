using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraFollowTransform;
    [SerializeField] private CinemachineCamera targetCamera;
    [SerializeField] private Collider2D targetCameraBounds;

    [Header("Move Settings")]
    [SerializeField] private float cameraMoveSpeed;

    [Header("Zoom Settings")]
    [SerializeField] private float maxCameraSize = 15f;
    [SerializeField] private float startingCameraSize = 8f;
    [SerializeField] private float minCameraSize = 6f;
    [SerializeField] private float cameraZoomStepSize = .5f;

    private Vector3? targetPosition;

    private void Awake()
    {
        targetCamera.Lens.OrthographicSize = startingCameraSize;
    }

    public void OnMoveDirectionChanged(Vector2 moveDirection)
    {
        float largeNumber = 1000f;
        if (moveDirection == Vector2.zero)
        {
            targetPosition = null;
        } else
        {
            targetPosition = cameraFollowTransform.position + (Vector3)(moveDirection * largeNumber);
        }
    }

    public void OnScroll(int scrollDirection)
    {
        targetCamera.Lens.OrthographicSize = Mathf.Clamp(targetCamera.Lens.OrthographicSize - cameraZoomStepSize * scrollDirection, minCameraSize, maxCameraSize);
        cameraFollowTransform.position = CameraBoundedPosition(cameraFollowTransform.position);
    }

    public Vector3 CameraBoundedPosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, targetCameraBounds.bounds.min.x + targetCamera.Lens.OrthographicSize * Camera.main.aspect, targetCameraBounds.bounds.max.x - targetCamera.Lens.OrthographicSize * Camera.main.aspect),
            Mathf.Clamp(position.y, targetCameraBounds.bounds.min.y + targetCamera.Lens.OrthographicSize, targetCameraBounds.bounds.max.y - targetCamera.Lens.OrthographicSize),
            0
            );
    }

    private void Update()
    {
        UpdateMoveToPosition();
    }

    private void UpdateMoveToPosition()
    {
        if (targetPosition == null) return;

        float epsilon = 0.01f;
        Vector3 distance = (CameraBoundedPosition(targetPosition.Value) - cameraFollowTransform.position);

        if (distance.sqrMagnitude < epsilon) return;
        
        cameraFollowTransform.position += cameraMoveSpeed * Time.deltaTime * distance.normalized;
    }
}
