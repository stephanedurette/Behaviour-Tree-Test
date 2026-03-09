using UnityEngine;

[CreateAssetMenu(fileName = "ResourceCollectionNode", menuName = "Scriptable Objects/ResourceCollectionNode")]
public class ResourceCollectionNode : ScriptableObject
{
    public int MaxAmount;
    public int AmountExtractedPerSecond;
    public int TimeToRespawn;
    public Resource Resource;
}
