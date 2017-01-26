using UnityEngine;

/// <summary>
/// 商品モデル
/// </summary>
public class ProductData {
	/// <summary>
	/// 名前
	/// </summary>
	public string Name { get; private set; }
	/// <summary>
	/// 単価
	/// </summary>
	public float UnitPrice { get; private set; }
	/// <summary>
	/// 原価
	/// </summary>
	public float Cost { get; private set; }
	/// <summary>
	/// 商品ランク
	/// </summary>
	public int Rank { get; private set; }
	/// <summary>
	/// 1つ当たりの売却にかかる時間。
	/// </summary>
	public float SecondsToSell { get; private set; }
	/// <summary>
	/// 商品の数
	/// </summary>
	public int NumberOfProducts { get; set; }

	/// <summary>
	/// 商品データのコンストラクタ
	/// </summary>
	/// <param name="name_">名前</param>
	/// <param name="unitPrice_">単価</param>
	/// <param name="cost">原価</param>
	/// <param name="rank">商品ランク</param>
	/// <param name="secondsToSelling">1つ当たり売却にかかる時間</param>
	public ProductData(string name_, 
		               float unitPrice_, 
					   float cost_, 
					   int rank_, 
					   float secondsToSell_)
	{
		Name = name_;
		UnitPrice = unitPrice_;
		Cost = cost_;
		Rank = rank_;
		SecondsToSell = secondsToSell_;
		NumberOfProducts = 0;
	}
}
