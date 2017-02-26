using UnityEngine;
using System.Collections;

public class GachaResultCloseButton : MonoBehaviour {
	public void CloseAllPopup()
	{
		GachaManager.Instance.DestroyAllPopup();
	}
}
