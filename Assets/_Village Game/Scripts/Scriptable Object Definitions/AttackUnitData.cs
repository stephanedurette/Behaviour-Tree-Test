using UnityEngine;

[CreateAssetMenu(fileName = "AttackUnitData", menuName = "Scriptable Objects/AttackUnitData")]
public class AttackUnitData : UnitData
{
    [Header("AttackUnitData Settings")]
    public AttackData AttackData;
}
