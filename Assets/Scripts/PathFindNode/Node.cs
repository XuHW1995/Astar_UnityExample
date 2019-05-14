using UnityEngine;

public class Node
{
    public enum NodeType
    {
        ground,
        air
    }

    public int x;
    public int y;
    public int z;

    public float hCost;
    public float gCost;
    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node parentNode;
    public bool isWalkable = true;
    public GameObject worldObject;
    public NodeType nodeType;
}

