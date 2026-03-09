using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float cameraMoveSpeed;

    private CinemachineCamera targetCamera;
    private Transform cameraFollowTransform;
    private CinemachineConfiner2D cinemachineConfiner;

    private Vector3? targetPosition;

    public Vector3? TargetPosition { get { return targetPosition; } set { targetPosition = value; } }

    private void Awake()
    {
        targetCamera = GetComponent<CinemachineCamera>();
        cameraFollowTransform = targetCamera.Target.TrackingTarget.transform;
        cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        StartCoroutine(InvalidateConfinerBoundsAfterFrame());
    }

    private IEnumerator InvalidateConfinerBoundsAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        cinemachineConfiner.InvalidateBoundingShapeCache();
    }

    public void OnMoveDirectionChanged(Vector2 moveDirection)
    {
        float largeNumber = 1000f;
        if (moveDirection == Vector2.zero)
        {
            targetPosition = null;
        }
        else
        {
            targetPosition = cameraFollowTransform.position + (Vector3)(moveDirection * largeNumber);
        }
    }

    public void OnScroll(int scrollDirection) { /* noop */ }

    public void OnSelect(Vector2 worldPos)
    {
        targetPosition = worldPos;
    }

    private Vector3 ClampToCameraBounds(Vector3 position)
    {
        Bounds bounds = cinemachineConfiner.BoundingShape2D.bounds;

        return new Vector3(
            Mathf.Clamp(position.x, bounds.min.x + targetCamera.Lens.OrthographicSize * Camera.main.aspect, bounds.max.x - targetCamera.Lens.OrthographicSize * Camera.main.aspect),
            Mathf.Clamp(position.y, bounds.min.y + targetCamera.Lens.OrthographicSize, bounds.max.y - targetCamera.Lens.OrthographicSize),
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
