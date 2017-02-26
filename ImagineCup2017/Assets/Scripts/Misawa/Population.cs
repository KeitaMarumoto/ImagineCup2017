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

    public int addPopulation { set; get; }
    // Use this for initialization
    void Start () {
        addPopulation = 0;
        population = 1000;
        //StartCoroutine(ChangePopulation());
    }
	
    public void ChangePopulation()
    {
        population += (int)((rate.Evaluate(pollutionStatus.SumPollution) - 0.3) * 100 * addPopulation);
        populationText.text = population.ToString();
        addPopulation = 0;
    }
}
