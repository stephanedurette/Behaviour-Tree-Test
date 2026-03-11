using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Job", story: "[Agent] Finds [Job] Of Type [JobData]", category: "Action", id: "6b63b7fa86ffeb4da0cbeee6e04595ee")]
public partial class FindJobAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<JobComponent> Job;
    [SerializeReference] public BlackboardVariable<JobData> JobData;

    protected override Status OnStart()
    {
        Job.Value = Agent.Value.GetComponent<Unit>().FindJob(JobData.Value);
        return Job.Value == null ? Status.Failure : Status.Success;
    }
}

