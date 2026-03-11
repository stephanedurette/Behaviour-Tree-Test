using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WorkJobAction", story: "Assign [Job] To [Agent] and work", category: "Action", id: "af43b2d007c535a76ca106f6d1edc63b")]
public partial class WorkJobAction : Action
{
    [SerializeReference] public BlackboardVariable<JobView> Job;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    protected override Status OnStart()
    {
        Agent.Value.GetComponent<Unit>().AssignJob(Job);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

