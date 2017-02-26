using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System;

using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    Image backGround;

    [SerializeField]
    Image[] earth;

    [SerializeField]
    Text resultText;

    [SerializeField]
    Text gameOverText;

    [SerializeField]
    GameObject[] buttons;

    [SerializeField]
    int fadeTime;

    [SerializeField]
    DateController dateController;

    void OnEnable()
    {
        resultText.color = new Color(0f, 0f, 0f, 0f);
        gameOverText.color = new Color(0f, 0f, 0f, 0f);
        backGround.color = new Color(0f, 0f, 0f, 0f);
        foreach (var img in earth)
        {
            img.color = new Color(0f, 0f, 0f, 0f);
        }
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        StateManager.state = StateManager.State.EVENT;
        resultText.text = dateController.Date.ToString() + "週間後、\nこの星から人はいなくなりました";
        SoundManager.Instance.PlayBGM("eventBGM");
        StartCoroutine(GameOverTaskManager());
    }

    IEnumerator GameOverTaskManager()
    {
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(BackGroundFadeIn());
        yield return StartCoroutine(TextFadeIn(resultText));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(TextFadeOut(resultText));
        yield return StartCoroutine(TextFadeIn(gameOverText));
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
    }

    IEnumerator BackGroundFadeIn()
    {
        float startTime = Time.timeSinceLevelLoad;
        while (backGround.color.a < 1f)
        {
            var diff = Time.timeSinceLevelLoad - startTime;
            if (diff > fadeTime)
            {
                backGround.color = new Color(0f, 0f, 0f, 1f);
                break;
            }

            var rate = diff / fadeTime;

            foreach (var img in earth)
            {
                img.color = Color.Lerp(new Color(0f, 0f, 0f, 0f), new Color(0.5f, 0.5f, 0.5f, 1), rate);
            }

            backGround.color = Color.Lerp(new Color(0f,0f,0f,0f), new Color(0f, 0f, 0f, 1f),rate);

            yield return null;
        }
    }

    IEnumerator TextFadeIn(Text fadeText)
    {
        float startTime = Time.timeSinceLevelLoad;

        while (fadeText.color.a < 1f)
        {
            var diff = Time.timeSinceLevelLoad - startTime;
            if (diff > fadeTime)
            {
                fadeText.color = new Color(1f, 1f, 1f, 1f);
                break;
            }

            var rate = diff / fadeTime;

            fadeText.color = Color.Lerp(new Color(0f, 0f, 0f, 0f), new Color(1f, 1f, 1f, 1f), rate);

            yield return null;
        }
    }

    IEnumerator TextFadeOut(Text fadeText)
    {
        float startTime = Time.timeSinceLevelLoad;

        while (fadeText.color.a > 0f)
        {
            var diff = Time.timeSinceLevelLoad - startTime;
            if (diff > fadeTime)
            {
                fadeText.color = new Color(0f, 0f, 0f, 0f);
                break;
            }

            var rate = diff / fadeTime;

            fadeText.color = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(0f, 0f, 0f, 0f), rate);

            yield return null;
        }


    }

    public void OnclickTweetResult()
    {
        Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL("ゲーム開始から"+dateController.Date.ToString() + "週間後、星から人がいなくなりました。 #EarthEater"));
    }

    public void BackTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
