public class AstarNode : PathFindNode
{
    public float H;
    public float G;
    public float F
    {
        get
        {
            return G + H;
        }
    }

    public AstarNode parentNode; 
}

