using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class FundsController : MonoBehaviour {
	[SerializeField]
	Text fundsText;

	float fundsValue = 15000.0f;

    public float FundsValue { get { return fundsValue; } }

    string strValue = "";

	void Start()
	{
		DividingValue();
		StartCoroutine(TextUpdate());
	}

	/// <summary>
	/// 資金額を変更する。
	/// </summary>
	/// <param name="diff_">加算なら(+)、減算なら(-)</param>
	public void FundsValueChange(float diff_)
	{
		ValueUpdate(diff_);
		ValueClamping();
		DividingValue();
		//TextUpdate();
	}

	/// <summary>
	/// 資金額を引数の数だけ加減算する。
	/// </summary>
	/// <param name="diff_">加算したいなら（＋）、減算したいなら（ー）</param>
	void ValueUpdate(float diff_)
	{
		fundsValue += diff_;
	}

	void ValueClamping()
	{
		if (fundsValue < 0)
		{
			fundsValue = 0.0f;
		}
	}

	/// <summary>
	/// 3桁ごとにカンマを入れる。
	/// </summary>
	void DividingValue()
	{
		int intValue_ = (int)fundsValue;
		string strValue_ = string.Format("{0:#,0}", intValue_);
		strValue = strValue_;
	}

    IEnumerator TextUpdate()
    {
        while (true)
        {
            fundsText.text = strValue;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
