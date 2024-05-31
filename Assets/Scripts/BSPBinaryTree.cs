using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPBinaryTree{
    private BSPTreeNode root; //root of bsp tree

    /* public BSPTreeNode GenerateBSPTree(BoundsInt region) {
        root = new BSPTreeNode(region);
    }

    public void SplitRegion(bool horizontal, int splitValue)
    {
        SplitRegionRecursive(root, horizontal, splitValue);
    }

    private void SplitRegionRecursive(BSPTreeNode node, bool horizontal, int splitValue)
    {
        if (node == null)
            return;

        if (horizontal)
        {
            node.Left = new BSPTreeNode(new BoundsInt(node.Region.Min, splitValue));
            node.Right = new BSPTreeNode(new BoundsInt(splitValue, node.Region.Max));
        }
        else
        {
            node.Left = new BSPTreeNode(new BoundsInt(node.Region.Min, node.Region.Max));
            node.Right = new BSPTreeNode(new BoundsInt(node.Region.Min, node.Region.Max));
        }

        SplitRegionRecursive(node.Left, !horizontal, splitValue);
        SplitRegionRecursive(node.Right, !horizontal, splitValue);
    }*/

    
}