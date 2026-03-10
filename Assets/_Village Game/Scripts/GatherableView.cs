using UnityEngine;
using Zenject;

public class GatherableView : MonoBehaviour
{
    [SerializeField] private JobData jobData;
    [SerializeField] private ResourceCollectionNode resourceCollectionNode;

    private ResourceGatheringJob job;
    private JobManager jobManager;

    [Inject]
    public void Construct(JobManager jobManager)
    {
        this.jobManager = jobManager;
    }

    private void Awake()
    {
        job = new(jobData, resourceCollectionNode);
    }

    private void OnEnable()
    {
        jobManager.RegisterJob(job);
    }

    private void OnDisable()
    {
        jobManager.UnregisterJob(job);
    }
}
