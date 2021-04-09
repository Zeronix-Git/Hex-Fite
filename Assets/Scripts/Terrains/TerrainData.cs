using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newTerrainData", menuName ="Terrain/Terrain Data")]
public class TerrainData : ScriptableObject {
    public new string name;
    public List<DefenseModifier> defModifier;
    public List<MovementCost> movementCosts;
    public int income;
    public TileBase[] tileBases;
    public bool isTerranModifier;
    public TerrainModifierCalculationType overrideBaseTerrainDefModifier;
    public TerrainModifierCalculationType overrideBaseTerrainMovementCosts;
}
