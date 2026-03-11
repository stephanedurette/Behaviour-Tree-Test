using UnityEngine;
using UnityEngine.Events;

public class JobComponent : MonoBehaviour
{
    [Header("Job Component Settings")]
    [SerializeField] protected JobData jobData;

    [Header("Job Component Events")]
    [SerializeField] private UnityEvent<JobComponent> OnEnabled;
    [SerializeField] private UnityEvent<JobComponent> OnDisabled;
    [SerializeField] private UnityEvent<JobComponent> OnFinished;

    public Job Job { get; protected set; }

    protected virtual void Awake()
    {
        //Job is assigned here through inherited classes
    }

    private void OnEnable()
    {
        Job.OnComplete += () => OnFinished?.Invoke(this);
        OnEnabled?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke(this);
    }
}
