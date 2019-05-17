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
    allDir,
}

public class MapInfo
{
    public int mapWith { get; set; }
    public int mapLength { get; set; }
    public MoveType moveType = MoveType.four;

    //实体节点列表
    public List<MapGridNode> mapGridNodeList = new List<MapGridNode>();
    //索引表
    private MapGridNode[,] mapGridCoordIndexArray;
    private Dictionary<string, MapGridNode> name2Node = new Dictionary<string, MapGridNode>();

    public int nodeCount{  get { return mapGridNodeList.Count; } }

    //设置地图长宽
    public void  SetMapSize(int maxWith, int maxlength)
    {
        mapWith = maxWith;
        mapLength = maxlength;
        mapGridCoordIndexArray = new MapGridNode[maxWith + 1, maxlength + 1];
    }
    //新增节点
    public void AddNode(int x, int y, MapGridNode node)
    {  
        //索引表
        mapGridCoordIndexArray[x, y] = node;
        name2Node[node.gameObject.name] = node;
        //原表
        mapGridNodeList.Add(node);
    }
    //改变节点
    public void ChangeNode(int x,int y, MapGridNode node, int index)
    {  
        mapGridCoordIndexArray[x, y] = node;
        name2Node[mapGridNodeList[index].gameObject.name] = node;

        mapGridNodeList[index] = node;
    }
    //设置寻路节点
    public void  SetPathFindNode(int index, AstarNode findNode)
    {
        mapGridCoordIndexArray[mapGridNodeList[index].coordx, mapGridNodeList[index].coordy].pathFindNode = findNode;

        name2Node[mapGridNodeList[index].gameObject.name].pathFindNode = findNode;

        mapGridNodeList[index].pathFindNode = findNode;
    }
    public MapGridNode GetNodeByIndex(int index)
    {
        return mapGridNodeList[index];
    }
    public MapGridNode GetNodeByCoord(int coordx,int coordy)
    {
        if (coordx <= mapLength
            && coordx >= 0
            && coordy <= mapWith
            && coordy >= 0)
        {
            return mapGridCoordIndexArray[coordx, coordy];
        }

        return null;
    }
    public MapGridNode GetNodeByName(string name)
    {
        return name2Node[name];
    }
}

