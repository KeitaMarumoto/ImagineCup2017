using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuPopupController : MonoBehaviour {
    [SerializeField]
    FactoryManager factoryManager;

    [SerializeField]
    ProductRegister productRegister;

    [SerializeField]
    GameObject popup;

    StateManager.State beforeState;

    [SerializeField, Space(15)]
    Text[] stockName;

    [SerializeField]
    Text[] stockNum;
    
    [SerializeField,Space(15)]
    Text maintenanceCost;

    [SerializeField, Space(15)]
    Text[] productName;

    [SerializeField]
    Text[] productNum;

    void SetStatusText()
    {
        productRegister.getProductDatas();

        Dictionary<string, int> stockCount = productRegister.getNumberOfProducts();
        List<string> stockName = new List<string>();
        List<int> stockNum = new List<int>();

        foreach (var stock in stockCount)
        {
            stockName.Add(stock.Key);
            stockNum.Add(stock.Value);
        }

        for (int i = 0; i < stockCount.Count; i++)
        {
            this.stockName[i].text = stockName[i];
            this.stockNum[i].text = stockNum[i].ToString();
        }

        maintenanceCost.text = factoryManager.PayMaintenance().ToString();

        Dictionary<string,int> productCount = factoryManager.Make();
        List<string> proName = new List<string>();
        List<int> proNum = new List<int>();

        foreach(var pro in productCount)
        {
            proName.Add(pro.Key);
            proNum.Add(pro.Value);
        }

        for (int i = 0; i < productCount.Count;i++)
        {
            productName[i].text = proName[i];
            productNum[i].text = proNum[i].ToString();
        }
    }

    public void OpenPopup()
    {
        beforeState = StateManager.state;
        StateManager.state = StateManager.State.EVENT;
        SetStatusText();
        popup.SetActive(true);
    }

    public void ClosePopup()
    {
        StateManager.state = beforeState;
        popup.SetActive(false);
    }
}
