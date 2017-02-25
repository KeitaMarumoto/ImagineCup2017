using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PollutionMap : MonoBehaviour {

    [SerializeField]
    PollutionEventManager pollutionEventManager;

    [SerializeField]
    Material groundMat;

    [SerializeField]
    Texture[] groundTextures;

    [SerializeField]
    Material waterMat;

    [SerializeField]
    Material[] seaMats;

    [SerializeField]
    Texture[] waterTextures;

    // Use this for initialization
    void Start () {
        groundMat.mainTexture = groundTextures[0];
        waterMat.mainTexture = waterTextures[0];
        foreach (var mat in seaMats)
        {
            mat.mainTexture = waterTextures[0];
        }
    }

    public void ChangeTexture (/*float sumPollution_*/WorldStatus worldStatus) {
        if (/*sumPollution_ > 0.75*/worldStatus == WorldStatus.DIRTY) {
            groundMat.mainTexture = groundTextures[2];
            waterMat.mainTexture = waterTextures[2];
            foreach (var mat in seaMats)
            {
                mat.mainTexture = waterTextures[2];
            }
        }
        else if (/*sumPollution_ > 0.5*/worldStatus == WorldStatus.STAGNANT) {
            groundMat.mainTexture = groundTextures[1];
            waterMat.mainTexture = waterTextures[1];
            foreach (var mat in seaMats)
            {
                mat.mainTexture = waterTextures[1];
            }
        }
        else {
            groundMat.mainTexture = groundTextures[0];
            waterMat.mainTexture = waterTextures[0];
            foreach (var mat in seaMats)
            {
                mat.mainTexture = waterTextures[0];
            }
        }
    }
}
