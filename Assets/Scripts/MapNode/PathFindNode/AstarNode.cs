public class AstarNode: NodeBase
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
    public AstarNode(int x, int y)
    {
        coordx = x;
        coordy = y;
    }
}

