using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttack", menuName = "Unit/Action/Attack Action")]
public class AttackAction : Action {
    
    public override Action CreateInstance(Unit unit) {
        AttackAction instance = ScriptableObject.CreateInstance<AttackAction>();
        instance.Init(unit, "Attack");
        return instance;
    }

    public override bool canActOn(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path) {
        return self.canAttack(unit);
    }

    public override bool executeAction(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path) {
        //TODO: implement the attack
        return true;
    }

    public override List<Vector3Int> getActionRange() {
        //TODO: calculate this based on unit attack range and unit current pos;
        return new List<Vector3Int>();
    }

    public override TargetType getTargetType() {
        //TODO: this should be fetched from unit data, more specifically attack type
        return TargetType.NON_EMPTY;
    }

    public override bool postExecute(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path) {
        self.endTurn();
        return true;
    }

    public override bool preExecute(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path) {
        //TODO: implement a way to let the user to select target
        return true;
    }

    public override bool requiresTarget() {
        return true;
    }
}
