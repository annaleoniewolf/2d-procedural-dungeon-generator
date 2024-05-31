using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGenerator {
    public static HashSet<Vector2Int> GenerateCorridors(List<Vector2Int> roomCenters) {

        List<Vector2Int> roomCentersCopy = new List<Vector2Int>(roomCenters); //copy roomCenters to avoid modifying the original list

        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>(); //hashset to store the corridor positions
        var currentRoomCenter = roomCentersCopy[Random.Range(0, roomCentersCopy.Count)]; //start with random room center
        roomCentersCopy.Remove(currentRoomCenter); //remove the current room center from the list

        while (roomCentersCopy.Count > 0) {
            Vector2Int closestCenter = FindClosestRoomCenter(currentRoomCenter, roomCentersCopy); //find the room center that is closest to the current room center
            roomCentersCopy.Remove(closestCenter); //remove the closest room center from the list
            HashSet<Vector2Int> newCorridor = GenerateCorridor(currentRoomCenter, closestCenter); //generate a corridor between the current room center and the closest room center
            currentRoomCenter = closestCenter; //update the current room center
            corridors.UnionWith(newCorridor); //add the new corridor to the set of corridors
        }

        return corridors;
    }
    public static Vector2Int FindClosestRoomCenter(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters) {
        Vector2Int closestCenter = Vector2Int.zero;
        float minDistance = float.MaxValue;

        foreach (var center in roomCenters) {
            float distance = Vector2Int.Distance(center, currentRoomCenter);
            if (distance < minDistance) {
                minDistance = distance;
                closestCenter = center;
            }
        }
        return closestCenter;
    }
    public static HashSet<Vector2Int> GenerateCorridor(Vector2Int currentRoomCenter, Vector2Int closestCenter) {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter; //start position of the corridor
        corridor.Add(position);
        while (position.y != closestCenter.y) {
            if (closestCenter.y > position.y) {
                position += Vector2Int.up; //go up
            } else if (closestCenter.y < position.y) {
                position += Vector2Int.down; //go down
            }
            corridor.Add(position);
        }
        while (position.x != closestCenter.x) {
            if (closestCenter.x > position.x) {
                position += Vector2Int.right; //go right
            } else if (closestCenter.x < position.x) {
                position += Vector2Int.left; //go left
            }
            corridor.Add(position);
        }
        return corridor;
    }
}
