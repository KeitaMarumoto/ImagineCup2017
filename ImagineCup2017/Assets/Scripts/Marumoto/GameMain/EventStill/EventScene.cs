using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum FadeState
{
	BEGIN = 0,
	END
}

public class EventScene : MonoBehaviour {
	[SerializeField]
	GameObject eventStillObj;

	[SerializeField]
	GameObject closeButtonObj;

	[SerializeField]
	Button closeButton;

	[SerializeField]
	Image eventStill;

	[SerializeField]
	Image fade;

	[SerializeField]
	float fadeSpeed;

	[SerializeField]
	float fadeWaitTime;

	void Start()
	{
		StartCoroutine(EventStream());
	}

	IEnumerator EventStream()
	{
		yield return StartCoroutine(FadeInOut("eventBGM", FadeState.BEGIN));
		fade.raycastTarget = false;
		yield return StartCoroutine(InputWait());
		yield return StartCoroutine(FadeInOut("mainBGM", FadeState.END));
	}

	IEnumerator FadeInOut(string bgmName_, FadeState state_)
	{
		yield return StartCoroutine(FadeAlphaIncrese());
		SoundManager.Instance.StopBGM();

		yield return new WaitForSeconds(fadeWaitTime / 2);
		EventStillActivate(state_);
		PollutionEventManager.Instance.UpdateWorldImpl();
		yield return new WaitForSeconds(fadeWaitTime / 2);

		SoundManager.Instance.PlayBGM(bgmName_);
		yield return StartCoroutine(FadeAlphaDecrese());
	}

	IEnumerator InputWait()
	{
		bool throwOnce = false;
		bool breakFunction = false;
		while (true)
		{
			if (!throwOnce)
			{
				throwOnce = true;
				yield return new WaitForSeconds(3.0f);
				closeButtonObj.SetActive(true);
				closeButton.onClick.AddListener(() => { breakFunction = true; });
			}
			if (breakFunction)
			{
				fade.raycastTarget = true;
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator FadeAlphaIncrese()
	{
		float alpha_ = 0.0f;
		while (true)
		{
			alpha_ += fadeSpeed;
			fade.color = new Color(0, 0, 0, alpha_);

			if (alpha_ >= 1.0f)
			{
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator FadeAlphaDecrese()
	{
		float alpha_ = fade.color.a;
		while (true)
		{
			alpha_ -= fadeSpeed;
			fade.color = new Color(0, 0, 0, alpha_);

			if (alpha_ <= 0.0f)
			{
				yield break;
			}
			yield return null;
		}
	}

	void EventStillActivate(FadeState state_)
	{
		if (state_ == FadeState.BEGIN)
		{
			eventStillObj.SetActive(true);
			eventStill.sprite = PollutionEventManager.Instance.GetEventStills();
		}
		else
		{
			eventStillObj.SetActive(false);
		}
	}
}
