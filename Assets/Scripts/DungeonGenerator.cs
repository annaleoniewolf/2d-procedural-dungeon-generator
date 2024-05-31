using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

//generates 2d procedural dungeon
public class DungeonGenerator : MonoBehaviour {

    //dungeon grid variables
    [SerializeField]
    public int dungeonVerticalSize = 20; // height of the dungeon grid
    [SerializeField]
    public int dungeonHorizontalSize = 20; // width of the dungeon grid
    [SerializeField]
    private Vector2Int startPosition = new Vector2Int(0, 0); //start position of the dungeon
    [SerializeField]
    private int minRoomWidth = 4; //minimum width of a room
    [SerializeField]
    private int minRoomHeight = 4; //minimum height of a room
    [SerializeField]
    [Range(1, 10)]
    private int roomOffset = 1; //offset for room generation
    public int minDifficulty; // minimum difficulty of the dungeon
    [SerializeField]
    public int maxDifficulty; // maximum difficulty of the dungeon
    [SerializeField]
    public float enemySpawnProbability; // probability of enemy spawn in a room
    [SerializeField]
    public int objectSRWIterationPerRoom; // number of simple random walk iterations for trap generation
    [SerializeField]
    public float objectSpawnProbability; // probability of object spawn in a room
    [SerializeField]
    private TileGenerator tileGenerator; //tile generator object
    [SerializeField]
    public int numberOfEvaluationInstances = 1000; //number of evaluation instances


