using UnityEngine;

public class FlatNode : MapGridNode
{
    private bool m_showPath;
    private Color m_originalColor;
    private MeshRenderer m_meshRenderer;
    private bool m_isBegin;

    public bool isShowPath
    {
        get
        {
            return m_showPath;
        }
        set
        {
            if (null == m_meshRenderer)
            {
                Debug.LogWarning( gameObject.name + ": MapNode don't have material,can't show path!");
            }

            m_showPath = value;
            if (m_showPath == true)
            {
                m_meshRenderer.material.color = Color.red;
            }
            else
            {
                m_meshRenderer.material.color = Color.white;
            }
        }
    }

    public bool isBegin
    {
        set
        {
            m_meshRenderer.material.color = Color.green;
            m_isBegin = value;
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

