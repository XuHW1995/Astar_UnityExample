using System.Collections.Generic;
using UnityEngine;

public class PathFindTest:MonoBehaviour
{
    MapCreater mapCreater = null;

    private void Start()
    {
        mapCreater = MapCreater.GetInstance();
    }

    private void Update()
    {
        if (mapCreater.initMap)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mapCreater.GetMapInfo().CleanShowPath();
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 100))
                {
                    if (hitInfo.transform.name != "barrier(Clone)")
                    {
                        MapGridNode endNode = mapCreater.GetMapInfo().GetNodeByName(hitInfo.transform.name);


                        PathFindMgr.RequestPathFind(mapCreater.beginNode, endNode, mapCreater.GetMapInfo(), FindComplete);

                        Debug.Log("终点是：" + hitInfo.transform.name);
                    }
                }
            }
        }
    }

    void FindComplete(List<AstarNode> foundPath)
    {
        foreach (AstarNode pathFindNode in foundPath)
        {
            FlatNode flatNode = mapCreater.GetMapInfo().GetNodeByCoord(pathFindNode.coordx, pathFindNode.coordy) as FlatNode;
            flatNode.isShowPath = true;
        }
    }
}

