using UnityEngine;

public class FlatNode: MapGridNode
{
    private bool m_showPath;
    private Color m_originalColor;
    private MeshRenderer m_meshRenderer;

    public bool isShowPath
    {
        get
        {
            if (null == m_meshRenderer)
            {
                Debug.LogWarning(gameObject.name + ": MapNode don't have material,can't show path!");
            }

            m_meshRenderer.material.color = m_originalColor;
            return m_showPath;
        }
        set
        {
            if (null == m_meshRenderer)
            {
                Debug.LogWarning( gameObject.name + ": MapNode don't have material,can't show path!");
            }

            m_showPath = value;
            m_meshRenderer.material.color = Color.red;
        }
    }

    public FlatNode(int x, int y, Vector3 pos, GameObject gridObj)
        : base(x, y, pos, gridObj)
    {
        canWalk = true;
        type = MapNodeType.flat;
        m_meshRenderer = gameObject.transform.Find("FloorMesh").GetComponent<MeshRenderer>();
        m_originalColor = m_meshRenderer.material.color;
    }
}

