using System.Collections.Generic;

public class WorkerUnit : Unit
{
    public readonly Dictionary<string, int> GatherableMaxInventoryValues = new()
    {
        {"Gold", 50 },
    };

    public readonly Dictionary<string, int> GatheringSpeeds = new()
    {
        {"Gold", 5 },
    };

    public Dictionary<string, int> Inventory = new()
    {
        {"Gold", 0 }
    };

    public float GatheringInterval { get; private set; } = 1f;
}
