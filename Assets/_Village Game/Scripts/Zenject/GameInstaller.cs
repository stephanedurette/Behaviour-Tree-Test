using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private JobManager jobManager;

    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(cameraController).AsSingle();
        Container.Bind<JobManager>().FromInstance(jobManager).AsSingle();
    }
}
