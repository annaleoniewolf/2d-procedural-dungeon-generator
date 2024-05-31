using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TopDownEvaluation {
   //method that evaluates the density of the dungeon
    public static float EvaluateDensity(HashSet<Vector2Int> dungeonFloor, int dungeonWidth, int dungeonHeight) {
        float density = (float)dungeonFloor.Count / (dungeonWidth * dungeonHeight);
        return density;
    }
    //method that evaluates how many rooms are in the dungeon
    public static int CountRooms(List<BoundsInt> regions) {
        return regions.Count;
    }
}

