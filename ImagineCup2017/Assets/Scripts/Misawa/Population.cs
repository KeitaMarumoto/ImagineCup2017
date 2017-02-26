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

    [SerializeField]
    GameObject gameOverManager;

    public int population { set; get; }

    public int addPopulation { set; get; }
    // Use this for initialization
    void Start () {
        addPopulation = 0;
        population = 1000;
        populationText.text = population.ToString();
        //StartCoroutine(ChangePopulation());
    }
	
    public void ChangePopulation()
    {
        population += (int)((rate.Evaluate(pollutionStatus.SumPollution) - 0.8) * 1000 + (addPopulation * rate.Evaluate(pollutionStatus.SumPollution) * 10));
        addPopulation = 0;
        if (population <= 0)
        {
            population = 0;
            gameOverManager.SetActive(true);
        }
        populationText.text = population.ToString();

    }
}
