using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class RandomWalkGenerator {
    private Vector2Int startPosition; //start position of the walk
    private int iterations; //number of iterations
    private int walkLength; //length of the walk
    private bool startRandomlyEachIteration; //start randomly each iteration
    private BoundsInt region; //region as boundary for the random walk

    //constuctor
    public RandomWalkGenerator(BoundsInt region, Vector2Int startPosition, int iterations, int walkLength, bool startRandomlyEachIteration) {
        this.region = region;
        this.startPosition = startPosition;
        this.iterations = iterations;
        this.walkLength = walkLength;
        this.startRandomlyEachIteration = startRandomlyEachIteration;
    }

    protected HashSet<Vector2Int> RunRandomWalk() {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++) {
            var path = SimpleRandomWalk(currentPosition, walkLength);
            //add path to floor positions
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration) {
                //select a random position from the floor positions
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }


    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength) {
        //represents our generated path
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        //add start position to our path
        path.Add(startPosition);

        var previousPosition = startPosition;
        //path generation
        for (int i = 0; i < walkLength; i++) {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    //list of all possible directions
     public static class Direction2D {
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>{
            new Vector2Int(0,1), //up direction
            new Vector2Int(1,0), //right direction
            new Vector2Int(0,-1), //down direction
            new Vector2Int(-1,0) //left direction
        };

        public static Vector2Int GetRandomCardinalDirection() {
            return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
        }
    }
}
