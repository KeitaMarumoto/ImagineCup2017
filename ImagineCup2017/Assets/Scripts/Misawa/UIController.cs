using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
    [SerializeField]
    FactoryManager factory;

    [SerializeField]
    Image[] rankupUIImages;

    [SerializeField]
    Text[] buildText;

    [SerializeField]
    Text[] rankupText;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            buildText[i].text = "商品：" + factory.GetFactoryStatus(i % 2,0).productName + "\n\n生産数：" + factory.GetFactoryStatus(i % 2, 0).productCount + "\n\n建設費：" + factory.GetFactoryStatus(i % 2, 0).rankUpcost;
        }
        //StartCoroutine(Wait());
    }

    public void setRankupText(int factoryID,int rank)
    {
        buildText[factoryID].text = "商品：" + factory.GetFactoryStatus(factoryID, rank).productName + "\n\n生産数：" + factory.GetFactoryStatus(factoryID, rank).productCount + "\n\n建設費：" + factory.GetFactoryStatus(factoryID, rank).rankUpcost;
    }

    public void setRankupUIMaterial(Material mat,int num)
    {
        rankupUIImages[num].material = mat;
    }
}
