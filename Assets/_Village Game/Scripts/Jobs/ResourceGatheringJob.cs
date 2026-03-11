using R3;
using System;
using UnityEngine;

public class ResourceGatheringJob : Job
{
    public ResourceCollectionNode Gatherable { get; private set; }

    private ReactiveProperty<int> RemainingAmount = new();

    private IDisposable subscription;

    public ResourceGatheringJob(JobData jobData, ResourceCollectionNode gatherable) : base(jobData)
    {
        Gatherable = gatherable;
        subscription = RemainingAmount.Subscribe(OnRemainingAmountValueChanged);

        RemainingAmount.Value = Gatherable.MaxAmount;
    }

    public void Reset()
    {
        RemainingAmount.Value = Gatherable.MaxAmount;
    }

    private void OnRemainingAmountValueChanged(int newValue)
    {
        ProgressValue.Value = (1f - (float)newValue / Gatherable.MaxAmount);
    }

    public override void Update(Unit unit, float timeSinceLastUpdate) 
    {
        base.Update(unit, timeSinceLastUpdate);

        WorkerUnit worker = unit as WorkerUnit;
        
        int amountExtracted = Mathf.CeilToInt(timeSinceLastUpdate * Gatherable.AmountExtractedPerSecond);
        RemainingAmount.Value -= amountExtracted;
        worker.Inventory[Gatherable.Resource.name] += amountExtracted;
    }

    ~ResourceGatheringJob() { subscription.Dispose(); }
}
