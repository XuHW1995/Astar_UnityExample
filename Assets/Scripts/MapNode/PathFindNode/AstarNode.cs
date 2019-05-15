public class AstarNode : PathFindNode
{
    public float h;
    public float g;
    public float f
    {
        get
        {
            return g + h;
        }
    }

    public AstarNode parentNode;
}

