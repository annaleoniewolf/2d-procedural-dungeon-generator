using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {
    //returns empty floor positions
    public static HashSet<Vector2Int> GetEmptyFloorPositions(HashSet<Vector2Int> floor, HashSet<Vector2Int> corridors, Vector2Int exitPosition, Vector2Int chestPosition, Vector2Int endbossPosition, HashSet<Vector2Int> enemies, HashSet<Vector2Int> objects) {
        HashSet<Vector2Int> emptyFloorPositions = new HashSet<Vector2Int>(floor);
        emptyFloorPositions.Remove(exitPosition);
        foreach (Vector2Int corridor in corridors) {
            emptyFloorPositions.Remove(corridor);
        }
    return emptyFloorPositions;
    }
}
