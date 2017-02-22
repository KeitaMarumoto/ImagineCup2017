using UnityEngine;

/// <summary>
/// 個別のタブの挙動と状態を管理するクラス。
/// </summary>
public class TabControl : MonoBehaviour
{
    //Easingに使う
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField, Tooltip("目的地までの距離")]
    private Vector3 endPositionDistance = new Vector3();

    [SerializeField, Tooltip("目的地にたどり着くまでの時間"), Range(0.1f, 5.0f)]
    private float moveTime;

    //Easingのエンドポジション
    private Vector3 endPosition = new Vector3();

    //Easingのスタートポジション。0が右目、1が左目用
    private Vector3 startPosition = new Vector3();

    //二種類のEasing関数を使い分けるための要素番号
    const int GOING_NUM = 0;
    const int RETURN_NUM = 1;

    //Easingを始めていいかどうか
    public bool[] canEasing = new bool[2];

    //Easingをするために必要な起動時間
    private float startTime;

    public bool isDisplay { get; private set; }

    void Start()
    {
        isDisplay = false;

        //Easingを始めるポジションの初期化
        startPosition = transform.localPosition;

        //エンドポジションの決定
        endPosition = transform.localPosition + endPositionDistance;
    }

    void Update()
    {
        if (canEasing[GOING_NUM] && !isDisplay)
        {
            StartEasing(startTime);
        }

        if (canEasing[RETURN_NUM] && isDisplay)
        {
            ReverceEasing(startTime);
        }
    }

    //元の位置に戻す処理
    public void OnClickClosing()
    {
		if (isDisplay)
		{
			canEasing[RETURN_NUM] = true;
			startTime = Time.timeSinceLevelLoad;
            StateManager.state = StateManager.State.PRODUCTION;
        }
    }

	public void ActiveEasing()
	{
		if (!canEasing[GOING_NUM])
		{
			canEasing[GOING_NUM] = true;
			startTime = Time.timeSinceLevelLoad;
		}
	}

    bool RayCast(string tagName_)
    {
        //カメラの場所からポインタの場所に向かってレイを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        //レイが何か当たっているかを調べる
        if (Physics.Raycast(ray, out hit))
        {
            //当たったオブジェクトを格納
            GameObject obj = hit.collider.gameObject;

            if (obj.CompareTag(tagName_))
            {
                return true;
            }
        }


        return false;
    }

    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    void StartEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            transform.position = endPosition;
            canEasing[GOING_NUM] = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(startPosition, endPosition, pos);

        //Easing終了時の処理を記述する
        if (rate >= 1)
        {
            isDisplay = true;
        }
    }

    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    //TIPS:元のポジションに戻すために行っているので、StartとEndが逆になっている
    void ReverceEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            transform.position = startPosition;
            canEasing[RETURN_NUM] = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(endPosition, startPosition, pos);

        //Easing終了時の処理を記述する
        if (rate >= 1)
        {
            isDisplay = false;
        }
    }
}