using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapInfo
{
    ////寻路节点二维数组
    //private PathFindNode[][] m_pathFindNodeArray; 

    public int mapWith { get; set; }
    public int mapLength { get; set; }
    public List<MapGridNode> mapGridNodeList { get; set; } = new List<MapGridNode>();

    public FlatNode beginNode { get; set; }
}

