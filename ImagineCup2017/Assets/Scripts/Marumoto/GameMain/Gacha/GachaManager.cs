using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour {
	private static GachaManager instance;
	public static GachaManager Instance
	{
		get { return instance; }
	}

	List<GameObject> popupes = new List<GameObject>();

	void Awake()
	{
		if (instance == null) instance = this;
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
}
