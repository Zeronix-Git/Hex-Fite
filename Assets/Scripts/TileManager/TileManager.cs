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

            print("At pos " + gridPos + ", terrain base is " + terrainDataLookupDict[clickedTerrainBase].getName() + " with a foot " +
                "movement cost of " + terrainDataLookupDict[clickedTerrainBase].getMovementCost(MovementType.Foot));
        }
    }
}
