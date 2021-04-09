using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Terrain {
    private TerrainData terrainData;

    private Dictionary<MovementType, int> movementCost;
    private Dictionary<DefenseType, int> defenseModifier;

    public Terrain(TerrainData terrainData) {
        this.terrainData = terrainData;
    }

    public string getName() {
        return terrainData.name;
    }

    public int getIncome() {
        return terrainData.income;
    }

    public bool isTerrainMoidfier() {
        return terrainData.isTerranModifier;
    }

    public TerrainModifierCalculationType getTerrainModifierDenfenseModifierCalculationType() {
        return terrainData.overrideBaseTerrainDefModifier;
    }

    public TerrainModifierCalculationType getTerrainModifierMovementCostCalculationType() {
        return terrainData.overrideBaseTerrainMovementCosts;
    }

    public int getMovementCost(MovementType  movementType) {
        //Caching movementCost from terrainData
        if (movementCost == null) {
            movementCost = terrainData.movementCosts.ToDictionary(
                movementCost => movementCost.movementType,
                movementCost => movementCost.movementCost);
        }

        int retMovementCost = -1;
        movementCost.TryGetValue(movementType, out retMovementCost);
        return retMovementCost;
    }

    public int getDefenseModifier(DefenseType defenseType) {
        //Caching defenseModifier from terrainData
        if (defenseModifier == null) {
            defenseModifier = terrainData.defModifier.ToDictionary(
                defenseModifier => defenseModifier.defenseType,
                defenseModifier => defenseModifier.defenseModifier);
        }

        int retTefenseModifier = -1;
        defenseModifier.TryGetValue(defenseType, out retTefenseModifier);
        return retTefenseModifier;
    }
}
