using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryManager : MonoBehaviour {
    [SerializeField]
    UIController ui;

    [SerializeField, Space(15)]
    public int maxFactoryRank;

    [SerializeField]
    FactoryData[] factoryData;

    /// <summary>
    /// 各種類各ランクごとの工場の数[工場の種類,工場ランク]
    /// </summary>
    /// 　　  |1|2|3|4|5|
    /// 工場A |3|0|0|2|0|
    /// 工場B |0|1|0|0|0|
    /// 工場C |0|0|4|0|0|
    int[,] factoriesCount;

    //Dictionary<string, int> factorieType = new Dictionary<string, int>();

    // Use this for initialization
    void Start () {

        //工場名で配列番号にアクセスできるようにするためにする
        /*for(int i = 0;i< factoryData.Length; i++)
        {
            factorieType.Add(factoryData[i].factoryName, i);
        }*/

        //工場数の初期化
        factoriesCount = new int[factoryData.Length, maxFactoryRank];
        for (int i = 0; i < factoriesCount.GetLength(0); i++)
        {
            for (int j = 0; j < factoriesCount.GetLength(1); j++)
            {
                factoriesCount[i, j] = 0;
            }
        }
    }

    //テスト用
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Unrock(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Unrock(3);
        }
    }

    /// <summary>
    /// 工場数を返す
    /// </summary>
    /// <param name="factoryName">工場名</param>
    /// <param name="rank">工場ランク</param>
    /// <returns>その工場がたっている数</returns>
    public int GetFactoriesCount(int factoryID,int rank){
        return factoriesCount[factoryID, rank-1];
    }

    /// <summary>
    /// その種類の工場が建てられるか？
    /// </summary>
    /// <param name="factoryID">工場番号</param>
    /// <returns></returns>
    public bool CanBuild(int factoryID)
    {
        return factoryData[factoryID].canBuild;
    }

    /// <summary>
    /// その種類の工場を建てられるようにする
    /// </summary>
    /// <param name="factoryID">工場番号</param>
    public void Unrock(int factoryID)
    {
        ui.setBuidStatusUI(factoryID);
        factoryData[factoryID].canBuild = true;
    }

    /// <summary>
    /// 工場建設
    /// </summary>
    /// <param name="factoryName">建てたい工場名</param>
    public int Construction(int factoryID)
    {
        if (factoryID < 0 && factoryID >= factoriesCount.GetLength(0)) return 0;
        factoriesCount[factoryID, 0]++;
        return factoryData[factoryID].factoryStatus[0].rankUpcost;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factoryID"></param>
    /// <returns></returns>
    public FactoryStatusData GetFactoryStatus(int factoryID,int rank)
    {
        return factoryData[factoryID].factoryStatus[rank];
    }

    /// <summary>
    /// 工場のランクアップ
    /// </summary>
    /// <param name="factoryName">ランクを上げたい工場の工場名</param>
    /// <param name="rank">ランクを上げたい工場の現在の工場ランク</param>
    public int RankUp(int factoryID, int rank)
    {
        if (factoryID < 0 && factoryID >= factoriesCount.GetLength(0)) return 0;
        if (rank < factoriesCount.GetLength(1) && rank > 0 && factoriesCount[factoryID, rank - 1] >= 0)
        {
            factoriesCount[factoryID, rank-1]--;
            factoriesCount[factoryID, rank]++;
            Debug.Log(factoryData[factoryID].factoryName + "の"+rank+"をランクアップ");
            return factoryData[factoryID].factoryStatus[rank].rankUpcost;
        }
        else
        {
            Debug.LogWarning(factoryData[factoryID].factoryName + "は存在しません");
            return 0;
        }
    }

    /// <summary>
    /// 生産
    /// </summary>
    /// <returns>商品の種類ごとの生産した個数</returns>
    public Dictionary<string, int> Make()
    {
        Dictionary<string, int> productCount = new Dictionary<string, int>();

        for (int i = 0; i < factoryData.Length; i++)
        {
            for (int j = 0; j < maxFactoryRank; j++)
            {
                if (productCount.ContainsKey(factoryData[i].factoryStatus[j].productName))
                    productCount[factoryData[i].factoryStatus[j].productName] += factoryData[i].factoryStatus[j].productCount * factoriesCount[i, j];
                else
                    productCount.Add(factoryData[i].factoryStatus[j].productName, factoryData[i].factoryStatus[j].productCount * factoriesCount[i, j]);
            }
        }
        return productCount;
    }

    /// <summary>
    /// 生産時に発生した汚染
    /// </summary>
    /// <returns>汚染度</returns>
    public Dictionary<string, float> MakePollution()
    {
        Dictionary<string, float> pollution = new Dictionary<string, float>();// = new int[4];//ここの4は商品の種類数

        for (int i = 0; i < factoryData.Length; i++)
        {
            for (int j = 0; j < maxFactoryRank; j++)
            {
                if (pollution.ContainsKey(factoryData[i].factoryStatus[j].pollutionType))
                    pollution[factoryData[i].factoryStatus[j].pollutionType] += factoryData[i].factoryStatus[j].pollutionDegree * 0.01f * factoriesCount[i, j];
                else
                    pollution.Add(factoryData[i].factoryStatus[j].pollutionType, factoryData[i].factoryStatus[j].pollutionDegree * 0.01f * factoriesCount[i, j]);
            }
        }
        return pollution;
    }

    /// <summary>
    /// 全工場の維持費の総計を調べる
    /// </summary>
    /// <returns>維持費の総計</returns>
    public int PayMaintenance()
    {
        int num = 0;
        for (int factoryID = 0; factoryID < factoriesCount.GetLength(0); factoryID++)
        {
            for (int rank = 0; rank < factoriesCount.GetLength(1); rank++)
            {
                num += (factoryData[factoryID].factoryStatus[rank].rankUpcost / 10) * factoriesCount[factoryID, rank];
            }
        }
        return num;
    }
}