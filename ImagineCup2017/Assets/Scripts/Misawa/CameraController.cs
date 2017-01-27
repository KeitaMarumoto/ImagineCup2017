using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Camera cam;

    [SerializeField,Tooltip("ズームアウトできる最高値")]
    float maxZoomSize;

    [SerializeField, Tooltip("ズームインできる最低値")]
    float minZoomSize;

/*    
    [SerializeField, Tooltip("上に移動できる最台値")]
    float upperLimit;
    [SerializeField, Tooltip("下に移動できる最台値")]
    float lowerLimit;
    [SerializeField, Tooltip("右に移動できる最台値")]
    float rightLimit;
    [SerializeField, Tooltip("左に移動できる最台値")]
    float leftLimit;
*/
    
    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        ZoomCamera();
        if (Input.GetKeyDown(KeyCode.UpArrow)) MoveVerticalCamera(1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) MoveVerticalCamera(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveHorizontalCamera(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveHorizontalCamera(-1);
    }

    void ZoomCamera()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        if (zoom < 0 && cam.orthographicSize > minZoomSize)
            cam.orthographicSize -= 1.0f;
        else if (zoom > 0 && cam.orthographicSize < maxZoomSize)
            cam.orthographicSize += 1.0f;
    }

    public void MoveVerticalCamera(float move)
    {
        transform.Translate(new Vector3(move,0,move),Space.World);
    }

    public void MoveHorizontalCamera(float move)
    {
        transform.Translate(new Vector3(move, 0, -move), Space.World);
    }
}
