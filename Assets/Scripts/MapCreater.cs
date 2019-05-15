using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreater : MonoBehaviour
{
    public int mapWith = 10;
    public int mapLength = 10;
    public int barrierCount = 0;
    public GameObject rootGrid;
    public GameObject barrierPrefab;
    //地图数据
    private MapInfo mapInfo = new MapInfo();

    private MapCreater instance = null;
    public MapCreater GetInstance()
    {
        if (null == instance)
        {
            Debug.Log("MapCreater don't have instance!");
        }

        return instance;
    }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitMapInfo();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                //TODO 寻路请求
                Debug.Log("终点是：" + hitInfo.transform.name);
            }
        }   
    }

    //初始化地图信息
    void InitMapInfo()
    {
        CreatMap();
        RandomCreatBarrierAndBeginNode();
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
                mapInfo.mapGridNodeList.Add(mapGridNode);
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
            int index = UnityEngine.Random.Range(0, mapInfo.mapGridNodeList.Count);
            if (!barrierIndexList.Contains(index))
            {
                barrierIndexList.Add(index);
                GameObject barrierGrid = Instantiate(barrierPrefab);
                MapGridNode randomNode = mapInfo.mapGridNodeList[index];
                barrierGrid.transform.SetParent(randomNode.gameObject.transform);
                barrierGrid.transform.position = barrierGrid.transform.parent.position;
                
                //障碍物节点替换原平地节点
                BarrierNode barrierNode = new BarrierNode(randomNode.coordx, randomNode.coordy, randomNode.pos, randomNode.gameObject, barrierGrid);
                mapInfo.mapGridNodeList[index] = barrierNode;
            }
        }

        while(mapInfo.beginNode == null)
        {
            int index = UnityEngine.Random.Range(0, mapInfo.mapGridNodeList.Count);
            if (!barrierIndexList.Contains(index))
            {
                mapInfo.beginNode = mapInfo.mapGridNodeList[index] as FlatNode;
                mapInfo.beginNode.isShowPath = true;
            }
        }
    }

    public MapInfo GetMapInfo()
    {
        return mapInfo;
    }
}
