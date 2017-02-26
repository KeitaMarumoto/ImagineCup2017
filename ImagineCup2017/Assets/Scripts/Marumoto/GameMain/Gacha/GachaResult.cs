using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using MiniJSON;

enum DATABASE_ELEMENTS
{
	NAME = 0,
	GRADE,
	SUPPLY,
	INDEX
}

public class GachaResult : MonoBehaviour {
	[SerializeField]
	Text text_;
	[SerializeField]
	GameObject supplyPanel_;
	[SerializeField]
	Text supplyValue;

	List<string> resultDatas = new List<string>();
	
	void Start () {
		GetFactory();
	}

	void JsonPersing(string text_)
	{
		var json = Json.Deserialize(text_) as List<object>;
		string jsonText = "";
		Dictionary<string, object> jsonDict = new Dictionary<string, object>();
		foreach(var dict in json)
		{
			jsonDict = (Dictionary<string, object>)dict;
		}
		foreach(var dict in jsonDict)
		{
			resultDatas.Add(dict.Value.ToString());
			jsonText += dict.Value.ToString() + "|";
		}
		TextUpdate();
	}

	void GetFactory()
	{
		string path_ = "http://localhost/gacha_db_request.php";
		HTTPService.Instance.Request(path_, JsonPersing);
	}

	void TextUpdate()
	{
		string message_ = "";
		if (resultDatas[(int)DATABASE_ELEMENTS.NAME] == "miss")
		{
			message_ = MissText();
		}
		else
		{
			if (!GachaManager.Instance.CanBuild(Convert.ToInt32(resultDatas[(int)DATABASE_ELEMENTS.INDEX])))
			{
				message_ = SuccessText();
				GachaManager.Instance.UnlockFactory(Convert.ToInt32(resultDatas[(int)DATABASE_ELEMENTS.INDEX]));
			}
			else
			{
				message_ = DoubleText();
				ActivateSupplyPanel();
				GachaManager.Instance.FundsValueChange(Convert.ToInt32(resultDatas[(int)DATABASE_ELEMENTS.SUPPLY]));
			}
		}

		text_.text = message_;
	}

	/// <summary>
	/// はずれを引いた時のメッセージ
	/// </summary>
	/// <returns>メッセージ</returns>
	string MissText()
	{
		string str_ = "残念！" + "\n"
					+ "失敗してしまった！" + "\n" + "\n"
					+ "技術の進歩に失敗は" + "\n" 
					+ "付き物だ。";
		return str_;
	}

	/// <summary>
	/// まだアンロックしてないものを引いたとき。
	/// </summary>
	/// <returns>メッセージ</returns>
	string SuccessText()
	{
		string str_ = "成功だ！" + "\n"
					+ "おめでとう！" + "\n" + "\n"
					+ "工場" + resultDatas[(int)DATABASE_ELEMENTS.NAME] + "が" + "\n" 
					+ "使用可能になったぞ！";
		return str_;
	}

	string DoubleText()
	{
		string str_ = "ふむ…。" + "\n"
					+ "あまり良い成果は" + "\n" 
					+ "得られなかった。" + "\n"
					+ "\n"
					+ "工場" + resultDatas[(int)DATABASE_ELEMENTS.NAME] + "は" + "\n"
					+ "既に使用可能だ。" + "\n"
					+ "代わりに支援金を送る";
		return str_;
	}

	void ActivateSupplyPanel()
	{
		supplyPanel_.SetActive(true);
		int supplyInt_ = Convert.ToInt32(resultDatas[(int)DATABASE_ELEMENTS.SUPPLY]);
		supplyValue.text = string.Format("{0:#,0}", supplyInt_);
	}
}
