using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMove", menuName = "Unit/Action/Movement Action")]
public class MovementAction : Action {

    public override Action CreateInstance(Unit unit) {
        MovementAction instance = ScriptableObject.CreateInstance<MovementAction>();
        instance.Init(unit, "Move");
        return instance;
    }

    public override bool canActOn(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path) {
        foreach (Vector2Int pathPos in path) {
            // if pathPos has unit, return false
        }
        return true;
    }

    public override bool executeAction(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path) {
        //TODO: implement move
        return true;
    }

    public override List<Vector3Int> getActionRange() {
        //TODO: use movementType and movement cost to calculate this.
        return new List<Vector3Int>();
    }

    public override TargetType getTargetType() {
        return TargetType.ANY;
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
