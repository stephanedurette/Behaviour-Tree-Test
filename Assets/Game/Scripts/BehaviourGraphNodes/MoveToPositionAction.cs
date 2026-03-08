using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Pathfinding;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToPosition", story: "[Self] moves to [position] with [speed] using pathfinding", category: "Action", id: "765ebd2a64bdc4364c6548cc0139e1f8")]
public partial class MoveToPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector2> Position;
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Unity.Behavior.Node.Status currentStatus = Status.Running;

    private Unit unit;

    protected override Status OnStart()
    {
        currentStatus = Status.Running;

        if (unit == null) { 
            InitializeUnit();
        }

        unit.MoveToLocation(Position.Value, Speed.Value);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return currentStatus;
    }

    protected override void OnEnd()
    {
    }

    private void InitializeUnit()
    {
        unit = Self.Value.GetComponent<Unit>();
        unit.OnMoveStarted.AddListener(OnPathStarted);
        unit.OnMoveFinished.AddListener(OnPathCompleted);
    }

    private void OnPathStarted()
    {
        currentStatus = Status.Running;
    }

    private void OnPathCompleted() { 
        currentStatus = Status.Success;
        Debug.Log("blah2");
    }
}

