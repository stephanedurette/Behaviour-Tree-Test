using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraFollowTransform;

    [Header("Settings")]
    [SerializeField] private float cameraMoveSpeed;

    private Vector2 moveDirection;

    public void OnMoveDirectionChanged(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void OnScroll(int scrollDirection)
    {

    }

    private void Update()
    {
        cameraFollowTransform.position += (Vector3)(cameraMoveSpeed * Time.deltaTime * moveDirection);
    }
}
