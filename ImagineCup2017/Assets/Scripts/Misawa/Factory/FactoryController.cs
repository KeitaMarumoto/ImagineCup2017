using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FactoryController : MonoBehaviour {
    enum State
    {
        MAKE, BUILD, RANKUP
    }

    [SerializeField]
    FactoryManager factoryManager;

    [SerializeField]
    MapGenerator mapGenerator;

    [SerializeField]
    FundsController fundsController;

    [SerializeField]
    ProductRegister productRegister;

    State state;

    // Use this for initialization
    void Start()
    {
        state = State.MAKE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == State.MAKE)
            {
                //工場で商品を生産
                Dictionary<string, int> productCount = factoryManager.Make();

                foreach (KeyValuePair<string, int> product in productCount)
                {
                    productRegister.NumberOfProductsValueChange(product.Key, product.Value);
                }
            }
        }
    }

    public void OnClickBuildButton(int factoryID)
    {
        state = State.BUILD;
        StartCoroutine(BuildNewFactory(factoryID));
    }

    IEnumerator BuildNewFactory(int buildFactoryID)
    {
        while (state == State.BUILD)
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
