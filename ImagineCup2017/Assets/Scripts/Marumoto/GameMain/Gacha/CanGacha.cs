using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanGacha : MonoBehaviour {
	[SerializeField]
	Button button;

	void Start()
	{
		StartCoroutine(CheckCanGacha());
	}

	IEnumerator CheckCanGacha()
	{
		if (GachaManager.Instance.GetFunds() >= 100000)
		{
			button.interactable = true;
		}
		else
		{
			button.interactable = false;
		}

		yield return new WaitForSeconds(0.2f);
	}
}
