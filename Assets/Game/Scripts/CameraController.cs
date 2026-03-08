using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float cameraMoveSpeed;

    [Header("Zoom Settings")]
    [SerializeField] private float maxCameraSize = 15f;
    [SerializeField] private float startingCameraSize = 8f;
    [SerializeField] private float minCameraSize = 6f;
    [SerializeField] private float cameraZoomStepSize = .5f;

    private CinemachineCamera targetCamera;
    private Transform cameraFollowTransform;
    private Collider2D targetCameraBounds;

    private Vector3? targetPosition;

    public Vector3? TargetPosition { get { return targetPosition; } set { targetPosition = value; } }

    private void Awake()
    {
        targetCamera = GetComponent<CinemachineCamera>();
        cameraFollowTransform = targetCamera.Target.TrackingTarget.transform;
        targetCameraBounds = GetComponent<CinemachineConfiner2D>().BoundingShape2D;

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
        cameraFollowTransform.position = ClampToCameraBounds(cameraFollowTransform.position);
    }

    public void OnSelect(Vector2 worldPos)
    {
        targetPosition = worldPos;
    }

    private Vector3 ClampToCameraBounds(Vector3 position)
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
        Vector3 distance = (ClampToCameraBounds(targetPosition.Value) - cameraFollowTransform.position);

        if (distance.sqrMagnitude < epsilon) return;
        
        cameraFollowTransform.position += cameraMoveSpeed * Time.deltaTime * distance.normalized;
    }
}
