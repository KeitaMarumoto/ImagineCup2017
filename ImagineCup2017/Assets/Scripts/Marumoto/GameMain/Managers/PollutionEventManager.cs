using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 世界の状態を表す。
/// </summary>
public enum WorldStatus
{
	CLEAR = 0,
	STAGNANT,
	DIRTY
}

/// <summary>
/// 汚染度に応じた変更を管理する。
/// </summary>
public class PollutionEventManager : MonoBehaviour {
	private static PollutionEventManager instance;
	public static PollutionEventManager Instance
	{
		get { return instance; }
	}

	[SerializeField]
	BackgroundSky backgroundSky;
	[SerializeField]
	PollutionStatus pollutionStatus;
    [SerializeField]
    PollutionMap pollutionMap;
	[SerializeField]
	List<Sprite> eventStills;
	[SerializeField]
	GameObject eventPrefab;
	[SerializeField]
	Transform eventStillParent;

	List<string> eventNewsText = new List<string>();

	WorldStatus worldStatus;
	WorldStatus oldWorldStatus;

	void Awake()
	{
		if (instance == null) instance = this;
		worldStatus = WorldStatus.CLEAR;
		oldWorldStatus = worldStatus;
		EventTextSetup();
	}

	void Start()
	{
		StartCoroutine(UpdateWorld());
	}

	/// <summary>
	/// 一定時間毎にWorldStatusに変更があるか監視し、それに応じて世界の状態を変遷させる。
	/// </summary>
	/// <returns>オーバーヘッド減少のため一定時間待つ</returns>
	IEnumerator UpdateWorld()
	{
		while (true)
		{
			UpdateWorldStatus();

			if (worldStatus != oldWorldStatus)
			{
				EventStart();
				oldWorldStatus = worldStatus;
            }
			yield return new WaitForSeconds(0.3f);
		}
	}

	public void UpdateWorldImpl()
	{
		backgroundSky.ChangeBackground(worldStatus);
		pollutionMap.ChangeTexture(worldStatus);
	}

	/// <summary>
	/// 汚染度に応じてworldStatusを更新する。
	/// </summary>
	void UpdateWorldStatus()
	{
		if (IsClear())
		{
			worldStatus = WorldStatus.CLEAR;
		}
		else if (IsStagnant())
		{
			worldStatus = WorldStatus.STAGNANT;
		}
		else if (IsDirty())
		{
			worldStatus = WorldStatus.DIRTY;
		}
	}

	bool IsClear()
	{
		if (pollutionStatus.SumPollution < 0.4f)
		{
			return true;
		}
		return false;
	}

	bool IsStagnant()
	{
		if ((0.4f <= pollutionStatus.SumPollution)
			&& (pollutionStatus.SumPollution < 0.8f))
		{
			return true;
		}
		return false;
	}

	bool IsDirty()
	{
		if (0.8f <= pollutionStatus.SumPollution)
		{
			return true;
		}
		return false;
	}

	public Sprite GetEventStills()
	{
		if (worldStatus == WorldStatus.CLEAR) return null;
		Sprite sprite_;
		sprite_ = eventStills[(int)worldStatus - 1];
		return sprite_;
	}

	void EventStart()
	{
		if ((oldWorldStatus == WorldStatus.CLEAR) && (worldStatus == WorldStatus.STAGNANT))
		{
			Instantiate(eventPrefab, eventStillParent, false);
		}
		else if ((oldWorldStatus == WorldStatus.STAGNANT) && (worldStatus == WorldStatus.DIRTY))
		{
			Instantiate(eventPrefab, eventStillParent, false);
		}
	}

	void EventTextSetup()
	{
		string str_1 = "国が豊かになりましたが、" + "\n"
					 + "環境問題が深刻になっています。";

		string str_2 = "地球全体が汚染されています。" + "\n"
					 + "生物が生きられる環境ではありません！";

		eventNewsText.Add(str_1);
		eventNewsText.Add(str_2);
	}

	public string GetNewsText()
	{
		if (worldStatus == WorldStatus.CLEAR) return null;
		return eventNewsText[(int)worldStatus - 1];
	}
}
