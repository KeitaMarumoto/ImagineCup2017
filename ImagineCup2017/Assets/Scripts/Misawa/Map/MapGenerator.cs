using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    int[,] groundData = { 
//     ○ ←カメラはこの位置
        {0,1,2,0,1,2,0,1,2,0,1,2,0,1,2,0,1,2,0,1, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
    };

    [SerializeField, Tooltip("地面に貼るマテリアル")]
    Material[] groundMaterials;

    [SerializeField]
    GameObject groundParent;


    int[,] buildingData = { 
//     ○ ←カメラはこの位置
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
    };

    [SerializeField, Tooltip("地面に貼るマテリアル")]
    Material[] buildingMaterials;

    [SerializeField]
    GameObject buildingParent;

    // Use this for initialization
    void Start()
    {
        for (int i = 0;i < groundData.GetLength(0); i++)
        {
            for (int j = 0; j < groundData.GetLength(1); j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(i,0,j);
                cube.GetComponent<Renderer>().material = groundMaterials[groundData[i,j]];
                cube.transform.SetParent(groundParent.transform);
            }
        }

        for (int i = 0; i < buildingData.GetLength(0); i++)
        {
            for (int j = 0; j < buildingData.GetLength(1); j++)
            {
                if (buildingData[i, j] == 1) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    cube.transform.position = new Vector3(i + 1.0f, 0, j + 1.0f);
                    cube.transform.rotation = Quaternion.Euler(45, 45, 0);
                    cube.transform.localScale = new Vector3(1.5f, 1.5f, 0);
                    cube.GetComponent<Renderer>().material = buildingMaterials[buildingData[i, j]-1];
                    cube.transform.SetParent(buildingParent.transform);
                }
            }
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
