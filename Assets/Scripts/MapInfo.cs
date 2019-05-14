using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapInfo
{
    private int m_mapWith;
    private int m_mapLength;
    //寻路节点二维数组
    private Node[][] m_pathFindNodeArray;
    
    //实体节点
    private List<MapGridNode> m_mapGridList;

    public int mapWith
    {
        get { return m_mapWith; }
        set { m_mapWith = value; }
    }

    public int mapLength
    {
        get { return m_mapLength; }
        set { m_mapLength = value; }
    }

    public MapInfo(int with, int length, List<MapGridNode> gridNodes)
    {

    }

    public void CreatNodeArray()
    {

    }
}

