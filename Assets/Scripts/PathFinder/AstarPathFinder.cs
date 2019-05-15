using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void AstarResult(List<AstarNode> findResult);

public class AstarPathFinder
{
    public PathFindNode beginNode { get; set; }
    public PathFindNode endNode { get; set; }
    public AstarResult findResultCallback;
    public List<AstarNode> foundPath { get; set; }

    //路径查找完成
    private bool m_findComplete = false;

    public AstarPathFinder(AstarNode begin, AstarNode end, AstarResult callback)
    {
        beginNode = begin;
        endNode = end;
        findResultCallback = callback;
    }

    public void FindPath()
    {        
        //要检索的点
        List<AstarNode> openList = new List<AstarNode>();
        //检索过的点
        HashSet<AstarNode> closedList = new HashSet<AstarNode>();

        //加入起始点
        openList.Add(beginNode as AstarNode);
        //只要检索列表有数据,就进行检索
        while (openList.Count > 0)
        {
            //每次取第一个进行检查
            AstarNode currentNode = openList[0];

            //如果第一个就是终点，那么路径搜索完毕
            if (currentNode.Equals(endNode))
            {   
                //TODO 从当前节点回溯，获取路径
                //foundPath = RetracePath(beginNode, currentNode);
                break;
            }

            for (int i = 0; i < openList.Count; i++)
            {
                AstarNode checkNode = openList[i];

                if (checkNode.f < currentNode.f || (checkNode.f == currentNode.f && checkNode.h < currentNode.h))
                {
                    if (!currentNode.Equals(checkNode))
                    {
                        currentNode = checkNode;
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

        }
        //搜索完毕
        CompleteHandler();
    }

    private void CompleteHandler()
    {
        findResultCallback(foundPath);
    }
}

