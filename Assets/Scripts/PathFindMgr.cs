using System.Collections.Generic;
using UnityEngine;

public delegate void AstarResult(List<AstarNode> findResult);

//路径搜索
public static class PathFindMgr
{
    //请求Astar寻路
    public static void RequestPathFind(MapGridNode beginNode, MapGridNode endNode, MapInfo mapInfo, AstarResult callback, Object targetObj = null)
    {
        //TODO 通过策略，决定用什么寻路方法,此处用Astar
        AstarPathFinder pathFinder = new AstarPathFinder(beginNode, endNode, mapInfo, callback);
        pathFinder.FindPath();
    }
}

