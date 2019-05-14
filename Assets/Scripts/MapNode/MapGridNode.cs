using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum MapNodeType
{
    min = 0,
    flat = 1,
    barrier = 2,
    max,
}

public class MapGridNode
{
    //格点坐标位置
    private int m_coordx;
    private int m_coordy;
    //格点实际位置
    private Vector3 m_pos;
    private GameObject m_gameObject;
    private bool m_canWalk;
    private MapNodeType m_type;

    public int coordx { get { return m_coordx; } }
    public int coordy { get { return m_coordy; } }
    public Vector3 pos { get { return m_pos; } }
    public GameObject gameObject { get { return m_gameObject; } }
    public MapNodeType type
    {
        get { return m_type; }
        set { m_type = value; }
    }

    public bool canWalk
    {
        get
        {
            return m_canWalk;
        }

        set
        {
            m_canWalk = value;
        }
    }

    public MapGridNode(int x, int y, Vector3 pos, GameObject obj)
    {
        m_coordx = x;
        m_coordy = y;
        m_pos = pos;
        m_gameObject = obj;
    }
}

