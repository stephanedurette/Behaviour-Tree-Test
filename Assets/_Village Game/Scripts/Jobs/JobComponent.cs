using UnityEngine;
using UnityEngine.Events;

public class JobComponent : MonoBehaviour
{
    [Header("Job Component Settings")]
    [SerializeField] protected JobData jobData;

    [Header("Job Component Events")]
    [SerializeField] private UnityEvent<Component> OnEnabled;
    [SerializeField] private UnityEvent<Component> OnDisabled;

    public Job Job { get; protected set; }

    protected virtual void Awake()
    {

    }

    private void OnEnable()
    {
        OnEnabled?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke(this);
    }
}
