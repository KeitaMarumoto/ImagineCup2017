using UnityEngine;
using System.Collections;

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

	WorldStatus worldStatus;
	WorldStatus oldWorldStatus;

	void Awake()
	{
		if (instance == null) instance = this;
		worldStatus = WorldStatus.CLEAR;
		oldWorldStatus = worldStatus;
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
				oldWorldStatus = worldStatus;

				backgroundSky.ChangeBackground(worldStatus);
			}
			yield return new WaitForSeconds(0.3f);
		}
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
}
