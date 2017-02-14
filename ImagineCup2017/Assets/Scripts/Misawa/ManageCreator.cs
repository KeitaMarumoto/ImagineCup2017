using UnityEngine;
using System.Collections;

public class ManageCreator : MonoBehaviour {

    //Awakeより前に自動でマネージャーを作成する
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreatorManager()
    {
#if UNITY_EDITOR
        Screen.SetResolution(540, 960, false, 60);
        Debug.Log("resize");
#elif UNITY_ANDROID
        Screen.fullScreen = true;
#endif

    }
}
