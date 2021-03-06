﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FactoryController : MonoBehaviour {

    [SerializeField]
    FactoryManager factoryManager;

    [SerializeField]
    MapGenerator mapGenerator;

    [SerializeField]
    FundsController fundsController;

    [SerializeField]
    ProductRegister productRegister;

    [SerializeField]
    PollutionStatus pollutionStatus;

    [SerializeField]
    Population population;

    [SerializeField]
    GameObject checkPopup;

    [SerializeField]
    GameObject[] missPopups;

    int buildFactoryID;

    void Start()
    {
        buildFactoryID = -1;
        //StartCoroutine(PayMaintenance());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (StateManager.state == StateManager.State.RANKUP)
            {
                mapGenerator.ChoicePosition();
            }
            if (StateManager.state == StateManager.State.BUILD)
            {
                mapGenerator.ChoicePosition();
            }
            else if (StateManager.state == StateManager.State.PRODUCTION)
            {
                if (RayCast().tag == "IndustryTab") return;

                population.addPopulation++;

                //工場で商品を生産
                Dictionary<string, int> productCount = factoryManager.Make();
                foreach (KeyValuePair<string, int> product in productCount)
                {
                    productRegister.NumberOfProductsValueChange(product.Key, product.Value);
                }

                //生産時に発生した汚染を反映
                Dictionary<string, float> makePollution = factoryManager.MakePollution();
                foreach (KeyValuePair<string, float> pollution in makePollution)
                {
                    pollutionStatus.SetPollution(pollution.Key, pollution.Value);
                }

                SoundManager.Instance.PlaySE("makeItem4");
                mapGenerator.PlayParticle();
            }
        }
    }

    public void OnClickChoiceBuild(int factoryID)
    {
        //if (factoryManager.CanBuild(factoryID) == false) return;
        //StartCoroutine(BuildNewFactory(factoryID));
        /*if (mapGenerator.CreateBuilding(factoryID))
        {
            int cost = factoryManager.Construction(mapGenerator.GetThisFactoryID());
            fundsController.FundsValueChange(-cost);
            FactoryStatusData data = factoryManager.GetFactoryStatus(mapGenerator.GetThisFactoryID(), 0);
            pollutionStatus.SetPollution(data.pollutionType, data.pollutionDegree);
            SoundManager.Instance.PlaySE("build");
        }*/
        buildFactoryID = factoryID;
    }

    public void OnCickPopOpen() {
        if (fundsController.GetFunds() < factoryManager.GetFactoryStatus(buildFactoryID, 0).rankUpcost)
        {
            missPopups[1].SetActive(true);
            return;
        }

        if (mapGenerator.CheckCanBuildPos() == false)
        {
            missPopups[0].SetActive(true);
            return;
        }
        checkPopup.SetActive(true);
    }

    public void OnClickBuildButton()
    {
        if (buildFactoryID < 0) return;

        int productID = 0;
        switch (factoryManager.GetFactoryStatus(buildFactoryID, 0).productName)
        {
            case "Toy":
                productID = 0;
                break;
            case "Game":
                productID = 1;
                break;
            case "Tool":
                productID = 2;
                break;
            default:
                productID = 0;
                break;
        }
        if (mapGenerator.CreateBuilding(buildFactoryID, productID))
        {
            int cost = factoryManager.Construction(mapGenerator.GetThisFactoryID());
            fundsController.FundsValueChange(-cost);
            FactoryStatusData data = factoryManager.GetFactoryStatus(mapGenerator.GetThisFactoryID(), 0);
            pollutionStatus.SetPollution(data.pollutionType, data.pollutionDegree);
            SoundManager.Instance.PlaySE("build");
            //buildFactoryID = -1;
        }
        /*else
        {
            missPopups[0].SetActive(true);
        }*/
    }

    public void ClosePopup()
    {
        checkPopup.SetActive(false);
        foreach (var missPop in missPopups) {
            missPop.SetActive(false);
        }
    }

    /*
    IEnumerator BuildNewFactory(int buildFactoryID)
    {
        while (StateManager.state == StateManager.State.BUILD)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mapGenerator.CreateBuilding(buildFactoryID))
                {
                    int cost = factoryManager.Construction(mapGenerator.GetThisFactoryID());
                    fundsController.FundsValueChange(-cost);
                    FactoryStatusData data = factoryManager.GetFactoryStatus(mapGenerator.GetThisFactoryID(), 0);
                    pollutionStatus.SetPollution(data.pollutionType, data.pollutionDegree);
                    SoundManager.Instance.PlaySE("build");
                    break;
                    //StateManager.state = StateManager.State.PRODUCTION;
                }
            }
            yield return null;
        }
    }*/

    public void OnClickRankUpButton()
    {
        if (fundsController.GetFunds() < factoryManager.GetFactoryStatus(buildFactoryID, mapGenerator.GetThisFactoryRank()+1).rankUpcost)
        {
            missPopups[1].SetActive(true);
            return;
        }

        RankUpFactory();
        //StateManager.state = StateManager.State.RANKUP;
        //StartCoroutine(RankUpFactory());
    }
    /*
    IEnumerator RankUpFactory()
    {
        while (StateManager.state == StateManager.State.RANKUP)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mapGenerator.RankUpBuilding())
                {
                    int cost = factoryManager.RankUp(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank());
                    fundsController.FundsValueChange(-cost);
                    FactoryStatusData data = factoryManager.GetFactoryStatus(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank());
                    pollutionStatus.SetPollution(data.pollutionType,data.pollutionDegree);
                    StateManager.state = StateManager.State.PRODUCTION;
                }
            }
            yield return null;
        }
    }
    */

    void RankUpFactory()
    {
        int productID = 0;
        switch (factoryManager.GetFactoryStatus(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank()+1).productName)
        {
            case "Toy":
                productID = 0;
                break;
            case "Game":
                productID = 1;
                break;
            case "Tool":
                productID = 2;
                break;
            default:
                productID = 0;
                break;
        }
        if (mapGenerator.RankUpBuilding(productID))
        {
            int cost = factoryManager.RankUp(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank());
            fundsController.FundsValueChange(-cost);
            FactoryStatusData data = factoryManager.GetFactoryStatus(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank());
            pollutionStatus.SetPollution(data.pollutionType, data.pollutionDegree);
            SoundManager.Instance.PlaySE("build");
            //StateManager.state = StateManager.State.PRODUCTION;
        }
    }

    public void OnClickCancelButton()
    {
        StateManager.state = StateManager.State.PRODUCTION;
    }


    //工場の維持費を払う
    public void PayMaintenance()
    {
        int maintenanceCost = 0;
        maintenanceCost = factoryManager.PayMaintenance();
        fundsController.FundsValueChange(-maintenanceCost);
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
