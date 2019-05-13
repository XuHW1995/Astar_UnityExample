using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public int mapWith = 10;
    public int mapLength = 10;
    public GameObject rootGrid;

    public GameObject barrier;
    public int barrierCount = 0;

    void Start()
    {
        InitMapInfo();
    }

    void InitMapInfo()
    {
        CreatMap();
        RandomCreatBarrier();
        //SetMapInfo();
    }

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
            }
        }
    }

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
            int index = Random.Range(0, this.transform.childCount);
            if (!indexList.Contains(index))
            {
                indexList.Add(index);
                GameObject barrierGrid = Instantiate(barrier);
                barrierGrid.transform.SetParent(this.transform.GetChild(index));
                barrierGrid.transform.position = barrierGrid.transform.parent.position;
            }
        }
    }

    void SetMapInfo()
    {
        for (int x = 0; x < mapLength; x++)
        {
            for (int y = 0; y < mapWith; y++)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
