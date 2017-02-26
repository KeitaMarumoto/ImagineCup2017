using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour {
	private static GachaManager instance;
	public static GachaManager Instance
	{
		get { return instance; }
	}

	[SerializeField]
	FactoryManager factoryManager;
	[SerializeField]
	FundsController fundscontroller;

	List<GameObject> popupes = new List<GameObject>();

	void Awake()
	{
		if (instance == null) instance = this;
	}

	/// <summary>
	/// デバッグ用
	/// </summary>
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			fundscontroller.FundsValueChange(20000);
		}
	}

	public Transform getParent()
	{
		return transform;
	}

	public void AddPopup(GameObject obj_)
	{
		popupes.Add(obj_);
	}

	public void RemovePopup(int index_)
	{
		popupes.RemoveAt(index_);
	}

	public void DestroyAllPopup()
	{
		foreach(GameObject popup_ in popupes)
		{
			Destroy(popup_);
		}
		popupes.Clear();
	}

	public bool CanBuild(int index_)
	{
		return factoryManager.CanBuild(index_);
	}

	public void UnlockFactory(int index_)
	{
		factoryManager.Unrock(index_);
	}

	public void FundsValueChange(int value_)
	{
		fundscontroller.FundsValueChange(value_);
	}

	public int GetFunds()
	{
		return fundscontroller.GetFunds();
	}
}
