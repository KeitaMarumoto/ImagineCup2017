using UnityEngine;
using System.Collections;

[System.Serializable]
public class FactoryStatusData
{
    [Tooltip("生産する商品")]
    public string productName;
    [Range(1, 100), Tooltip("ランクアップコスト")]
    public int rankUpcost;
    [Range(1, 100), Tooltip("生産数")]
    public int productCount;
    [Tooltip("汚染の種類")]
    public string pollutionType;
    [Range(0,1), Tooltip("汚染度")]
    public float pollutionDegree;
}
