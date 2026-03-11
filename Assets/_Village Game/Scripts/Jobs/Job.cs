using R3;
using System;
public class Job
{
    public ReactiveProperty<float> ProgressValue { get; private set; } = new();

    public Action OnComplete = delegate { };

    private IDisposable subscription;

    public JobData JobData { get; private set; }

    public Job(JobData jobData)
    {
        JobData = jobData;
        subscription = ProgressValue.Subscribe(OnProgressValueChanged);
    }

    ~Job()
    {
        subscription.Dispose();
    }

    private void OnProgressValueChanged(float newValue)
    {
        if (newValue >= 1)
        {
            OnComplete?.Invoke();
        }
    }

    public virtual void Update(Unit unit, float timeSinceLastUpdate)
    {

    }
}
