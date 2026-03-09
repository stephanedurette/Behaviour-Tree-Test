using R3;
using System;
public class Job
{
    public ReactiveProperty<float> ProgressValue { get; private set; }

    public Action OnComplete = delegate { };
    public Action OnStarted = delegate { };

    private IDisposable subscription;

    public Job()
    {
        subscription = ProgressValue.Subscribe(OnProgressValueChanged);
    }

    ~Job()
    {
        subscription.Dispose();
    }

    private void OnProgressValueChanged(float newValue)
    {
        if (newValue == 0)
        {
            OnStarted?.Invoke();
        }

        if (newValue > 1)
        {
            OnComplete?.Invoke();
        }
    }

    protected virtual void Update(Unit unit)
    {

    }
}
