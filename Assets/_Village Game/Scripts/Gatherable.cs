using UnityEngine;

public class Gatherable : JobComponent
{
    [Header("Gatherable Settings")]
    [SerializeField] private ResourceCollectionNode resourceCollectionNode;

    protected override void Awake()
    {
        Job = new ResourceGatheringJob(jobData, resourceCollectionNode);
    }
}
