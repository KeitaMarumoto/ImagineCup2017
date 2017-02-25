using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollutionStatus : MonoBehaviour {
	[SerializeField]
	RectTransform gauge_;

    [SerializeField]
    PollutionMap pollutionMap;

	Vector3 basePosition_;

	public float SumPollution { get; private set; }
	public Dictionary<string, float> Pollutions { get; private set; }

	private void Start()
	{
		Pollutions = new Dictionary<string, float>();
		basePosition_ = gauge_.localPosition;
		SetupData();
		StartCoroutine(DecreasePollution());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SetupData();
		}
	}

	public void SetPollution(string key_, float value_)
	{
		Pollutions[key_] += value_;
		Pollutions[key_] = ClumpingPollution(Pollutions[key_]);
        SumPollutionsData();
    }

    void SetupData()
	{
		Pollutions["CO2"] = 0.0f;
		Pollutions["PM2.5"] = 0.0f;
		Pollutions["CO"] = 0.0f;
		SumPollutionsData();
	}

	IEnumerator DecreasePollution()
	{
		while (true)
		{
			yield return new WaitForSeconds(2.0f);
			Debug.Log(SumPollution);
			List<string> keys_ = new List<string>();
			foreach(var key_ in Pollutions)
			{
				keys_.Add(key_.Key);
			}

			foreach (var key_ in keys_)
			{
				float updateValue_ = Pollutions[key_] - 0.01f;
				updateValue_ = ClumpingPollution(updateValue_);
				Pollutions[key_] = updateValue_;
			}

			SumPollutionsData();
		}
	}

	void SumPollutionsData()
	{
		float result_ = 0.0f;
		foreach (var value_ in Pollutions)
		{
			result_ += value_.Value;
		}
		SumPollution = ClumpingPollution(result_);
        pollutionMap.ChangeTexture(SumPollution);
        UpdateGauge();
	}

	float ClumpingPollution(float data_)
	{
		if (data_ > 1.0f)
		{
			data_ = 1.0f;
			return data_;
		}
		if (data_ < 0.0f)
		{
			data_ = 0.0f;
			return data_;
		}
		return data_;
	}

	void UpdateGauge()
	{
		Debug.Log("rect.height : " + gauge_.rect.height);
		gauge_.localPosition = new Vector3(basePosition_.x,
										   basePosition_.y + gauge_.rect.height * SumPollution,
										   basePosition_.z);
	}
}
