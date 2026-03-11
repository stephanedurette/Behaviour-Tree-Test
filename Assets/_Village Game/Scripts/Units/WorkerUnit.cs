using System.Collections.Generic;

public class WorkerUnit : Unit
{
    public readonly Dictionary<string, int> GatherableMaxInventoryValues = new()
    {
        {"Gold", 50 },
    };

    public Dictionary<string, int> Inventory = new()
    {
        {"Gold", 0 }
    };
}
