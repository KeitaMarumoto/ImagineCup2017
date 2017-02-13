using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Population : MonoBehaviour {

    [SerializeField]
    Text populationText;

    [SerializeField]
    AnimationCurve rate;

    [SerializeField]
    PollutionStatus pollutionStatus;

    public int population { set; get; }

    [SerializeField,Range(0,1),Tooltip("デバッグ用")]
    float testPollution = 0;

    // Use this for initialization
    void Start () {
        population = 1000;
        StartCoroutine(ChangePopulation());
    }
	
    IEnumerator ChangePopulation()
    {
        while (true)
        {
            population += (int)((rate.Evaluate(pollutionStatus.SumPollution) -0.5)*100);
            populationText.text = population.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Update is called once per frame
    void Update () {
        
	}
}
