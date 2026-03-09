using UnityEngine;

public class ResourceGatheringJob : Job
{
    public Gatherable Gatherable { get; private set; }

    private int RemainingAmount;

    public ResourceGatheringJob(Gatherable gatherable) : base()
    {
        Gatherable = gatherable;
        RemainingAmount = Gatherable.MaxAmount;
    }

    public void Reset()
    {
        RemainingAmount = Gatherable.MaxAmount;
    }

    protected override void Update(Unit unit, float t) 
    {
        base.Update(unit, t);
    }
}
