using UnityEngine;
using Zenject;

public class JobView : MonoBehaviour
{
    [SerializeField] protected JobData jobData;

    public Job Job { get; protected set; }
    private JobManager jobManager;

    [Inject]
    public void Construct(JobManager jobManager)
    {
        this.jobManager = jobManager;
    }

    protected virtual void Awake()
    {
        
    }

    private void OnEnable()
    {
        jobManager.RegisterJob(this);
    }

    private void OnDisable()
    {
        jobManager.UnregisterJob(this);
    }
}
