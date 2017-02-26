using UnityEngine;
using UnityEngine.UI;
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

	List<string> resultDatas = new List<string>();
	
	void Start () {
		GetFactory();
	}

	void JsonPersing(string text_)
	{
		var json = Json.Deserialize(text_) as List<object>;
		Debug.Log(json);
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
		Debug.Log(jsonText);
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

		text_.text = message_;
	}

	string MissText()
	{
		string str_ = "残念！" + "\n"
					+ "失敗してしまった！" + "\n" + "\n"
					+ "技術の進歩に失敗は" + "\n" 
					+ "付き物だ。";
		return str_;
	}
}
