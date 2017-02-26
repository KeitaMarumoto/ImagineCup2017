using UnityEngine;
using System.Collections;

public class GachaManager : MonoBehaviour {
	private static GachaManager instance;
	public static GachaManager Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance == null) instance = this;
	}

	public Transform getParent()
	{
		return transform;
	}
}
