using UnityEngine;

public class Gatherable : JobComponent
{
    [SerializeField] private ResourceCollectionNode resourceCollectionNode;

    protected override void Awake()
    {
        Job = new ResourceGatheringJob(jobData, resourceCollectionNode);
    }
}
