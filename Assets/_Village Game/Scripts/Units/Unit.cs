using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Unit : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMoveStarted;
    [HideInInspector] public UnityEvent OnMoveFinished;

    private Seeker seeker;
    private AILerp aiLerp;

    private JobManager jobManager;

    [Inject]
    public void Construct(JobManager jobManager)
    {
        this.jobManager = jobManager;
    }

    protected virtual void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiLerp = GetComponent<AILerp>();
    }

    public void MoveToLocation(Vector2 destination, float speed)
    {
        aiLerp.speed = speed;
        seeker.StartPath(transform.position, destination);
        StartCoroutine(WaitForTargetReachedCoroutine());
    }

    private IEnumerator WaitForTargetReachedCoroutine()
    {
        OnMoveStarted?.Invoke();
        yield return new WaitUntil(() => !aiLerp.reachedEndOfPath);
        yield return new WaitUntil(() => aiLerp.reachedEndOfPath);
        OnMoveFinished?.Invoke();
    }

    public JobComponent FindJob(JobData jobData)
    {
        return jobManager.FindJob(jobData);
    }

    public void AssignJob(JobComponent jobView) { 
        jobManager.AssignJob(this, jobView);
    }
}
