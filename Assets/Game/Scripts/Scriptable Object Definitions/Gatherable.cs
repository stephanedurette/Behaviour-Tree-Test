using UnityEngine;

[CreateAssetMenu(fileName = "Gatherable", menuName = "Scriptable Objects/Gatherable")]
public class Gatherable : ScriptableObject
{
    public int MaxAmount;
    public int AmountExtractedPerSecond;
    public int TimeToRespawn;
}
