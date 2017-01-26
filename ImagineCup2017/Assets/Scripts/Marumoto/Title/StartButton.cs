using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public void PushStartButton()
	{
		SceneManager.LoadScene("GameMain");
	}
}
