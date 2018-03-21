using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static bool active;

    enum Stage { preparing, done };
    Stage stage;

	// Use this for initialization
	void Start () {
        stage = Stage.preparing;
	}
	
	// Update is called once per frame
	void Update () {
		if(MasterController.actualStage.Equals("game"))
        {
            if(stage == Stage.preparing)
            {
                PreparePlayer();
            }
        }
	}

    void PreparePlayer()
    {
        Players players = FindObjectOfType<Players>();
        Transform parent = players.transform;

        foreach (Transform child in parent)
        {
            Player currentPlayer = child.GetComponent<Player>();

            currentPlayer.PrepareForBattle();
            currentPlayer.ShowElements();
        }

        
        stage = Stage.done;
    }
}
