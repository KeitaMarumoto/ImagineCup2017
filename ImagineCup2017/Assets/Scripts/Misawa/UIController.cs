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
    Toggle[] buildButtons;

    [SerializeField]
    Text[] rankupText;

    [SerializeField]
    Button checkButton;
     
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (factory.CanBuild(i) == true)
            {
                buildButtons[i].interactable = true;

                buildText[i].text = "商品：" + factory.GetFactoryStatus(i % 4, 0).productName
                                + "\n生産数：" + factory.GetFactoryStatus(i % 4, 0).productCount
                                + "\n建設費：" + factory.GetFactoryStatus(i % 4, 0).rankUpcost
                                + "\n維持費：" + (factory.GetFactoryStatus(i % 4, 0).rankUpcost / 10).ToString();
            }
            else
            {
                buildButtons[i].interactable = false;

                buildText[i].text = "商品：" + "?????"
                                + "\n生産数：" + "?????"
                                + "\n建設費：" + "?????"
                                + "\n維持費：" + "?????";
            }
        }
        checkButton.interactable = false;
    }

    public void setBuidStatusUI(int id)
    {
        buildButtons[id].interactable = true;
        buildText[id].text = "商品：" + factory.GetFactoryStatus(id, 0).productName
                        + "\n生産数：" + factory.GetFactoryStatus(id, 0).productCount
                        + "\n建設費：" + factory.GetFactoryStatus(id, 0).rankUpcost
                        + "\n維持費：" + (factory.GetFactoryStatus(id, 0).rankUpcost / 10).ToString();
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
