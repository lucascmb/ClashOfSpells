using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {

    private static int numberOfCards = 0;
    public Card cardPrefab;

    void Start () {
		
	}
	
	void Update () {

	}

    static void ResetCardNumber()
    {
        numberOfCards = 0;
    }
    
    public void InstantiateCard(int position)
    {
        GameObject.Instantiate(cardPrefab, new Vector3(-6.5f + (position * 4), 10, -5f), Quaternion.identity, this.transform);
        numberOfCards++;
    }
}
