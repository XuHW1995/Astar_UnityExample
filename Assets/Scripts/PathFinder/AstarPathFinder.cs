using System;
using System.Collections.Generic;

public class AstarPathFinder
{
    private AstarNode beginNode;
    private AstarNode endNode;
    private MapInfo findMapInfo;
    private AstarResult findResultCallback;
    //目标地图的寻路信息
    private AstarNode[,] targetMapPathFindData;
    //寻路结果信息
    private List<AstarNode> foundPath = new List<AstarNode>(); 

    public AstarPathFinder(MapGridNode begin, MapGridNode end, MapInfo mapInfo, AstarResult callback)
    {
        findMapInfo = mapInfo;
        targetMapPathFindData = CreatPathFindData(mapInfo);
        beginNode = targetMapPathFindData[begin.coordx, begin.coordy];
        endNode = targetMapPathFindData[end.coordx, end.coordy];
        findResultCallback = callback;
    }
    public void FindPath()
    {        
        //要检索的点
        List<AstarNode> openList = new List<AstarNode>();
        //检索过的点
        HashSet<AstarNode> closedList = new HashSet<AstarNode>();

        //加入起始点
        openList.Add(beginNode as AstarNode);
        //只要检索列表有数据,就进行检索
        while (openList.Count > 0)
        {
            //每次取第一个进行检查
            AstarNode currentNode = openList[0];
            //遍历待检查列表，选取F最小的点
            for (int i = 0; i < openList.Count; i++)
            {
                AstarNode checkNode = openList[i];

                if (checkNode.F < currentNode.F|| (checkNode.F == currentNode.F && checkNode.H < currentNode.H))
                {
                    if (!currentNode.Equals(checkNode))
                    {
                        currentNode = checkNode;
                    }
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //如果当前检测点是终点，那么路径搜索完毕
            if (currentNode.Equals(endNode))
            {
                foundPath = RecallPath(beginNode, currentNode);
                break;
            }

            //检测当前节点的相邻节点
            //获取当前检测节点的相邻节点（剔除不可行走节点）
            List<AstarNode> currentNodeNeighbors = GetNeighborNodes(currentNode);
            foreach (AstarNode neighborNode in currentNodeNeighbors)
            {
                //已检测过的去除
                if (!closedList.Contains(neighborNode))
                {
                    //首先把未加入open的全加入
                    if (!openList.Contains(neighborNode))
                    {
                        neighborNode.G = GetDistance(currentNode, neighborNode);
                        neighborNode.H = GetDistance(neighborNode, endNode);
                        neighborNode.parentNode = currentNode;
                        openList.Add(neighborNode);
                    }
                    else//已加入的，计算新G和原G的大小
                    {
                        float newG = currentNode.G + GetDistance(currentNode, neighborNode);
                        if (newG < neighborNode.G)
                        {
                            neighborNode.G = newG;
                            neighborNode.H = GetDistance(neighborNode, endNode);
                            neighborNode.parentNode = currentNode;
                        }
                    }               
                }
            }
        }
        //搜索完毕
        CompleteHandler();
    }

    //根据地图数据，生成寻路信息
    private AstarNode[,] CreatPathFindData(MapInfo mapInfo)
    {
        AstarNode[,] targetMapPathFindData = new AstarNode[mapInfo.mapLength, mapInfo.mapWith];

        for(int i = 0; i < mapInfo.nodeCount; i++)
        {
            MapGridNode thisNode = mapInfo.GetNodeByIndex(i);
            targetMapPathFindData[thisNode.coordx, thisNode.coordy] = new AstarNode(thisNode.coordx, thisNode.coordy);
        }

        return targetMapPathFindData;
    }
    //从检测的终点，回溯到起点，构成寻路结果
    private List<AstarNode> RecallPath(AstarNode beginNode, AstarNode currentNode)
    {
        List<AstarNode> foundPath = new List<AstarNode>();
        AstarNode midNode = currentNode;
        while (!midNode.Equals(beginNode))
        {
            foundPath.Add(midNode);
            midNode = midNode.parentNode;
        }

        foundPath.Reverse();
        return foundPath;
    }
    //获取某点的相邻格点
    private List<AstarNode> GetNeighborNodes(AstarNode targetNode)
    {
        List<AstarNode> neighborNodes = new List<AstarNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //TODO 根据移动类型取相邻格点
                if (findMapInfo.moveType == MoveType.four &&  Math.Abs(x) != Math.Abs(y))
                {
                    AstarNode neighborNode = GetNeighborNode(targetNode.coordx + x, targetNode.coordy + y);
                    if (null != neighborNode && findMapInfo.GetNodeByCoord(neighborNode.coordx, neighborNode.coordy).canWalk)
                    {
                        neighborNodes.Add(neighborNode);
                    }
                }

                if (findMapInfo.moveType == MoveType.eight)
                {
                    AstarNode neighborNode = GetNeighborNode(targetNode.coordx + x, targetNode.coordy + y);
                    if (null != neighborNode && findMapInfo.GetNodeByCoord(neighborNode.coordx, neighborNode.coordy).canWalk)
                    {
                        neighborNodes.Add(neighborNode);
                    }
                }
            }
        }
        return neighborNodes;
    }
    //根据坐标获取相邻节点
    private AstarNode GetNeighborNode(int coordx, int coordy)
    {
        if (coordx < findMapInfo.mapLength
            && coordx >= 0
            && coordy < findMapInfo.mapWith
            && coordy >= 0)
        {
            AstarNode lookForNode = null;
            lookForNode = targetMapPathFindData[coordx, coordy];
            if (null != lookForNode)
            {
                return lookForNode;
            }
        }
        return null;
    }
    //Manhattan Distance
    private float GetDistance(AstarNode firstNode, AstarNode lastNode)
    {
        //TODO get G or h
        int distX = Math.Abs(firstNode.coordx - lastNode.coordx);
        int distY = Math.Abs(firstNode.coordy - lastNode.coordy);

        //return 14 * distX + 10 * distY;
        return  distX + distY;
    }
    private void CompleteHandler()
    {
        findResultCallback(foundPath);
    }
}

