using UnityEngine;
using System.Collections;

public class GachaButtonYes : MonoBehaviour {
	[SerializeField]
	GameObject gachaPopup;

	public void CreatePopup()
	{
		GameObject refObj = (GameObject)Instantiate(gachaPopup, GachaManager.Instance.getParent(), false);
		GachaManager.Instance.AddPopup(refObj);
	}

	public void PayFundsGacha()
	{
		SoundManager.Instance.PlaySE("cash");
		GachaManager.Instance.FundsValueChange(-100000);
	}
}
