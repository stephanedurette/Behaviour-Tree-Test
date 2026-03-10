using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetJobPosition", story: "Gets the [Job] [Position]", category: "Action", id: "3edb80ae14c22586fd9d6aac8e046fa0")]
public partial class GetJobPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<JobView> Job;
    [SerializeReference] public BlackboardVariable<Vector2> Position;

    protected override Status OnStart()
    {
        Position.Value = Job.Value.transform.position;
        return Status.Success;
    }
}

