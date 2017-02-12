﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {
    enum State
    {
        MAKE,BUILD,RANKUP
    }

    [SerializeField]
    FactoryManager factoryManager;

    [SerializeField]
    MapGenerator mapGenerator;

    [SerializeField]
    FundsController fundsController;

    [SerializeField]
    ProductRegister productRegister;

    //int[] productCount = new int[4];

    State state;
    //int buildFactoryID;
    
    // Use this for initialization
    void Start () {
        state = State.MAKE;
        //buildFactoryID = 0;
        //foreach (var product in productCount) product.Equals(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(state == State.MAKE)
            {
                //工場で商品を生産
                Dictionary<string, int> pro = factoryManager.Make();

                foreach (KeyValuePair<string, int> tes in pro)
                {
                    productRegister.NumberOfProductsValueChange(tes.Key,tes.Value);
                }
                /*for (int i = 0; i < 4; i++)
                {
                    productCount[i] += pro[i];
                }*/

                string log = "";
                log += "ランク1：" + factoryManager.GetFactoriesCount(0, 1).ToString() + "\n";
                log += "ランク2：" + factoryManager.GetFactoriesCount(0, 2).ToString() + "\n";
                log += "ランク3：" + factoryManager.GetFactoriesCount(0, 3).ToString() + "\n";

                log += "ランク1：" + factoryManager.GetFactoriesCount(1, 1).ToString() + "\n";
                log += "ランク2：" + factoryManager.GetFactoriesCount(1, 2).ToString() + "\n";
                log += "ランク3：" + factoryManager.GetFactoriesCount(1, 3).ToString() + "\n";

                Debug.Log(log);
                productRegister.tesLog();
            }
        }
    }

    public void OnClickBuildButton(int factoryID)
    {
        //buildFactoryID = factoryID;
        state = State.BUILD;
        StartCoroutine(BuildNewFactory(factoryID));
    }

    IEnumerator BuildNewFactory(int buildFactoryID)
    {
        while(state == State.BUILD)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mapGenerator.CreateBuilding(buildFactoryID))
                {
                    int num = factoryManager.Construction(mapGenerator.GetThisFactoryID());
                    fundsController.FundsValueChange(-num);
                    Debug.Log(num);
                    state = State.MAKE;
                }
            }
            yield return null;
        }
    }

    public void OnClickRankUpButton()
    {
        state = State.RANKUP;
        StartCoroutine(RankUpFactory());
    }

    IEnumerator RankUpFactory()
    {
        while (state == State.RANKUP)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mapGenerator.RankUpBuilding())
                {
                    int num = factoryManager.RankUp(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank());
                    fundsController.FundsValueChange(-num);
                    Debug.Log(num);
                    state = State.MAKE;
                }
                Debug.Log(state);
            }
            yield return null;
        }
    }

    public void OnClickCancelButton()
    {
        state = State.MAKE;
    }

    private GameObject RayCast()
    {
        //カメラの場所からポインタの場所に向かってレイを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        //レイが何か当たっているかを調べる
        if (Physics.Raycast(ray, out hit))
        {
            return (hit.collider.gameObject);
        }
        return null;
    }
}
