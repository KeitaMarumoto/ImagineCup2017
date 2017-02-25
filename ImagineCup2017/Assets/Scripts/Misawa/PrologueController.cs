using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrologueController : MonoBehaviour {
    [SerializeField]
    Image prologueImage;

    [SerializeField]
    Sprite[] prologueSprites;

    [SerializeField]
    GameObject next;

    [SerializeField]
    GameObject startButton; 

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
                if(pageCount == prologueSprites.Length - 1)
                {
                    next.SetActive(false);
                    startButton.SetActive(true);
                }
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
            transform.localPosition = new Vector3(325+Mathf.Sin(num)*25,-800,0);
            yield return null;
        }
    }
}
