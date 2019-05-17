public class AstarNode
{
    public int coordx { get; }
    public int coordy { get; }

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

