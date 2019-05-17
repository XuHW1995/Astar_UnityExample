using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreater : MonoBehaviour
{
    #region 地图生成参数
    public int mapWith = 10;
    public int mapLength = 10;
    public int barrierCount = 0;
    public GameObject rootGrid;
    public GameObject barrierPrefab;
    public MoveType moveType = MoveType.four;
    #endregion

    //地图数据
    private MapInfo mapInfo = new MapInfo();

    public FlatNode beginNode { get; set; } = null;
    public bool initMap { get; set; } = false;

    //Singleton
    private static MapCreater instance = null;
    public static MapCreater GetInstance()
    {
        if (null == instance)
        {
            Debug.Log("MapCreater don't have instance!");
        }

        return instance;
    }
    public MapInfo GetMapInfo()
    {
        if (initMap)
        {
            return mapInfo;
        }
        return null;
    }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitMapInfo();
        InitCameraPos();
    }

    void InitCameraPos()
    {
        Camera.main.transform.position = mapInfo.GetNodeByCoord((int)Mathf.Floor(mapLength / 2), (int)Mathf.Floor(mapWith / 2)).pos + new Vector3(0,100,0);
    }
    //初始化地图信息
    void InitMapInfo()
    {

        CreatMap();
        RandomCreatBarrierAndBeginNode();

        initMap = true;
        Debug.Log("地图生成成功！");
    }
    //生成地图
    void CreatMap()
    {
        mapInfo.SetMapData(mapWith, mapLength, moveType);
        for(int x = 0; x < mapLength; x++)
        {
            for (int y = 0; y < mapWith; y++)
            {
                GameObject mapGrid = Instantiate(rootGrid);
                mapGrid.transform.position = new Vector3(x,0,y);
                mapGrid.name = x + "_" + y;
                mapGrid.transform.SetParent(this.transform);

                FlatNode mapGridNode = new FlatNode(x, y, mapGrid.transform.position, mapGrid);

                mapInfo.AddNode(x, y, mapGridNode);
            }
        }
    }
    //随机生成障碍物和起点
    void RandomCreatBarrierAndBeginNode()
    {
        if (barrierCount > this.transform.childCount)
        {
            Debug.LogWarning("Invalid barrierCount is " + barrierCount);
            return;
        }

        List<int> barrierIndexList = new List<int>();
        while (barrierIndexList.Count < barrierCount)
        {
            //range函数左闭，右开
            int index = UnityEngine.Random.Range(0, mapInfo.nodeCount);
            if (!barrierIndexList.Contains(index))
            {
                barrierIndexList.Add(index);
                GameObject barrierGrid = Instantiate(barrierPrefab);
                MapGridNode randomNode = mapInfo.GetNodeByIndex(index);
                barrierGrid.transform.SetParent(randomNode.gameObject.transform);
                barrierGrid.transform.position = barrierGrid.transform.parent.position;
                
                //障碍物节点替换原平地节点
                BarrierNode barrierNode = new BarrierNode(randomNode.coordx, randomNode.coordy, randomNode.pos, randomNode.gameObject, barrierGrid);
                mapInfo.ChangeNode(randomNode.coordx, randomNode.coordy, barrierNode, index);                 
            }
        }

        while(beginNode == null)
        {
            int index = UnityEngine.Random.Range(0, mapInfo.nodeCount);
            if (!barrierIndexList.Contains(index))
            {
                beginNode = mapInfo.GetNodeByIndex(index) as FlatNode;
                beginNode.isBegin = true;
            }
        }
    }
}
