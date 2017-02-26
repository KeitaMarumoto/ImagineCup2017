using UnityEngine;
using System;
using System.Collections;

public class HTTPService : MonoBehaviour {
	private static HTTPService instance;
	public static HTTPService Instance
	{
		get { return instance; }
	}
	// Use this for initialization
	void Awake() {
		if (instance == null) instance = this;
	}
	
	public void Request(string url_, Action<string> callback_)
	{
		StartCoroutine(RequestImpl(url_, callback_));
	}

	IEnumerator RequestImpl(string url_, Action<string> callback_)
	{
		WWW www = new WWW(url_);
		yield return www;
		if (www.error == null)
		{
			callback_(www.text);
		}
	}
}
