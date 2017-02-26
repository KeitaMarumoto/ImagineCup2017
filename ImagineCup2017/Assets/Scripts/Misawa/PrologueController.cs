using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PrologueController : MonoBehaviour {
    [SerializeField]
    Image prologueImage;

    [SerializeField]
    Sprite[] prologueSprites;

    [SerializeField]
    Text text;

    [SerializeField]
    GameObject next;

    [SerializeField]
    GameObject startButton;

    string[] prologueText = {
        "ある国の王さまは、\nどうやって国を今より\nよくできるかなやんでいました",
        "王さまの元気がないことを\n知ったネコのリアンは、\n王さまに声をかけました。",
        "王さまはこう言いました。\n｢もっと皆を幸せにしたい。\n便利な国にしたい｣と。",
        "リアンは胸を張って答えました。\n｢分かりました！\n僕がなんとかしてみせます！｣"};

    int pageCount;

    // Use this for initialization
    void Start () {
        next.SetActive(true);
        startButton.SetActive(false);
        pageCount = 0;
        prologueImage.sprite = prologueSprites[pageCount];
        SoundManager.Instance.PlayBGM("eventBGM");
        StartCoroutine(NextMove());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if ((pageCount + 1) < prologueSprites.Length)
            {
                pageCount++;
                prologueImage.sprite = prologueSprites[pageCount];
                text.text = prologueText[pageCount];
                if (pageCount == prologueSprites.Length-1)
                {
                    next.SetActive(false);
                }
            }
            else
            {
                SceneManager.LoadScene("GameMain");
            }
        }
	}

    IEnumerator NextMove()
    {
        float num = 0.0f;
        RectTransform transform = next.GetComponent<RectTransform>();
        while (true)
        {
            if (next.activeInHierarchy == false) break;
            Debug.Log("asdfg");
            num +=0.1f;
            transform.localPosition = new Vector3(400+Mathf.Sin(num)*10,-850,0);
            yield return null;
        }
    }
}
