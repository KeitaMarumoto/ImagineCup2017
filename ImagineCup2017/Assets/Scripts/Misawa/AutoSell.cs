using UnityEngine;
using System.Collections;

public class AutoSell : MonoBehaviour
{
    [SerializeField]
    FundsController fundsController;

    [SerializeField]
    ProductRegister productRegister;

    [SerializeField]
    Population popilation;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Start関数の実行順が不定のためProductRegisterクラスのStart関数が実行されるのを待つ
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        while (true)
        {
            if (productRegister.getProductDatas().Count > 0) break;

            yield return null;
        }

        foreach (string key in productRegister.getProductDatas().Keys)
        {
            StartCoroutine(SellProduct(key));
        }
    }

    /// <summary>
    /// 商品の売却速度に合わせて商品を売る
    /// </summary>
    /// <param name="key">自動売却をする商品の名前</param>
    /// <returns></returns>
    IEnumerator SellProduct(string key)
    {
        while (true)
        {
            if (StateManager.state == StateManager.State.PRODUCTION)
            {
                if (productRegister.getProductDatas()[key].NumberOfProducts > 0)
                {
                    int sellNum = (int)(productRegister.getProductDatas()[key].SecondsToSell) * (1 + (popilation.population / 10000));
                    Debug.Log("売れた数" + sellNum);
                    productRegister.NumberOfProductsValueChange(key, -sellNum);
                    fundsController.FundsValueChange(productRegister.getProductDatas()[key].UnitPrice * sellNum);
                }
                yield return new WaitForSeconds(1f);
            }
            else yield return null;
        }
    }

}
