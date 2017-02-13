using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PollutionMap : MonoBehaviour {

    [SerializeField]
    Material groundMat;

    [SerializeField]
    Texture[] groundTextures;

    [SerializeField]
    Material waterMat;

    [SerializeField]
    Texture[] waterTextures;

    // Use this for initialization
    void Start () {
        groundMat.mainTexture = groundTextures[0];
    }

    public void ChangeTexture (float sumPollution_) {
        if (sumPollution_ > 0.75) {
            groundMat.mainTexture = groundTextures[2];
        }
        else if (sumPollution_ > 0.5) {
            groundMat.mainTexture = groundTextures[1];
        }
        else {
            groundMat.mainTexture = groundTextures[0];
        }
    }
}
