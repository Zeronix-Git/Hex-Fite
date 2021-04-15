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

        // Offsets for computing neighbours
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
        
        neighbourOffsets = new List<List<Vector3Int>>()
        {
            evenRowNeighbourOffsets, 
            oddRowNeighbourOffsets
        };
    }

    private Vector3Int convertOffsetToCube(Vector3Int offsetGridPos) {
        var col = offsetGridPos.y;
        var row = offsetGridPos.x;

        var x = col - (row - (row&1)) / 2;
        var z = row;
        var y = -x-z;
        var cubeGridPos = new Vector3Int(x, y, z);
        return cubeGridPos;
    }

    private Vector3Int convertCubeToOffset(Vector3Int cubeGridPos) {
        var x = cubeGridPos.x;
        var y = cubeGridPos.y;
        var z = cubeGridPos.z;
        
        var col = x + (z - (z&1)) / 2;
        var row = z;
        var offsetGridPos = new Vector3Int(row, col, 0);
        return offsetGridPos;
    }

    private List<Vector3Int> getNeighbour(Vector3Int offsetGridPos) {

        var neighbourPositions = new List<Vector3Int>();
        var rowParity = offsetGridPos.y % 2; // Unity uses y-coordinate to store row  
        foreach (var offset in neighbourOffsets[rowParity]) {
            neighbourPositions.Add(offsetGridPos + offset);
        }
        return neighbourPositions;
    }

    private int getDistance(Vector3Int offsetStartPos, Vector3Int offsetEndPos) {
        var cubeStartPos = convertOffsetToCube(offsetStartPos);
        var cubeEndPos = convertOffsetToCube(offsetEndPos);
        return Mathf.Max(
            Mathf.Abs(cubeStartPos.x - cubeEndPos.x),
            Mathf.Abs(cubeStartPos.y - cubeEndPos.y),
            Mathf.Abs(cubeStartPos.z - cubeEndPos.z)
        );
    }

    private void getMovementRange(Vector3Int gridPos, MovementType movementType) {
        
    }

    private void getPath() {

    }

    private void Update() {
        // This is all for testing
        var prevGridPos = new Vector3Int(0,0,0);
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


            // print("At pos " + gridPos + ", terrain base is " + nameToPrint + " with a foot movement cost of " + movementCostToPrint);
            print("At pos " + gridPos + ", cube coordinate is " + convertCubeToOffset(gridPos));
            print("Distance to previous tile is " + getDistance(gridPos, prevGridPos));
            prevGridPos = gridPos;
        }
    }
}
