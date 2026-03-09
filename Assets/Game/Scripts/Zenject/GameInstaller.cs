using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CameraController cameraController;

    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(cameraController).AsSingle();
    }
}
