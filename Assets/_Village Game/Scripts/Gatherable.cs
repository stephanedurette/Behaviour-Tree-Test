using UnityEngine;

public class Gatherable : JobView
{
    [SerializeField] private ResourceCollectionNode resourceCollectionNode;

    protected override void Awake()
    {
        Job = new ResourceGatheringJob(jobData, resourceCollectionNode);
    }
}
