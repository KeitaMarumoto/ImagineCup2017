using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class test : MonoBehaviour {
    /*
    [SerializeField]
    Text[] countText;

    [SerializeField]
    Text[] fCount;
    */
    [SerializeField]
    FactoryManager factoryManager;

    int[] productCount = new int[4];
    // Use this for initialization
    void Start () {
        foreach (var product in productCount) product.Equals(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            factoryManager.Construction("工場A");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            factoryManager.Construction("工場B");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            factoryManager.RankUp("工場A", 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            factoryManager.RankUp("工場B", 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            int[] pro = factoryManager.Make();

            for (int i = 0; i < 4; i++)
            {
                productCount[i] += pro[i];
            }
            string log = "";
            log += "ランク1：" + factoryManager.GetFactoriesCount("工場A", 1).ToString() + "\n";
            log += "ランク2：" + factoryManager.GetFactoriesCount("工場A", 2).ToString() + "\n";
            log += "ランク3：" + factoryManager.GetFactoriesCount("工場A", 3).ToString() + "\n";
            
            log += "ランク1：" + factoryManager.GetFactoriesCount("工場B", 1).ToString() + "\n";
            log += "ランク2：" + factoryManager.GetFactoriesCount("工場B", 2).ToString() + "\n";
            log += "ランク3：" + factoryManager.GetFactoriesCount("工場B", 3).ToString() + "\n";

            Debug.Log(log);
            log = "";
            for (int i = 0; i < 4; i++) {
                log += i+"の個数：" + productCount[i].ToString("00000000") +"\n";
            }
            Debug.Log(log);
        }

        //fCount[0].text = "ランク1：" + factoryManager.GetFactoriesCount("工場A", 1).ToString();
        //fCount[1].text = "ランク2：" + factoryManager.GetFactoriesCount("工場A", 2).ToString();
        //fCount[2].text = "ランク3：" + factoryManager.GetFactoriesCount("工場A", 3).ToString();

        //fCount[3].text = "ランク1：" + factoryManager.GetFactoriesCount("工場B", 1).ToString();
        //fCount[4].text = "ランク2：" + factoryManager.GetFactoriesCount("工場B", 2).ToString();
        //fCount[5].text = "ランク3：" + factoryManager.GetFactoriesCount("工場B", 3).ToString();

        //for (int i = 0; i < 4; i++) {
        //    countText[i].text = i+"の個数：" + productCount[i].ToString("00000000");
        //}
    }
}
