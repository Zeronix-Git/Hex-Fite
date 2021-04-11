using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    [SerializeField]
    private Tilemap terrainBase;
    [SerializeField]
    private Tilemap terrainModifier;
    [SerializeField]
    private Tilemap unit;
    [SerializeField]
    private List<TerrainData> terrainDatas;

    private Dictionary<TileBase, Terrain> terrainDataLookupDict;
    // Unity.Tilemap uses offset coordinates for hexagonal grid
    // Reference: https://www.redblobgames.com/grids/hexagons/
    private List<List<Vector3Int>> neighbourOffsets;

    private void Awake() {
        //Initialize TileBase to TileData lookup table
        terrainDataLookupDict = new Dictionary<TileBase, Terrain>();
        foreach (var terrainData in terrainDatas) {
            foreach (var tile in terrainData.tileBases) {
                terrainDataLookupDict.Add(tile, new Terrain(terrainData));
            }
        }

        var evenRowNeighbourOffsets = new List<Vector3Int>();
        evenRowNeighbourOffsets.Add(new Vector3Int(1,0,0));
        evenRowNeighbourOffsets.Add(new Vector3Int(-1,0,0));
        evenRowNeighbourOffsets.Add(new Vector3Int(0,1,0));
        evenRowNeighbourOffsets.Add(new Vector3Int(0,-1,0));
        evenRowNeighbourOffsets.Add(new Vector3Int(-1,1,0));
        evenRowNeighbourOffsets.Add(new Vector3Int(-1,-1,0));

        var oddRowNeighbourOffsets = new List<Vector3Int>();
        oddRowNeighbourOffsets.Add(new Vector3Int(1,0,0));
        oddRowNeighbourOffsets.Add(new Vector3Int(-1,0,0));
        oddRowNeighbourOffsets.Add(new Vector3Int(0,1,0));
        oddRowNeighbourOffsets.Add(new Vector3Int(0,-1,0));
        oddRowNeighbourOffsets.Add(new Vector3Int(1,1,0));
        oddRowNeighbourOffsets.Add(new Vector3Int(1,-1,0));
        
        var neighbourOffsets = new List<List<Vector3Int>>()
        {
            evenRowNeighbourOffsets, 
            oddRowNeighbourOffsets
        };
    }

    private List<Vector3Int> getNeighbour(Vector3Int gridPos) {

        var neighbourPositions = new List<Vector3Int>();
        var rowParity = gridPos[1] % 2; // Unity uses second coordinate to store row  
        foreach (var offset in neighbourOffsets[rowParity]) {
            neighbourPositions.Add(gridPos + offset);
        }
        return neighbourPositions;
    }

    private int getDistance(Vector3Int start, Vector3Int end) {
        return 0;
    }

    private void getMovementRange(Vector3Int gridPos, MovementType movementType) {
        
    }

    private void getPath() {

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
            //print("At pos " + gridPos + ", neighbours are ");
            //foreach (var neighbourPos in getNeighbour(gridPos)) {
            //    print(neighbourPos);
            //}
        }
    }
}
