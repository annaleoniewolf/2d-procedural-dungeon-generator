using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator {

    //generates rooms in the dungeon
    //TODO: save roompositions on the grid
    public static HashSet<Vector2Int> GenerateRooms(List<BoundsInt> roomsList, int roomOffset) {
        //contains all floor positions of the rooms in a hashset
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); //TODO: save each room individually to be able to add content to it later
        foreach (var room in roomsList) {
            for (int col = roomOffset; col < room.size.x - roomOffset; col++) {
                for (int row = roomOffset; row < room.size.y - roomOffset; row++) {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    //collects the center points of each room
    public static List<Vector2Int> CollectRoomCenterPoints(List<BoundsInt> roomsList) {
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList) {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        return roomCenters;
    }
}
