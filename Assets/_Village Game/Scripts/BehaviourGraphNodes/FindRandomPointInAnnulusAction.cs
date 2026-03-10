using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Random Point In Annulus", story: "Computes a [position] in an annulus based on [minimum] and [maximum] radius values around [self]", category: "Action/Find", id: "26f57f3f21a8bd8d4de8f324ab30a613")]
public partial class FindRandomPointInAnnulusAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> Position;
    [SerializeReference] public BlackboardVariable<float> Minimum;
    [SerializeReference] public BlackboardVariable<float> Maximum;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        Position.Value = RandomPointInAnnulus(Self.Value.transform.position, Minimum.Value, Maximum.Value);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        float angle = UnityEngine.Random.value * Mathf.PI * 2f;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Squaring and then square-rooting radii to ensure uniform distribution within the annulus
        float minRadiusSquared = minRadius * minRadius;
        float maxRadiusSquared = maxRadius * maxRadius;
        float distance = Mathf.Sqrt(UnityEngine.Random.value * (maxRadiusSquared - minRadiusSquared) + minRadiusSquared);

        // Calculate the position vector
        Vector2 position = direction * distance;
        return origin + position;
    }
}

