using UnityEngine;

public class BarrierNode : MapGridNode
{
    //障碍物
    public GameObject barrierObj { get; }
    public BarrierNode(int x, int y, Vector3 pos, GameObject obj, GameObject barrierObj)
        :base(x, y, pos, obj)
    {
        canWalk = false;
        type = MapNodeType.barrier;
        this.barrierObj = barrierObj;
    }
}

