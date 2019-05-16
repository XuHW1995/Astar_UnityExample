﻿using UnityEngine;

public enum MapNodeType
{
    min = 0,
    flat = 1,
    barrier = 2,
    max,
}

public class MapGridNode
{
    public int coordx { get; }
    public int coordy { get; }

    public bool canWalk { get; set; }
    public Vector3 pos { get; }
    public GameObject gameObject { get; }
    public MapNodeType type { get; set; }
    //实体节点里包含了寻路节点
    public AstarNode pathFindNode { get; set; } = new AstarNode(); 

    public MapGridNode(int x, int y, Vector3 pos, GameObject obj)
    {
        coordx = x;
        coordy = y;
        this.pos = pos;
        gameObject = obj;
    }
}
