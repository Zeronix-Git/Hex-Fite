using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {
    protected Unit self;
    protected string actionName;
    
    public string getActionName() {
        return actionName;
    }

    protected void Init(Unit unit, string name) {
        this.self = unit;
        this.actionName = name;
    }

    public abstract Action CreateInstance(Unit unit);

    //Conditions for the action
    public abstract bool requiresTarget();
    public abstract List<Vector3Int> getActionRange();
    public abstract TargetType getTargetType();
    public abstract bool canActOn(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path); // target unit, target terrain, target location

    //executing the action
    public abstract bool preExecute(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path);
    public abstract bool executeAction(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path);
    public abstract bool postExecute(Unit unit, Terrain terrain, Vector3Int pos, List<Vector3Int> path);
}
