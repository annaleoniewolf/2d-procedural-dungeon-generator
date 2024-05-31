using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PurposeGenerator {

    //sets start position of the dungeon
    public static Vector2Int SetDungeonEntry(List<Vector2Int> roomCenters, int dungeonHorizontalSize) {

        Vector2Int dungeonEntry = roomCenters[Random.Range(0, roomCenters.Count)]; //randomly select a room center as the start position

        Vector2Int randomStartPosition = new Vector2Int(Random.Range(dungeonHorizontalSize, 0), 0); 

        //find the room center closest to the random start position
        foreach (Vector2Int roomCenter in roomCenters) {
            if (Vector2Int.Distance(roomCenter, randomStartPosition) < Vector2Int.Distance(dungeonEntry, randomStartPosition)) {
                dungeonEntry = roomCenter;
            }
        }

        return dungeonEntry; 
    }

    //sets exit position of the dungeon
    public static Vector2Int SetDungeonExit(List<Vector2Int> roomCenters, Vector2Int startposition) {

        Vector2Int dungeonExit = roomCenters[Random.Range(0, roomCenters.Count)]; //randomly select a room center as the exit position

        //find the room center farthest from the start position
        foreach (Vector2Int roomCenter in roomCenters) {
            if (Vector2Int.Distance(roomCenter, startposition) > Vector2Int.Distance(dungeonExit, startposition)) {
                dungeonExit = roomCenter;
            }
        }

        return dungeonExit;
    }

    //generate difficult level for each room
    public static Dictionary<Vector2Int, int> GenerateRoomCenterDifficulties(List<Vector2Int> roomCenters, int minDifficulty, int maxDifficulty, Vector2Int startPosition, Vector2Int exitPosition) {

        Dictionary<Vector2Int, int> roomDifficulty = new Dictionary<Vector2Int, int>(); //contains room center and its difficulty level

        List<Vector2Int> roomCentersCopy = new List<Vector2Int>(roomCenters); //copy of room centers

        //remove start and exit positions from room centers and assign them the minimum and maximum difficulty respectively
        if (!roomDifficulty.ContainsKey(startPosition)) {
            roomDifficulty.Add(startPosition, minDifficulty);
            roomCentersCopy.Remove(startPosition);
        }
        
        if (!roomDifficulty.ContainsKey(exitPosition)) {
            roomDifficulty.Add(exitPosition, maxDifficulty);
            roomCentersCopy.Remove(exitPosition);
        }

        //assign random difficulty to the rest of the rooms
        foreach (Vector2Int roomCenter in roomCentersCopy) {
            if (!roomDifficulty.ContainsKey(roomCenter)) {
                roomDifficulty.Add(roomCenter, Random.Range(minDifficulty, maxDifficulty));
            }
        }

        return roomDifficulty;
    }
}
