using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    [SerializeField]
    public Tilemap terrainBase;
    [SerializeField]
    public Tilemap terrainModifier;
    [SerializeField]
    public Tilemap unit;
    [SerializeField]
    private List<TerrainData> terrainDatas;

    private Dictionary<TileBase, Terrain> terrainDataLookupDict;

    private void Awake() {
        //Initialize TileBase to TileData lookup table
        terrainDataLookupDict = new Dictionary<TileBase, Terrain>();
        foreach (var terrainData in terrainDatas) {
            foreach (var tile in terrainData.tileBases) {
                terrainDataLookupDict.Add(tile, new Terrain(terrainData));
            }
        }
    }

    private void Update() {
        // This is all for testing
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = terrainBase.WorldToCell(mousePos);

            TileBase clickedTerrainBase = terrainBase.GetTile(gridPos);
            TileBase clickedTerrainModifier = terrainModifier.GetTile(gridPos);

            string nameToPrint = terrainDataLookupDict[clickedTerrainBase].getName();
            int movementCostToPrint = terrainDataLookupDict[clickedTerrainBase].getMovementCost(MovementType.Foot);

            if(clickedTerrainModifier != null) {
                switch (terrainDataLookupDict[clickedTerrainModifier].getTerrainModifierMovementCostCalculationType()) {
                    case TerrainModifierCalculationType.None:
                        break;
                    case TerrainModifierCalculationType.Add:
                        movementCostToPrint += terrainDataLookupDict[clickedTerrainModifier].getMovementCost(MovementType.Foot);
                        break;
                    case TerrainModifierCalculationType.Replace:
                        movementCostToPrint = terrainDataLookupDict[clickedTerrainModifier].getMovementCost(MovementType.Foot);
                        break;
                }
                nameToPrint = terrainDataLookupDict[clickedTerrainModifier].getName();
            }


            print("At pos " + gridPos + ", terrain base is " + nameToPrint + " with a foot movement cost of " + movementCostToPrint);
        }
    }
}
