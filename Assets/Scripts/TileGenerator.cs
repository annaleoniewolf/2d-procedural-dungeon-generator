using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//generates tiles
public class TileGenerator : MonoBehaviour {
    //floor tiles
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase[] floorTiles;
    //enemie tiles
    [SerializeField]
    private Tilemap enemiesTilemap;
    [SerializeField]
    private TileBase[] enemiesTiles;
    //chest tiles
    [SerializeField]
    private Tilemap chestTilemap;
    [SerializeField]
    private TileBase[] chestTiles;
    //trap tiles
    [SerializeField]
    private Tilemap trapTilemap;
    [SerializeField]
    private TileBase[] trapTiles;
    //exit tiles
    [SerializeField]
    private Tilemap exitTilemap;
    [SerializeField]
    private TileBase[] exitTiles;
    //player tiles
    [SerializeField]
    private Tilemap playerTilemap;
    [SerializeField]
    private TileBase[] playerTiles;
    //end boss tiles
    [SerializeField]
    private Tilemap endBossTilemap;
    [SerializeField]
    private TileBase[] endBossTiles;
    //decorative tiles
    [SerializeField]
    private Tilemap decorativeTilemap;
    [SerializeField]
    private TileBase[] decorativeTiles;

    public void GenerateFloorTiles(IEnumerable<Vector2Int> floorPositions) { //method, that paints floor tiles on tilemap
        GenerateTiles(floorPositions, floorTilemap, floorTiles);
    }
    public void GenerateEnemyTiles(IEnumerable<Vector2Int> enemyPositions) { //method, that paints enemy tiles on tilemap
        GenerateTiles(enemyPositions, enemiesTilemap, enemiesTiles);
    }
    public void GenerateChestTile(Vector2Int chestPosition) { //method, that paints chest tiles on tilemap
        GenerateSingleTile(chestTilemap, chestTiles, chestPosition);
    }
    public void GenerateTrapTiles(IEnumerable<Vector2Int> trapPositions) { //method, that paints trap tiles on tilemap
        GenerateTiles(trapPositions, trapTilemap, trapTiles);
    }
    public void GenerateExitTile(Vector2Int exitPositions) { //method, that paints exit tiles on tilemap
        GenerateSingleTile(exitTilemap, exitTiles, exitPositions);
    }
    public void GeneratePlayerTile(Vector2Int playerPosition) { //method, that paints player tiles on tilemap
        GenerateSingleTile(playerTilemap, playerTiles, playerPosition);
    }
    public void GenerateEndBossTile(Vector2Int endBossPosition) { //method, that paints end boss tiles on tilemap
        GenerateSingleTile(endBossTilemap, endBossTiles, endBossPosition);
    }
    public void GenerateDecorativeTiles(IEnumerable<Vector2Int> decorativePositions) { //method, that paints decorative tiles on tilemap
        GenerateTiles(decorativePositions, decorativeTilemap, decorativeTiles);
    }

    public void ClearTiles() {     //clears all tiles
        floorTilemap.ClearAllTiles(); //clears floor tilemap
        enemiesTilemap.ClearAllTiles(); //clears enemies tilemap
        chestTilemap.ClearAllTiles(); //clears chest tilemap
        trapTilemap.ClearAllTiles(); //clears trap tilemap
        exitTilemap.ClearAllTiles(); //clears exit tilemap
        playerTilemap.ClearAllTiles(); //clears player tilemap
        endBossTilemap.ClearAllTiles(); //clears end boss tilemap
        decorativeTilemap.ClearAllTiles(); //clears decorative tilemap
    }

    private void GenerateTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase[] tiles) {
        foreach (var position in positions) {
            GenerateSingleTile(tilemap, tiles, position);
        }
    }
    private void GenerateSingleTile(Tilemap tilemap, TileBase[] tiles, Vector2Int position) {
        //converts word position to cell position
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);

        var tile = tiles[Random.Range(0, tiles.Length)];  //paint random tile on tilePosition
        tilemap.SetTile(tilePosition, tile);
    }
}

