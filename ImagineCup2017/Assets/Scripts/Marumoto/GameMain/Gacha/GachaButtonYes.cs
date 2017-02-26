using UnityEngine;
using System.Collections;

public class GachaButtonYes : MonoBehaviour {
	[SerializeField]
	GameObject gachaPopup;

	public void CreatePopup()
	{
		Instantiate(gachaPopup, GachaManager.Instance.getParent(), false);
	}
}
