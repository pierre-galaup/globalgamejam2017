using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreetPropsManager : MonoBehaviour
{
    [SerializeField]
    private List<StreetPropSpot> AvailableSpots = new List<StreetPropSpot>();

    [SerializeField]
    private int NumberOfPropsToDisplay = 5;

	// Use this for initialization
	void Start ()
    {
        var spotsList = this.PickMeStreetSpots(Mathf.Min(AvailableSpots.Count, this.NumberOfPropsToDisplay));
        foreach (var spot in spotsList)
        {
            var prop = spot.AvailableProps[Random.Range(0, spot.AvailableProps.Count - 1)];
            Instantiate(prop, spot.transform, false);
        }
	}
	
    private StreetPropSpot[] PickMeStreetSpots(int numberToPick)
    {
        var random = new System.Random();
        var pickedIdx = Enumerable.Range(0, this.AvailableSpots.Count).OrderBy(x => random.Next()).Take(numberToPick).ToList();
        
        var spotsList = new List<StreetPropSpot>();

        foreach (var idx in pickedIdx)
            spotsList.Add(this.AvailableSpots[idx]);

        return spotsList.ToArray();
    }

}
