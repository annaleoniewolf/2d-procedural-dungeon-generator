using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that represents a cell in the dungeon grid
public class GridCell {
    public Vector2Int coordinates; //coordinates of the cell
    public int region; //bsp region of the cell
    public TileType tileType; //tile type of the cell

    //enum that contains all possible tile types
    public enum TileType {
        Wall,
        Floor, 
        Fill
        // Add more tile types as needed
    }

    public GridCell(int x, int y, int region, TileType tileType) {
        this.coordinates = new Vector2Int(x, y);
        this.region = region;
        this.tileType = tileType;
    }
}