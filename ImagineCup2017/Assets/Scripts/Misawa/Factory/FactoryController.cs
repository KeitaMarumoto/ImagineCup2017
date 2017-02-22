using UnityEngine;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (StateManager.state == StateManager.State.RANKUP)
            {
                mapGenerator.ChoicePosition();
            }
            else if (StateManager.state == StateManager.State.PRODUCTION)
            {
                if (RayCast().tag == "IndustryTab") return;

                //工場で商品を生産
                Dictionary<string, int> productCount = factoryManager.Make();

                foreach (KeyValuePair<string, int> product in productCount)
                {
                    productRegister.NumberOfProductsValueChange(product.Key, product.Value);
                }
                SoundManager.Instance.PlaySE("makeItem4");
                mapGenerator.PlayParticle();
            }
        }
    }

    public void OnClickBuildButton(int factoryID)
    {
        //if (factoryManager.CanBuild(factoryID) == false) return;
        StartCoroutine(BuildNewFactory(factoryID));
    }

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
    }

    public void OnClickRankUpButton()
    {
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
        if (mapGenerator.RankUpBuilding())
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
