﻿using UnityEngine;
using System.Collections.Generic;

public class ProductRegister : MonoBehaviour {
	[SerializeField]
	List<string> name_;
	[SerializeField]
	List<float> unitPrice_;
	[SerializeField]
	List<float> cost_;
	[SerializeField]
	List<int> rank_;
	[SerializeField]
	List<int> secondsToSell_;

	Dictionary<string, ProductData> products = new Dictionary<string, ProductData>();

	void Start ()
	{
		Register();
		OutProductsData();
	}

	/// <summary>
	/// 読み込んだ商品データをDictionaryでコンテナとして登録
	/// </summary>
	void Register()
	{
		for (int productDataNum = 0; productDataNum < name_.Count; productDataNum++)
		{
			ProductData product_ = new ProductData(name_[productDataNum],
												   unitPrice_[productDataNum],
												   cost_[productDataNum],
												   rank_[productDataNum],
												   secondsToSell_[productDataNum]);
			products.Add(product_.Name, product_);
		}
	}

	/// <summary>
	/// 商品データ取得(Dictionary<string,ProductData>)
	/// </summary>
	/// <returns>商品データを返却。</returns>
	public Dictionary<string, ProductData> getProductDatas() { return products; }

	//デバッグ用:商品データを確実に登録できているかの確認用。
	void OutProductsData()
	{
		foreach (KeyValuePair<string, ProductData> prod_ in products)
		{
			string message = "Name : " + prod_.Value.Name + " | "
						   + "UnitPrice : " + prod_.Value.UnitPrice.ToString() + " | "
						   + "Cost : " + prod_.Value.Cost.ToString() + " | "
						   + "Rank" + prod_.Value.Rank.ToString() + " | "
						   + "Seconds : " + prod_.Value.SecondsToSell.ToString() + " | "
						   + "Number : " + prod_.Value.NumberOfProducts.ToString();
			Debug.Log(message);
		}
	}
}
