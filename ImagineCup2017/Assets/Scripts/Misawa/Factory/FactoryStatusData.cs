using UnityEngine;
using System.Collections;

[System.Serializable]
public class FactoryStatusData
{
    [Range(0, 3), Tooltip("生産する商品")]
    public int productType;
    [Range(1, 100), Tooltip("ランクアップコスト")]
    public int rankUpcost;
    [Range(1, 100), Tooltip("生産数")]
    public int productCount;
    [Range(1, 100), Tooltip("汚染度")]
    public float pollutionDegree;
}
