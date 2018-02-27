using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {

    private static int maxNumberOfCards = 4;
    private static int numberOfCards = 0;
    public Card cardPrefab;

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InstantiateCard();
        }
	}

    static void ResetCardNumber()
    {
        numberOfCards = 0;
    }
    
    void InstantiateCard()
    {
        GameObject.Instantiate(cardPrefab, new Vector3(-6, 0, 0), Quaternion.identity, this.transform);
        numberOfCards++;
    }
}
