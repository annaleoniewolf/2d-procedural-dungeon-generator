using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemieGenerator {
    
    //generate endboss
    public static Vector2Int GenerateEndBoss(Vector2Int exitPosition) {
        //endboss position is one down the exit position
        Vector2Int endBossPosition = new Vector2Int(exitPosition.x, exitPosition.y - 1);
        return endBossPosition; //
    }

    public static HashSet<Vector2Int> GenerateEnemies(Dictionary<Vector2Int, int> roomCenterDifficulties, int minDifficulty, int maxDifficulty, float enemySpawnProbability) {
        
        HashSet<Vector2Int> enemyPositions = new HashSet<Vector2Int>();

        //iterate though each room
        foreach (KeyValuePair<Vector2Int, int> roomCenterDifficulty in roomCenterDifficulties) {
            if(roomCenterDifficulty.Value == minDifficulty) { //no enemies should spawn in start room
                continue;
            } else if(roomCenterDifficulty.Value == maxDifficulty) { //endboss should spawn in end room
                continue;
            } else {
                ///generate enemies in the room with a certain probability
                if(Random.value < enemySpawnProbability) {
                    enemyPositions.Add(roomCenterDifficulty.Key);
                }
            }
        }

        return enemyPositions;
    }
}
