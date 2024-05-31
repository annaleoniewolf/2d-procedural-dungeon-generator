using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represent a node in a BSP tree
public class BSPTreeNode{
    public BoundsInt Region { get; set; }
    public BSPTreeNode Left { get; set; }
    public BSPTreeNode Right { get; set; }

    public BSPTreeNode(BoundsInt region) {
        Region = region;
        Left = null;
        Right = null;
    }
}