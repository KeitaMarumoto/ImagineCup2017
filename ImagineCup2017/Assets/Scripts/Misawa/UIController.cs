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

    [SerializeField]
    Button checkButton;
     
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            buildText[i].text = "商品：" + factory.GetFactoryStatus(i % 2,0).productName 
                            + "\n生産数：" + factory.GetFactoryStatus(i % 2, 0).productCount
                            + "\n建設費：" + factory.GetFactoryStatus(i % 2, 0).rankUpcost
                            + "\n維持費：" + (factory.GetFactoryStatus(i % 2, 0).rankUpcost / 10).ToString();
        }
        checkButton.interactable = false;
    }

    public void clearRankupText(int num)
    {
        rankupText[num].text = "";
    }

    public void setRankupText(int factoryID,int rank,int num)
    {
        rankupText[num].text = "商品：" + factory.GetFactoryStatus(factoryID, rank).productName.ToString()
                            + "\n生産数：" + factory.GetFactoryStatus(factoryID, rank).productCount.ToString()
                            + "\n維持費：" + (factory.GetFactoryStatus(factoryID, rank).rankUpcost/10).ToString();
        if(num == 1)
        {
            rankupText[num].text += "\n建設費：" + factory.GetFactoryStatus(factoryID, rank).rankUpcost.ToString();
        }
    }

    public void setRankupUIMaterial(Material mat, int num)
    {
        checkButton.interactable = (mat != null);
        rankupUIImages[num].material = mat;
    }
}
