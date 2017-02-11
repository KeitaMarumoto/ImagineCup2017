using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollutionStatus : MonoBehaviour {
	[SerializeField]
	Transform gauge_;

	public float SumPollution { get; private set; }
	public Dictionary<string, float> Pollutions { get; set; }

	private void Start()
	{
		Pollutions = new Dictionary<string, float>();
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

	void SetupData()
	{
		Pollutions["CO2"] = 33.0f;
		Pollutions["PM2.5"] = 33.0f;
		Pollutions["CO"] = 33.0f;
		SumPollutionsData();
	}

	IEnumerator DecreasePollution()
	{
		while (true)
		{
			yield return new WaitForSeconds(2.0f);

			List<string> keys_ = new List<string>();
			foreach(var key_ in Pollutions)
			{
				keys_.Add(key_.Key);
			}

			foreach (var key_ in keys_)
			{
				float updateValue_ = Pollutions[key_] - 3.0f;
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
		UpdateGauge();
	}

	float ClumpingPollution(float data_)
	{
		if (data_ > 100.0f)
		{
			data_ = 100.0f;
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
		gauge_.localScale = new Vector3(gauge_.localScale.x,
									    SumPollution / 100.0f,
									    gauge_.localScale.z);
	}
}
