using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    enum Stage {choosingElement1, choosingElement2, elementChoosen, go };
    Stage stage;

    public Player playerPrefab;

    public Fire fireElement;
    public Land landElement;

    Player player;

	// Use this for initialization
	void Start () {
        player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity, this.transform );
        stage = Stage.choosingElement1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
