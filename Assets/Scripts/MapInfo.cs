using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum MoveType
{
    four,
    eight,
}

public class MapInfo
{
    public int mapWith { get; set; }
    public int mapLength { get; set; }
    public MoveType moveType = MoveType.four;
    //实体节点列表
    public List<MapGridNode> mapGridNodeList = new List<MapGridNode>();
    public int nodeCount { get { return mapGridNodeList.Count; } }

    //索引表
    private MapGridNode[,] mapGridCoordIndexArray;

    //设置地图长宽
    public void SetMapData(int maxWith, int maxlength, MoveType type)
    {
        mapWith = maxWith;
        mapLength = maxlength;
        moveType = type;
        mapGridCoordIndexArray = new MapGridNode[maxWith + 1, maxlength + 1];
    }
    //新增节点
    public void AddNode(int x, int y, MapGridNode node)
    {  
        //索引表
        mapGridCoordIndexArray[x, y] = node;
        //原表
        mapGridNodeList.Add(node);
    }
    //改变节点
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

