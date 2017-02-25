using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 背景の空オブジェクトの管理をするクラス。
/// </summary>
public class BackgroundSky : MonoBehaviour
{
	[SerializeField]
	List<Sprite> backgrounds;
	[SerializeField]
	Image image;
	
	void Awake()
	{
		image.sprite = backgrounds[(int)WorldStatus.CLEAR];
	}

	/// <summary>
	/// 引数から空の色を変更する。
	/// </summary>
	/// <param name="skyStatus_">SkyStatus(enum)の値を設定</param>
	public void ChangeBackground(WorldStatus skyStatus_)
	{
		image.sprite = backgrounds[(int)skyStatus_];
	}
}
