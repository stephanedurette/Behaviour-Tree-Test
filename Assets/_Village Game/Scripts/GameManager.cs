using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private CameraController cameraController;

    [Inject]
    public void Construct(CameraController cameraController)
    {
        this.cameraController = cameraController;
    }

    public void OnWorldPositionSelected(Vector2 worldPos)
    {
        cameraController.TargetPosition = worldPos;
    }
}
