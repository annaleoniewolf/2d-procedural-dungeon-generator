using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//splits a given grid into regions
public class RegionGenerator {

    //generates regions of the dungeon
    public static List<BoundsInt> GenerateRegions(BoundsInt spaceToSplit, int minWidth, int minHeight) {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        //while we have rooms left to split perform BSP
        while(roomsQueue.Count > 0 ) {
            var room = roomsQueue.Dequeue();
            //discard rooms that are too small room.size.y is the height of the room, room.size.x is the width of the room
            if(room.size.y >= minHeight && room.size.x >= minWidth) { //condition means space it big enough to be split into rooms
                if(Random.value < 0.5f) { //random number that determins if we check to split horizontally or vertically first
                    if (room.size.y >= minHeight * 2) { //determines if the room is tall enough to be split horizontally
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else if(room.size.x >= minWidth * 2) { //determines if the room is wide enough to be split vertically
                        SplitVertically(minWidth, roomsQueue, room);
                    } else if(room.size.x >= minWidth && room.size.y >= minHeight) { //if the generated region is too small to be split, add it to the list of rooms
                        roomsList.Add(room);
                    }
                } else {
                    if(room.size.x >= minWidth * 2) { //determines if the room is wide enough to be split vertically
                        SplitVertically(minWidth, roomsQueue, room);
                    } else if (room.size.y >= minHeight * 2) { //determines if the room is tall enough to be split horizontally
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else if(room.size.x >= minWidth && room.size.y >= minHeight) { //if the generated region is too small to be split, add it to the list of rooms
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        var ySlit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySlit, room.size.z)); //min is the bottom left corner of the room
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySlit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySlit, room.size.z));
        //add the two new rooms to the queue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        //add the two new rooms to the queue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    // Find the region that contains a given cell
    private BoundsInt? FindRegionForCell(GridCell cell, IEnumerable<BoundsInt> regions) {
    foreach (BoundsInt region in regions) {
        if (region.Contains(new Vector3Int(cell.coordinates.x, cell.coordinates.y, 0))) {
            return region;
        }
    }
    return null;
}

    //helper function to print regions
    private void PrintRegions(Dictionary<BoundsInt, int> regions) {
        foreach (KeyValuePair<BoundsInt, int> region in regions) {
            Debug.Log("Region " + region.Value + ": " + region.Key);
        }
    }

    //new version
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        //while we have rooms left to split perform BSP
        while(roomsQueue.Count > 0 ) {
            var room = roomsQueue.Dequeue();
            //discard rooms that are too small room.size.y is the height of the room, room.size.x is the width of the room
            if(room.size.y >= minHeight && room.size.x >= minWidth) { //condition means space it big enough to be split into rooms
                if(Random.value < 0.5f) { //random number that determins if we check to split horizontally or vertically first
                    if (room.size.y >= minHeight * 2) { //determines if the room is tall enough to be split horizontally
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else if(room.size.x >= minWidth * 2) { //determines if the room is wide enough to be split vertically
                        SplitVertically(minWidth, roomsQueue, room);
                    } else if(room.size.x >= minWidth && room.size.y >= minHeight) { //if the generated region is too small to be split, add it to the list of rooms
                        roomsList.Add(room);
                    }
                } else {
                    if(room.size.x >= minWidth * 2) { //determines if the room is wide enough to be split vertically
                        SplitVertically(minWidth, roomsQueue, room);
                    } else if (room.size.y >= minHeight * 2) { //determines if the room is tall enough to be split horizontally
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else if(room.size.x >= minWidth && room.size.y >= minHeight) { //if the generated region is too small to be split, add it to the list of rooms
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

}