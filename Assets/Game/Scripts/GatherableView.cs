using UnityEngine;
using Zenject;

public class GatherableView : MonoBehaviour
{
    [SerializeField] private Gatherable gatherable;

    private ResourceGatheringJob job;
    private JobManager jobManager;

    [Inject]
    public void Construct(JobManager jobManager)
    {
        this.jobManager = jobManager;
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        job = new(gatherable);

        jobManager.RegisterJob(job);
    }
}
