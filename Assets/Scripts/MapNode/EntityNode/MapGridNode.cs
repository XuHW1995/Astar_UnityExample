using UnityEngine;

public enum MapNodeType
{
    min = 0,
    flat = 1,
    barrier = 2,
    max,
}

public class MapGridNode: NodeBase
{
    public bool canWalk { get; set; }
    public Vector3 pos { get; }
    public GameObject gameObject { get; }
    public MapNodeType type { get; set; }
    
    public MapGridNode(int x, int y, Vector3 pos, GameObject obj)
    {
        coordx = x;
        coordy = y;
        this.pos = pos;
        gameObject = obj;
    }
}

