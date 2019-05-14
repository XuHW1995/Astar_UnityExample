using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public int mapWith = 10;
    public int mapLength = 10;
    public GameObject rootGrid;

    public GameObject barrierPrefab;
    public int barrierCount = 0;

    //抽象地图数据
    private MapInfo mapInfo;
    //实体节点信息
    private List<MapGridNode> mapGridNodeList = new List<MapGridNode>();

    void Start()
    {
        InitMapInfo();
    }

    void InitMapInfo()
    {
        CreatMap();
        RandomCreatBarrier();
        SetMapInfo();
    }

    //生成地图
    void CreatMap()
    {
        for(int x = 0; x < mapLength; x++)
        {
            for (int y = 0; y < mapWith; y++)
            {
                GameObject mapGrid = Instantiate(rootGrid);
                mapGrid.transform.position = new Vector3(x,0,y);
                mapGrid.name = x + "_" + y;
                mapGrid.transform.SetParent(this.transform);

                FlatNode mapGridNode = new FlatNode(x, y, mapGrid.transform.position, mapGrid);
                mapGridNodeList.Add(mapGridNode);
            }
        }
    }

    //随机生成障碍物
    void RandomCreatBarrier()
    {
        if (barrierCount > this.transform.childCount)
        {
            Debug.LogWarning("Invalid barrierCount is " + barrierCount);
            return;
        }

        List<int> indexList = new List<int>();
        while (indexList.Count < barrierCount)
        {
            //range函数左闭，右开
            int index = Random.Range(0, mapGridNodeList.Count);
            if (!indexList.Contains(index))
            {
                indexList.Add(index);
                GameObject barrierGrid = Instantiate(barrierPrefab);
                MapGridNode randomNode = mapGridNodeList[index];
                barrierGrid.transform.SetParent(randomNode.gameObject.transform);
                barrierGrid.transform.position = barrierGrid.transform.parent.position;
                
                //障碍物节点替换原平地节点
                BarrierNode barrierNode = new BarrierNode(randomNode.coordx, randomNode.coordy, randomNode.pos, randomNode.gameObject, barrierGrid);
                mapGridNodeList[index] = barrierNode;
            }
        }
    }

    //设置地图信息
    void SetMapInfo()
    {
        mapInfo = new MapInfo(mapWith, mapLength, mapGridNodeList);   
    }
}
