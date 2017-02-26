using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DateController : MonoBehaviour {

    [SerializeField]
    Text dateText;

    [SerializeField]
    Population population;

    [SerializeField]
    FactoryController factoryController;

    int date;
    public int Date { get { return date; } }

	// Use this for initialization
	void Start () {
        date = 0;
        StartCoroutine(DataUpdate());
	}
	

    IEnumerator DataUpdate()
    {
        while (true)
        {
            if (StateManager.state == StateManager.State.PRODUCTION)
            {
                yield return new WaitForSeconds(6f);
                date++;
                population.ChangePopulation();
                factoryController.PayMaintenance();
                DateTextUpdate();
            }
            else yield return null;
        }
    }

    void DateTextUpdate()
    {
        dateText.text = date.ToString() + "日目";
    }
}
