using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectFurnisher {
    //generates endboss chest near the exit
    public static Vector2Int GenerateEndbossChestPosition(Vector2Int exitPosition) {
        //chest position is one down the exit position
        Vector2Int chestPosition = new Vector2Int(exitPosition.x -1, exitPosition.y);
        return chestPosition;
    }

    public static HashSet<Vector2Int> GenerateObjectPositions(HashSet<Vector2Int> emptyFloorPositions, List<Vector2Int> roomCenters, int objectSRWIterationPerRoom, float objectSpawnProbability) {
        HashSet<Vector2Int> objectPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialObjectPositions = new HashSet<Vector2Int>(emptyFloorPositions);

        foreach (Vector2Int roomCenter in roomCenters) {

            //generate trap in the room with a certain probability
            for (int i = 0; i < objectSRWIterationPerRoom; i++) {
                if (Random.value < objectSpawnProbability) {
                    var trapPosition = GenerateSRWObjectPosition(roomCenter, 5); //generate trap position using simple random walk
                    if(potentialObjectPositions.Contains(trapPosition)) { //check if the trap position is in the empty floor positions
                        objectPositions.Add(trapPosition);
                        potentialObjectPositions.Remove(trapPosition);
                    }
                }
            }
        }

        return objectPositions;
    } 

    //generates random object position using simple random walk
    public static Vector2Int GenerateSRWObjectPosition(Vector2Int startPosition, int walkLength) {
        //represents our generated path
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        //add start position to our path
        path.Add(startPosition);

        var previousPosition = startPosition;
        //path generation
        for (int i = 0; i < walkLength; i++) {
            var newPosition = previousPosition + RandomWalkGenerator.Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return previousPosition; // last position as spawn position
    }
}
