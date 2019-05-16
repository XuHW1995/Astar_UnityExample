using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public delegate void AstarResult(List<AstarNode> findResult);

//路径搜索
public static class PathFindMgr
{
    //请求Astar寻路
    public static void RequestPathFind(MapGridNode beginNode, MapGridNode endNode, MapInfo mapInfo, AstarResult callback, Object targetObj = null)
    {
        //TODO 通过策略，决定用什么寻路方法
        //此处用Astar
        List<AstarNode> astarPathFindNodeList = CreatAstarPathFindNodeList(mapInfo);
        AstarPathFinder pathFinder = new AstarPathFinder(beginNode, endNode, mapInfo, astarPathFindNodeList, callback);

        pathFinder.FindPath();
    }

    //根据策略，生成对应地图的寻路信息
    private static List<AstarNode> CreatAstarPathFindNodeList(MapInfo mapInfo)
    {
        List<AstarNode> astarPathFindNodeList = new List<AstarNode>();
        for (int i = 0;i < mapInfo.nodeCount; i++)
        {
            AstarNode pathFindNode = new AstarNode();

            pathFindNode.mapGridNode = mapInfo.GetNodeByIndex(i);

            mapInfo.SetPathFindNode(i, pathFindNode);
            //mapGridNode.pathFindNode = pathFindNode;

            astarPathFindNodeList.Add(pathFindNode);
        }

        return astarPathFindNodeList;
    }
    // todo 其他寻路策略


}

