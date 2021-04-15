using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    [SerializeField]
    private int mapSizeX;
    [SerializeField]
    private int mapSizeY;
    [SerializeField]
    private Tilemap terrainBase;
    [SerializeField]
    private Tilemap terrainModifier;
    [SerializeField]
    private Tilemap unit;
    [SerializeField]
    private List<TerrainData> terrainDatas;
    [SerializeField]
    private List<UnitData> unitDatas;

    private Dictionary<Vector3Int, Unit> unitsOnTheMap;

    private Dictionary<TileBase, Terrain> terrainDataLookupDict;
    private Dictionary<TileBase, UnitData> unitDataLookupDict;
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

        unitDataLookupDict = new Dictionary<TileBase, UnitData>();
        foreach (var unitData in unitDatas) {
            foreach (var tile in unitData.tilesBases) {
                unitDataLookupDict.Add(tile, unitData);
            }
        }

        unitsOnTheMap = new Dictionary<Vector3Int, Unit>();
        //TileBase[] units = unit.GetTilesBlock(unit.cellBounds);
        for (int i = unit.origin.x; i < unit.size.x;  i++) {
            for (int j = unit.origin.y; j < unit.size.y; j++) {
                Vector3Int pos = new Vector3Int(i, j, 0);
                TileBase actualUnit = unit.GetTile(pos);
                if (actualUnit != null) {
                    UnitData data;
                    unitDataLookupDict.TryGetValue(actualUnit, out data);
                    unitsOnTheMap.Add(pos, new Unit(data, pos));
                }
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

        neighbourOffsets = new List<List<Vector3Int>>()
        {
            evenRowNeighbourOffsets, 
            oddRowNeighbourOffsets
        };
    }

    private List<Vector3Int> getNeighbour(Vector3Int gridPos) {

        var neighbourPositions = new List<Vector3Int>();
        var rowParity = Mathf.Abs(gridPos[1] % 2); // Unity uses second coordinate to store row  
        print(neighbourOffsets);
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
            TileBase clickedUnit = unit.GetTile(gridPos);

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

            if (clickedUnit != null) {
                Unit actualUnit;
                unitsOnTheMap.TryGetValue(gridPos, out actualUnit);
                print("At pos " + gridPos + ", the unit is " + actualUnit.getName() + ", and their id is "  + actualUnit.getUnitId());
            }

            //print("At pos " + gridPos + ", terrain base is " + nameToPrint + " with a foot movement cost of " + movementCostToPrint);
            //print("At pos " + gridPos + ", neighbours are ");
            //foreach (var neighbourPos in getNeighbour(gridPos)) {
            //    print(neighbourPos);
            //    terrainBase.SetTile(neighbourPos, terrainDatas[0].tileBases[0]);
            //}
        }
    }
}
