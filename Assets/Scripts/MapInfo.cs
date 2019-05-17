using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    four,
    eight,
}

public class MapInfo
{
    //索引表
    private MapGridNode[,] mapGridCoordIndexArray;

    public int mapLength { get; set; }
    public int mapWith { get; set; }
    public MoveType moveType = MoveType.four;
    //实体节点列表
    public List<MapGridNode> mapGridNodeList = new List<MapGridNode>();
    public int nodeCount { get { return mapGridNodeList.Count; } }

    public void SetMapData(int maxlength, int maxWith, MoveType type)
    {
        mapLength = maxlength;
        mapWith = maxWith;  
        moveType = type;
        mapGridCoordIndexArray = new MapGridNode[maxlength, maxWith];
    }
    public void AddNode(int x, int y, MapGridNode node)
    {  
        //索引表
        mapGridCoordIndexArray[x, y] = node;
        //原表
        mapGridNodeList.Add(node);
    }
    public void ChangeNode(int x,int y, MapGridNode node, int index)
    {  
        mapGridCoordIndexArray[x, y] = node;
        mapGridNodeList[index] = node;
    }

    public MapGridNode GetNodeByIndex(int index)
    {
        return mapGridNodeList[index];
    }
    public MapGridNode GetNodeByCoord(int coordx,int coordy)
    {
        if (coordx < mapLength
            && coordx >= 0
            && coordy < mapWith
            && coordy >= 0)
        {
            return mapGridCoordIndexArray[coordx, coordy];
        }

        return null;
    }
    public MapGridNode GetNodeByName(string name)
    {
        string[] nameArray = name.Split('_');
        return GetNodeByCoord(int.Parse(nameArray[0]), int.Parse(nameArray[1]));
    }
    public void CleanShowPath()
    {
        foreach(var node in mapGridNodeList)
        {
            if (node.GetType() == typeof(FlatNode))
            {
                FlatNode thisNode = node as FlatNode;
                if (thisNode.isShowPath)
                {
                    thisNode.isShowPath = false;
                }
            }
        }
    }
}

