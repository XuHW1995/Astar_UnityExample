using UnityEngine;

public class BarrierNode: MapGridNode
{
    private GameObject m_barrierObj;
    //障碍物
    public GameObject barrierObj { get { return m_barrierObj; } }

    public BarrierNode(int x, int y, Vector3 pos, GameObject obj, GameObject barrierObj)
        :base(x, y, pos, obj)
    {
        canWalk = false;
        type = MapNodeType.barrier;
        m_barrierObj = barrierObj;
    }
}