    protected List<BoundsInt> regions = new List<BoundsInt>();  //dictionary to store the regions and their id
    protected List<Vector2Int> roomCenters = new List<Vector2Int>(); //list to store the center of each room
    protected Vector2Int playerSpawnPosition; //position where the player will spawn
    protected Vector2Int exitPosition; //position of the exit
    protected Vector2Int chestPosition; //position of the chest
    protected Vector2Int endbossPosition; //position of the endboss
    protected HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); //hashset to store the floor positions
    protected HashSet<Vector2Int> corridors = new HashSet<Vector2Int>(); //hashset to store the corridor positions
    protected Dictionary<Vector2Int, int> roomCenterDifficulties= new Dictionary<Vector2Int, int>(); //dictionary to store the room centers and their difficulty level
    protected HashSet<Vector2Int> emptyFloorPositions; //hashset to store the empty floor positions

    protected string evaluationResultsFilePath; //file path to store the evaluation results
    protected string timeTakenFilePath; //file path to store the time taken to generate dungeons

    public void GenerateDungeon() {

        Initialize(); //initialize the dungeon
        Stopwatch stopwatch = Stopwatch.StartNew();

        //DUNGEON-LAYOUT GENERATION

        //Step 1: Generate Regions with BSP
        regions = RegionGenerator.GenerateRegions(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonHorizontalSize, dungeonVerticalSize, 0)), minRoomWidth, minRoomHeight);

        //Step 2: Generate Rooms in each region
        floor = RoomGenerator.GenerateRooms(regions, roomOffset);
        roomCenters = RoomGenerator.CollectRoomCenterPoints(regions);
        emptyFloorPositions = new HashSet<Vector2Int>(floor);//copy floor to emptyFloorPositions


        //Step 3: Generate Corridors between rooms
        corridors = CorridorGenerator.GenerateCorridors(roomCenters);
        foreach (Vector2Int corridors in corridors) { //remove corridors from emptyFloorPositions
            emptyFloorPositions.Remove(corridors);
        }

        //Step 4: Generate Layout-Tiles
        floor.UnionWith(corridors);
        tileGenerator.GenerateFloorTiles(floor);
        

        //GAME CONTENT FURNISHER

        //Step 1: Generate Enter and Exit Positions
        playerSpawnPosition = PurposeGenerator.SetDungeonEntry(roomCenters, dungeonHorizontalSize);
        emptyFloorPositions.Remove(playerSpawnPosition);
        tileGenerator.GeneratePlayerTile(playerSpawnPosition);
        exitPosition = PurposeGenerator.SetDungeonExit(roomCenters, playerSpawnPosition);
        emptyFloorPositions.Remove(exitPosition);
        tileGenerator.GenerateExitTile(exitPosition);

        //Step 2: Generate Room Difficulties
        roomCenterDifficulties = PurposeGenerator.GenerateRoomCenterDifficulties(roomCenters, minDifficulty, maxDifficulty, playerSpawnPosition, exitPosition);
        foreach (var roomCenterDifficulty in roomCenterDifficulties) {
            Debug.Log("Room Center: " + roomCenterDifficulty.Key + " Difficulty: " + roomCenterDifficulty.Value);
        }

        //Step 3: Generate Endboss and Endroom Chest Positions
        endbossPosition = EnemieGenerator.GenerateEndBoss(exitPosition);
        emptyFloorPositions.Remove(endbossPosition);
        tileGenerator.GenerateEndBossTile(endbossPosition);

        chestPosition = ObjectFurnisher.GenerateEndbossChestPosition(exitPosition);
        emptyFloorPositions.Remove(chestPosition);
        tileGenerator.GenerateChestTile(chestPosition);

        //Step 4: Generate Enemies
        HashSet<Vector2Int> enemies = EnemieGenerator.GenerateEnemies(roomCenterDifficulties, minDifficulty, maxDifficulty, enemySpawnProbability);
        foreach (Vector2Int enemy in enemies) {
            emptyFloorPositions.Remove(enemy);
        }
        tileGenerator.GenerateEnemyTiles(enemies);

        //Step 5: Furnish Dungeon with traps and decorative objects using SRW Algorithm
        HashSet<Vector2Int> trapPositions = ObjectFurnisher.GenerateObjectPositions(emptyFloorPositions, roomCenters, objectSRWIterationPerRoom, objectSpawnProbability);
        foreach (Vector2Int trapPosition in trapPositions) {
            emptyFloorPositions.Remove(trapPosition);
        }
        tileGenerator.GenerateTrapTiles(trapPositions);
        HashSet<Vector2Int> decorationPositions = ObjectFurnisher.GenerateObjectPositions(emptyFloorPositions, roomCenters, objectSRWIterationPerRoom, objectSpawnProbability);
        foreach (Vector2Int decorationPosition in decorationPositions) {
            emptyFloorPositions.Remove(decorationPosition);
        }
        tileGenerator.GenerateDecorativeTiles(decorationPositions);

        //Step 6: Evaluate Dungeon
        // Get the time taken to generate the dungeon
       // stopwatch.Stop();
        //float timeTaken = (float)stopwatch.Elapsed.TotalSeconds;
        //EvaluateDungeon(timeTaken);
    }

    //evaluate for 1000 dungeon instances
   /* void Start() {
        // Check if the file exists and delete it
        if (File.Exists(evaluationResultsFilePath)) {
            File.Delete(evaluationResultsFilePath);
        }
        if (File.Exists(timeTakenFilePath)) {
            File.Delete(timeTakenFilePath);
        }
        //top down evaluation results
        evaluationResultsFilePath = $"Assets/Ressources/Results_{dungeonVerticalSize}x{dungeonHorizontalSize}_{numberOfEvaluationInstances}.csv";
        HandleEvaluationFile.WriteHeaderToCSV(evaluationResultsFilePath, "Density,RoomCount");
        //time taken to generate dungeons
        timeTakenFilePath = $"Assets/Ressources/TimeTaken_{dungeonVerticalSize}x{dungeonHorizontalSize}_{numberOfEvaluationInstances}.csv";
        HandleEvaluationFile.WriteHeaderToCSV(timeTakenFilePath, "TimeTaken");

        for (int i = 0; i <= numberOfEvaluationInstances; i++) {
            GenerateDungeon();
            Debug.Log("Dungeon Generated: " + i + "/" + numberOfEvaluationInstances);
        }

    }*/

    private void EvaluateDungeon(float timeTaken) {
        //DUNGEON EVALUATION
        float density = TopDownEvaluation.EvaluateDensity(floor, dungeonHorizontalSize, dungeonVerticalSize);
        int roomCount = TopDownEvaluation.CountRooms(regions);

        //WRITE RESULTS TO CSV
        HandleEvaluationFile.WriteEvaluationResultsToCSV(evaluationResultsFilePath, density, roomCount);
        HandleEvaluationFile.WriteTimeTakenResultsToCSV(timeTakenFilePath, timeTaken);
    }

    protected void Initialize() {
        tileGenerator.ClearTiles();
        regions.Clear(); //clear regions
        roomCenters.Clear(); //clear room centers
        floor.Clear(); //clear floor
        corridors.Clear(); //clear corridors
        roomCenterDifficulties.Clear(); //clear room center difficulties
        emptyFloorPositions = new HashSet<Vector2Int>(); //clear empty floor positions
    }
}
