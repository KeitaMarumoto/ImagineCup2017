using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class test : MonoBehaviour {
    enum State
    {
        MAKE,BUILD,RANKUP
    }

    [SerializeField]
    FactoryManager factoryManager;

    [SerializeField]
    MapGenerator mapGenerator;

    int[] productCount = new int[4];

    State state;
    int buildFactoryID;
    
    // Use this for initialization
    void Start () {
        state = State.MAKE;
        buildFactoryID = 0;
        foreach (var product in productCount) product.Equals(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == State.BUILD)
            {
                if (mapGenerator.CreateBuilding(buildFactoryID))
                {
                    int num = factoryManager.Construction(mapGenerator.GetThisFactoryID());
                    Debug.Log(num);
                }
                state = State.MAKE;
            }
            else if (state == State.RANKUP)
            {
                if (mapGenerator.RankUpBuilding())
                {
                    int num = factoryManager.RankUp(mapGenerator.GetThisFactoryID(), mapGenerator.GetThisFactoryRank());
                    Debug.Log(num);
                }
                state = State.MAKE;
            }
            else
            {
                //工場で商品を生産
                int[] pro = factoryManager.Make();

                for (int i = 0; i < 4; i++)
                {
                    productCount[i] += pro[i];
                }

                string log = "";
                log += "ランク1：" + factoryManager.GetFactoriesCount(0, 1).ToString() + "\n";
                log += "ランク2：" + factoryManager.GetFactoriesCount(0, 2).ToString() + "\n";
                log += "ランク3：" + factoryManager.GetFactoriesCount(0, 3).ToString() + "\n";

                log += "ランク1：" + factoryManager.GetFactoriesCount(1, 1).ToString() + "\n";
                log += "ランク2：" + factoryManager.GetFactoriesCount(1, 2).ToString() + "\n";
                log += "ランク3：" + factoryManager.GetFactoriesCount(1, 3).ToString() + "\n";

                Debug.Log(log);
                log = "";
                for (int i = 0; i < 4; i++)
                {
                    log += i + "の個数：" + productCount[i].ToString("00000000") + "\n";
                }
                Debug.Log(log);
            }
        }
    }

    public void Build(int factoryID)
    {
        buildFactoryID = factoryID;
        state = State.BUILD;
    }

    public void RankUp()
    {
        state = State.RANKUP;
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
