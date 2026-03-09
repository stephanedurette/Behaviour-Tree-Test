using R3;
using System;
using UnityEngine;

public class ResourceGatheringJob : Job
{
    public ResourceCollectionNode Gatherable { get; private set; }

    private ReactiveProperty<int> RemainingAmount = new();

    private IDisposable subscription;

    public ResourceGatheringJob(ResourceCollectionNode gatherable) : base()
    {
        Gatherable = gatherable;
        subscription = RemainingAmount.Subscribe(OnRemainingAmountValueChanged);

        Gatherable = gatherable;
        RemainingAmount.Value = Gatherable.MaxAmount;
    }

    public void Reset()
    {
        RemainingAmount.Value = Gatherable.MaxAmount;
    }

    private void OnRemainingAmountValueChanged(int newValue)
    {
        ProgressValue.Value = (1 - newValue / Gatherable.MaxAmount);
    }

    protected override void Update(Unit unit, float t) 
    {
        base.Update(unit, t);

        WorkerUnit worker = unit as WorkerUnit;
        
        int amountExtracted = Mathf.CeilToInt(t * worker.GatheringSpeeds[Gatherable.name]);
        RemainingAmount.Value -= amountExtracted;
        worker.Inventory[Gatherable.name] += amountExtracted;
    }

    ~ResourceGatheringJob() { subscription.Dispose(); }
}
