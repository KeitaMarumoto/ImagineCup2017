using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IndustryTabManager : MonoBehaviour {
	private static IndustryTabManager instance;
	public static IndustryTabManager Instance
	{
		get { return instance; }
	}

	[SerializeField]
	List<GameObject> tabs;
	[SerializeField]
	List<TabControl> tabControls;

	Vector3 beginMousePos;
	Vector3 endMousePos;

	//二種類のEasing関数を使い分けるための要素番号
	const int GOING_NUM = 0;
	const int RETURN_NUM = 1;

	public bool isPullingTab = false;

	void Awake()
	{
		if (instance == null) { instance = this; }
	}

	void LateUpdate()
	{
		DragBegin();
		DragEnd();
	}

	/// <summary>
	/// どれか一つでもTabが開いているかどうか。
	/// </summary>
	/// <returns></returns>
	public bool IsAnyDisplaying()
	{
		foreach(TabControl _tabControl in tabControls)
		{
			if (_tabControl.isDisplay == true) return true;
		}
		return false;
	}

	/// <summary>
	/// タブを閉じる。
	/// </summary>
	/// <param name="_hit3D"></param>
	void ClosingTab(RaycastHit _hit3D)
	{
		const int _null = 3;
		int _tabIndex = SearchTabIndex(_hit3D);
		if (_tabIndex == _null) return;
		if (_hit3D.transform.tag == "HoldTab") return;

		tabControls[_tabIndex].OnClickLightIsOut();
	}

	/// <summary>
	/// タブを開く。
	/// </summary>
	/// <param name="_hit3D"></param>
	void DisplayingTab(RaycastHit _hit3D)
	{
		const int _null = 3;
		int _tabIndex = SearchTabIndex(_hit3D);
		if (_tabIndex == _null) return;

		tabControls[_tabIndex].ActiveEasing();
	}

	/// <summary>
	/// どのタブにタップしたか。
	/// </summary>
	/// <param name="_hit3D"></param>
	/// <returns></returns>
	int SearchTabIndex(RaycastHit _hit3D)
	{
		if (_hit3D.transform.name == "PurchaseTab") return 0;
		if (_hit3D.transform.name == "RemodelingTab") return 1;
		else return 3;
	}

	/// <summary>
	/// タップしたポイントにレイキャスト。
	/// </summary>
	/// <returns>当たったらRaycastHitを返す。</returns>
	RaycastHit Raycast()
	{
		RaycastHit hit3D;
		Ray ray3D = Camera.main.ScreenPointToRay(beginMousePos);

		Debug.DrawRay(ray3D.origin, ray3D.direction, new Color(1, 0, 0), 3, false);

		if (Physics.Raycast(ray3D, out hit3D))
		{
			Transform _objectHit = hit3D.transform;
			if (_objectHit.tag == "IndustoryTab")
			{
				return hit3D;
			}
		}

		return hit3D;
	}

	/// <summary>
	/// ドラッグ判定の条件を満たしたかどうか。
	/// </summary>
	/// <returns>満たせばtrue</returns>
	bool IsDrag()
	{
		Debug.Log(endMousePos.y - beginMousePos.y);
		if (endMousePos.y - beginMousePos.y >= 60.0f) return true;
		return false;
	}

	/// <summary>
	/// 指でタップおよびマウスでPushした瞬間。
	/// </summary>
	void DragBegin()
	{
		if (!IsTappingDown()) return;
		SetupBeginPos();

		RaycastHit hit3D = Raycast();
		if (hit3D.collider)
		{
			Debug.Log(hit3D.collider.tag);
			if (hit3D.transform.tag == "HoldTab")
			{
				ClosingTab(hit3D);
			}
			if (hit3D.transform.tag == "IndustryTab")
			{
				hit3D.transform.SetAsLastSibling();
				isPullingTab = true;
			}
		}
	}

	/// <summary>
	/// 指およびマウスをPullした瞬間。
	/// </summary>
	void DragEnd()
	{
		if (!IsTappingUp()) return;
		isPullingTab = false;
		SetupEndPos();

		if (!IsDrag()) return;

		RaycastHit hit3D = Raycast();
		if (hit3D.collider)
		{
			if (hit3D.transform.tag == "IndustryTab")
			{
				hit3D.transform.SetAsLastSibling();
				DisplayingTab(hit3D);
			}
		}
	}

	/// <summary>
	/// 左クリックかシングルタップされたかどうか。
	/// </summary>
	/// <returns></returns>
	bool IsTappingDown()
	{
#if UNITY_STANDALONE
		if (Input.GetMouseButtonDown(0)) return true;

#elif UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch _touch = Input.GetTouch(0);
			if (_touch.phase == TouchPhase.Began) return true;
		}
#endif
		return false;
	}

	/// <summary>
	/// 左クリックが離されたか、タップした指を離されたかどうか。
	/// </summary>
	/// <returns></returns>
	bool IsTappingUp()
	{
#if UNITY_STANDALONE
		if (Input.GetMouseButtonUp(0)) return true;

#elif UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch _touch = Input.GetTouch(0);
			if (_touch.phase == TouchPhase.Ended) return true;
		}
#endif
		return false;
	}

	void SetupBeginPos()
	{
#if UNITY_STANDALONE
		beginMousePos = Input.mousePosition;

#elif UNITY_ANDROID
		Touch _touch = Input.GetTouch(0);
		beginMousePos = _touch.position;
#endif
	}

	void SetupEndPos()
	{
#if UNITY_STANDALONE
		endMousePos = Input.mousePosition;
#elif UNITY_ANDROID
		Touch _touch = Input.GetTouch(0);
		endMousePos = _touch.position;
#endif
	}
}
