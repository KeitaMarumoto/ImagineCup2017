using UnityEngine;
using System.Collections;

public class AutoSell : MonoBehaviour
{
    [SerializeField]
    FundsController fundsController;

    [SerializeField]
    ProductRegister productRegister;

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
            if (productRegister.getProductDatas()[key].NumberOfProducts > 0)
            {
                productRegister.NumberOfProductsValueChange(key, -1);
                fundsController.FundsValueChange(productRegister.getProductDatas()[key].UnitPrice);
            }
            yield return new WaitForSeconds(productRegister.getProductDatas()[key].SecondsToSell);
        }
    }

}
