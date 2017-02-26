using UnityEngine;

public class GachaButtonNo : MonoBehaviour {
	public void ClosingPopup(int index_)
	{
		GachaManager.Instance.RemovePopup(index_);
		Destroy(gameObject);
	}
}
