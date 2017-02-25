using UnityEngine;
using System.Collections;

[System.Serializable]
public class FactoryStatusData
{
    [Tooltip("生産する商品")]
    public string productName;
    [Tooltip("ランクアップコスト")]
    public int rankUpcost;
    [Tooltip("生産数")]
    public int productCount;
    [Tooltip("汚染の種類")]
    public string pollutionType;
    [Range(0,0.1f), Tooltip("汚染度")]
    public float pollutionDegree;
}
